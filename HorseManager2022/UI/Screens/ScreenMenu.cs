using HorseManager2022.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class ScreenMenu : Screen
    {
        // Properties
        public string title;

        
        // Constructor
        public ScreenMenu(string title, Screen? previousScreen = null)
            : base(previousScreen)
        {
            this.title = title;
        }


        override public Screen? Show(GameManager? gameManager)
        {
            base.Show(gameManager);
            // Variables
            string title = this.title;
            string mark = "";
            title = title.PadLeft((37 / 2) + (title.Length / 2)).PadRight(37);

            // Wait for option
            Option? selectedOption = WaitForOption(() => {

                // Display Title
                Console.Clear();
                Console.WriteLine("+---------------------------------------+");
                Console.WriteLine("| " + title + " |");
                Console.WriteLine("+---------------------------------------+");
                Console.WriteLine("|                                       |");

                // Display Options
                for (int i = 0; i < this.options.Count; i++)
                {
                    string text = this.options[i].text.PadRight(32, ' ');
                    mark = (i == this.selectedPosition) ? "X" : " ";
                    Console.WriteLine("| [" + mark + "] - " + text + "|");
                    Console.WriteLine("|                                       |");
                }

                // Display Back / Exit Option
                mark = (this.options.Count == this.selectedPosition) ? "X" : " ";
                if (this.isInitialScreen)
                    Console.WriteLine("| [" + mark + "] - Exit                            |");
                else
                    Console.WriteLine("| [" + mark + "] - Back                            |");

                // Close Menu
                Console.WriteLine("|                                       |");
                Console.WriteLine("+---------------------------------------+");

            });
            
            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;

        }

        // Methods for each selection direction (up, down, left, right)
        override public void SelectUp()
        {
            if (this.selectedPosition > 0)
                this.selectedPosition--;
            else
                this.selectedPosition = this.options.Count;
        }

        override public void SelectDown()
        {
            if (this.selectedPosition < this.options.Count)
                this.selectedPosition++;
            else
                this.selectedPosition = 0;
        }

        override public void SelectLeft() => SelectUp();
        override public void SelectRight() => SelectDown();

        override public Option? SelectEnter()
        {
            if (this.selectedPosition == this.options.Count)
                return Option.GetBackOption(this.previousScreen);
            else
                return this.options[this.selectedPosition];
        }
        
    }
}
