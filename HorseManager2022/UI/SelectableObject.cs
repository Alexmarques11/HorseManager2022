using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseManager2022.Interfaces;

namespace HorseManager2022.UI
{
    internal abstract class SelectableObject : ISelectable
    {
        // Properties
        public List<Option> options { get; set; }
        virtual public int selectedPosition { get; set; }

        
        // Constructor
        public SelectableObject()
        {
            options = new List<Option>();
        }

        
        // Methods
        public void AddOption(string text, Screen? nextScreen, Action onEnter) =>
            options.Add(new Option(text, nextScreen, onEnter));

        
        public void AddOption(Option option) => options.Add(option);
        

        public void ClearOptions() => options.Clear();

        
        public Option WaitForOption(Action onWait)
        {
            Option? selectedOption = null;

            do
            {
                onWait();

                // Get option
                selectedOption = SelectOption();

            } while (selectedOption == null);

            return selectedOption;
        }
        
        // Methods for each selection direction (up, down, left, right)
        virtual public void SelectLeft() { }
        virtual public void SelectRight() { }
        virtual public void SelectUp() { }
        virtual public void SelectDown() { }
        virtual public Option? SelectEnter() => null;

        // Select option from the list
        public Option? SelectOption()
        {
            // Read key
            ConsoleKeyInfo selectedOption = Console.ReadKey();

            // Check for up / down / enter / esc keys
            switch (selectedOption.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    SelectLeft();
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    SelectRight();
                    break;

                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    SelectUp();
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    SelectDown();
                    break;

                case ConsoleKey.Enter:
                    return SelectEnter();
                default:
                    break;
            }

            return null;
        }
    }

}
