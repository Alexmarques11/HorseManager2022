using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseManager2022;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using HorseManager2022.Attributes;
using System.Reflection.PortableExecutable;

namespace HorseManager2022.UI.Screens
{
    internal class ScreenTable<T> : ScreenWithTopbar
    {
        // Properties
        private Table<T> table;

        
        // Constructor
        public ScreenTable(Topbar topbar, Screen? previousScreen = null, string[]? propertiesToExclude = null)
            : base(topbar, previousScreen)
        {
            table = new Table<T>(propertiesToExclude ?? Array.Empty<string>());
        }


        // Methods
        override public Screen? Show(GameManager? gameManager)
        {
            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();

                topbar.Show(this, gameManager);

                table.Show(gameManager);

            });

            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;
        }
        

        override public Option? SelectEnter()
        {
            if (menuMode == MenuMode.Down)
                return Option.GetBackOption(this.previousScreen);
            else
            {
                if (this.selectedPosition == this.topbar.options.Count)
                {
                    return Option.GetBackOption(this.previousScreen);
                }
                else
                    return this.topbar.options[this.selectedPosition];
            }
        }

    }
}
