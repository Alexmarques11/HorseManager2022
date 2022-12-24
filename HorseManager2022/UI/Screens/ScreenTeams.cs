using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using HorseManager2022.UI.Components;
using HorseManager2022.UI.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Screens
{
    internal class ScreenTeams : ScreenTable<Team, Player>
    {
        // Properties
        private readonly RaceType? raceType;
        private ScreenTable<Horse, Player> screenTableHorses { get; set; }
        private ScreenTable<Jockey, Player> screenTableJockeys { get; set; }

        // Constructor
        public ScreenTeams(Topbar topbar, string title, Screen? previousScreen = null, string[]? propertiesToExclude = null, bool isSelectable = false, bool isAddable = false, RaceType? raceType = null)
            : base(topbar, title, previousScreen, propertiesToExclude, isSelectable, isAddable)
        {
            screenTableHorses = new ScreenTable<Horse, Player>(topbar, "Horses", this, new string[] { "price" }, true, false);
            screenTableJockeys = new ScreenTable<Jockey, Player>(topbar, "Jockeys", this, new string[] { "price" }, true, false);
            this.raceType = raceType;
        }


        override protected void SetAdditionalOptions(GameManager? gameManager)
        {
            if (gameManager == null)
                return;

            options.Add(new Option(nextScreen: this, onEnter: () => {

                // Select horse
                screenTableHorses.Show(gameManager);
                List<Horse> horsesInTable = screenTableHorses.table.GetTableItems(gameManager);
                if (screenTableHorses.menuMode == MenuMode.Up) return; // Check if back button was pressed
                Horse selectedHorse = horsesInTable[screenTableHorses.selectedPosition];

                // Select jockey
                screenTableJockeys.Show(gameManager);
                List<Jockey> jockeysInTable = screenTableJockeys.table.GetTableItems(gameManager);
                if (screenTableJockeys.menuMode == MenuMode.Up) return; // Check if back button was pressed
                Jockey selectedJockey = jockeysInTable[screenTableJockeys.selectedPosition];

                // Check if horse and jockey are already in team
                if (IsHorseAndJockeyInTeam(gameManager, selectedHorse, selectedJockey))
                {
                    DialogMessage dialogWarning = new(
                            x: 20, y: 8,
                            title: "Team not allowed!",
                            message: "Horse and jockey are already a team!",
                            dialogType: DialogType.Error,
                            previousScreen: this
                        );
                    dialogWarning.Show();
                }
                else
                {
                    // Add team
                    gameManager?.Add<Team, Player>(new Team(selectedHorse, selectedJockey, 0));
                }

            }));
        }


        private bool IsHorseAndJockeyInTeam(GameManager? gameManager, Horse horse, Jockey jockey)
        {
            if (gameManager == null)
                return false;

            List<Team> teams = gameManager.GetList<Team, Player>();
            foreach (var team in teams)
            {
                if (team.horse == horse && team.jockey == jockey)
                    return true;
            }

            return false;
        }

        
        override protected Action GetOptionOnEnter(Team team, GameManager? gameManager)
        {
            return () =>
            {
                if (gameManager == null)
                    return;

                Event? todayEvent = Event.GetTodayEvent(gameManager);
                Race? race;

                if (raceType == RaceType.Training) {
                    race = GetTrainingRace(team, todayEvent);
                    race?.Start(gameManager);
                }
                else 
                {
                    // Get event entry cost
                    int entryCost = todayEvent?.GetEntryCost() ?? 0;

                    // Check if player have money to buy ticket
                    if (entryCost > gameManager.money)
                    {
                        string message = "You can't buy the ticket!";
                        string missingValue = (entryCost - gameManager.money).ToString("C");
                        message += Utils.AlignLeft($"You need more {missingValue} to enter!", 36);

                        DialogMessage dialogMessage = new(
                            x: 20, y: 8,
                            title: "Insuficient funds!",
                            message: message,
                            dialogType: DialogType.Question,
                            previousScreen: this);

                        return;
                    }

                    // Create race
                    race = GetEventRace(team, todayEvent);
                    if (race == null) return;

                    if (todayEvent?.type == EventType.Race)
                    {
                        string entryCostText = entryCost.ToString("C");
                        string text = "Are you sure you want to enter?     ";
                        text += Utils.AlignLeft($"It will cost you {entryCostText}!", 36);
                        text += "If you enter you can't do anything  else today!";

                        DialogConfirmation dialogConfirmation = new(
                            x: 20, y: 8,
                            title: "Confirmation",
                            message: text,
                            dialogType: DialogType.Question,
                            previousScreen: this,
                            onConfirm: () =>
                            {
                                // Buy the entry ticket
                                gameManager.money -= entryCost;
                                race?.Start(gameManager);
                            },
                            onCancel: () => { }
                            );
                    
                        dialogConfirmation.Show();
                    }
                    else
                    {
                        race?.Start(gameManager);
                    }
                }

            };
        }


        private Race? GetTrainingRace(Team team, Event? todayEvent)
        {
            if (todayEvent != null && todayEvent?.type == EventType.Holiday)
            {
                DialogMessage dialogWarning = new(
                        x: 20, y: 8,
                        title: "Coach is unavailable!",
                        message: "Coach don't work on holidays!",
                        dialogType: DialogType.Error,
                        previousScreen: this
                    );
                dialogWarning.Show();
                return null;
            }
            
            return new(team, this);
        }

        
        private Race? GetEventRace(Team team, Event? todayEvent)
        {
            if (todayEvent == null || todayEvent?.type == EventType.Holiday)
            {
                string message = "There is no race event today!";
                if (todayEvent?.type == EventType.Holiday)
                    message += "       Go enjoy the holiday!";

                DialogMessage dialogWarning = new(
                        x: 20, y: 8,
                        title: "No race event today!",
                        message: message,
                        dialogType: DialogType.Error,
                        previousScreen: this
                    );
                dialogWarning.Show();
                return null;
            }



            List<Team> competitors = Team.GenerateEventTeams(todayEvent ?? new());
            return new(team, competitors, this, todayEvent ?? new());
        }
    }
}
