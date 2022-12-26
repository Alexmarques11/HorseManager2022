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
using static System.Net.Mime.MediaTypeNames;

namespace HorseManager2022.UI.Screens
{
    internal class ScreenTeams : ScreenTable<Team, Player>
    {
        // Constants
        private const int DIALOG_POSITION_X = 20, DIALOG_POSITION_Y = 8;

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


        private bool IsHorseTooTired(Horse horse) => horse.energy < 33;


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
                // Initial verifications
                if (gameManager == null)
                    return;

                // Get data
                Event? todayEvent = Event.GetTodayEvent(gameManager);
                int entryCost = todayEvent?.GetEntryCost() ?? 0;
                bool isHorseTooTired = IsHorseTooTired(team.horse);
                Race? race;

                // Check if horse is too tired
                if (isHorseTooTired)
                {
                    ShowHorseTiredDialog();
                    return;
                }

                // Training mode
                if (raceType == RaceType.Training) {
                    race = GetTrainingRace(team, todayEvent);
                    race?.Start(gameManager);
                }
                // Event mode (race / demonstration)
                else 
                {
                    // Check if player have money to buy ticket
                    if (entryCost > gameManager.money)
                    {
                        ShowNotEnoughMoneyDialog(gameManager, entryCost);
                        return;
                    }

                    // Create race
                    race = GetEventRace(team, todayEvent);
                    if (race == null) return;

                    if (todayEvent?.type == EventType.Race)
                        ShowRaceEnterDialog(team, todayEvent, race, gameManager, entryCost);
                    else if (todayEvent?.type == EventType.Demostration)
                        ShowDemonstrationEnterDialog(team, todayEvent, race, gameManager);
                }

            };
        }


        private Race? GetTrainingRace(Team team, Event? todayEvent)
        {
            if (todayEvent != null && todayEvent?.type == EventType.Holiday)
            {
                ShowCoachUnavailableDialog();
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


        // Dialogs
        private void ShowHorseTiredDialog()
        {
            DialogMessage dialogWarning = new(
                    x: DIALOG_POSITION_X, y: DIALOG_POSITION_Y,
                    title: "Low energy!",
                    message: "This horse is too tired!",
                    dialogType: DialogType.Error,
                    previousScreen: this
                );
            dialogWarning.Show();
        }

        
        // Dialogs
        private void ShowCoachUnavailableDialog()
        {
            DialogMessage dialogWarning = new(
                    x: DIALOG_POSITION_X, y: DIALOG_POSITION_Y,
                    title: "Coach is unavailable!",
                    message: "Coach don't work on holidays!",
                    dialogType: DialogType.Error,
                    previousScreen: this
                );
            dialogWarning.Show();
        }

        
        private void ShowDemonstrationEnterDialog(Team team, Event todayEvent, Race race, GameManager gameManager)
        {
            string text = Utils.AlignLeft($"Are you sure you want to enter?", 36);
            text += Utils.AlignLeft($"Demonstrations are free and very    rewarding, ", 36);
            text += Utils.AlignLeft($"but your horse will lose all energy!", 36);

            DialogConfirmation dialogConfirmation = new(
                    x: DIALOG_POSITION_X, y: DIALOG_POSITION_Y,
                    title: $"{todayEvent.difficulty} Demostration",
                   message: text,
                   dialogType: DialogType.Question,
                   previousScreen: this,
                   onConfirm: () =>
                   {
                       race?.Start(gameManager);
                   },
                   onCancel: () => { }
                   );
            dialogConfirmation.Show();
        }

        
        private void ShowRaceEnterDialog(Team team, Event todayEvent, Race race, GameManager gameManager, int entryCost)
        {
            string entryCostText = entryCost.ToString("C");
            string text = "Are you sure you want to enter?     ";
            text += Utils.AlignLeft($"It will cost you {entryCostText}!", 36);
            text += "If you enter you can't do anything  else today!";

            DialogConfirmation dialogConfirmation = new(
                x: DIALOG_POSITION_X, y: DIALOG_POSITION_Y,
                title: $"{todayEvent.difficulty} Race",
                message: text,
                dialogType: DialogType.Question,
                previousScreen: this,
                onConfirm: () =>
                {
                    race?.Start(gameManager);
                },
                onCancel: () => { }
                );

            dialogConfirmation.Show();
        }


        private void ShowNotEnoughMoneyDialog(GameManager gameManager, int entryCost)
        {

            string message = Utils.AlignLeft($"You can't buy the ticket!", 36);
            string missingValue = (entryCost - gameManager.money).ToString("C");
            message += Utils.AlignLeft($"You need more {missingValue} to enter!", 36);

            DialogMessage dialogMessage = new(
                x: DIALOG_POSITION_X, y: DIALOG_POSITION_Y,
                title: "Insufficient funds!",
                message: message,
                dialogType: DialogType.Error,
                previousScreen: this);

            dialogMessage.Show();

        }


    }
}
