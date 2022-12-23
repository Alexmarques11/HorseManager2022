using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using HorseManager2022.UI.Components;
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
        private Screen addTeamScreen { get; set; }

        // Constructor
        public ScreenTeams(Topbar topbar, string title, Screen addTeamScreen, Screen? previousScreen = null, string[]? propertiesToExclude = null, bool isSelectable = false, bool isAddable = false)
            : base(topbar, title, previousScreen, propertiesToExclude, isSelectable, isAddable)
        {
            this.addTeamScreen = addTeamScreen;
        }


        override protected void SetTableOptions(GameManager? gameManager)
        {
            foreach (Team item in table.GetTableItems(gameManager))
            {
                Action onEnter = GetOptionOnEnter(item, gameManager);
                options.Add(new Option(nextScreen: this, onEnter: onEnter));
            }
        }


        override protected void SetAdditionalOptions(GameManager? gameManager)
        {
            options.Add(new Option(nextScreen: addTeamScreen, onEnter: () => { Console.WriteLine("Add team"); Console.ReadKey(); }));
        }


        private Action GetOptionOnEnter(Team item, GameManager? gameManager)
        {
            return () =>
            {
                Console.WriteLine("Team selected: " + item.horseName + " & " + item.jockeyName);
                Console.ReadKey();
            };
        }
    }
}
