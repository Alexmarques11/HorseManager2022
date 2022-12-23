using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using HorseManager2022.UI.Components;
using HorseManager2022.UI.Dialogs;
using System;
using System.Collections.Generic;
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
                Race race;
                switch (raceType)
                {
                    case RaceType.Training:
                        
                        race = new(team, this);
                        
                        break;

                    case RaceType.Event:

                        List<Team> competitors = Team.GenerateRandomTeams(4);
                        competitors.Add(team);
                        race = new("Event", competitors, this);

                        break;
                    default:
                        return;
                }

                race.Start();
            };
        }
    }
}
