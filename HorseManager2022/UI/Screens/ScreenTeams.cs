using HorseManager2022.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Screens
{
    internal class ScreenTeams<T, U> : ScreenTable<T, U>
    {
        // Properties
        private Action onAddNewTeam = () =>
        {
            Console.WriteLine("Hello World");
            Console.ReadKey();
        };


        // Constructor
        public ScreenTeams(Topbar topbar, string title, Screen? previousScreen = null, string[]? propertiesToExclude = null, bool isSelectable = false, bool isAddable = false)
            : base(topbar, title, previousScreen, propertiesToExclude, isSelectable, isAddable)
        {
        }


        override protected void SetTableOptions(GameManager? gameManager)
        {
            foreach (T item in table.GetTableItems(gameManager))
            {
                Action onEnter = GetOptionOnEnter(item, gameManager);
                options.Add(new Option(nextScreen: this, onEnter: onEnter));
            }
        }


        override protected void SetAdditionalOptions(GameManager? gameManager)
        {
            options.Add(new Option(nextScreen: this, onEnter: onAddNewTeam));
        }


        private Action GetOptionOnEnter(T item, GameManager? gameManager)
        {
            return () =>
            {

            };
        }
    }
}
