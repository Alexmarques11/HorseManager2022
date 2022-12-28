using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Dialogs
{
    internal class DialogConfirmation : Dialog
    {
        // Properties
        private int initialY;

        // Constructor
        public DialogConfirmation(int x, int y, string title, string message, DialogType dialogType, Screen previousScreen, Action onConfirm, Action onCancel) : base(x, y, title, message, dialogType, previousScreen, new List<Option>())
        {
            options.Add(new Option("Yes", previousScreen, () => onConfirm()));
            options.Add(new Option("No", previousScreen, () => onCancel()));
            initialY = y;
        }

        // Methods
        override public Screen? Show()
        {
            // Wait for option
            Option? selectedOption = WaitForOption(() => {

                y = initialY;
                DrawHeader();

                ShowMessage();

                Console.SetCursorPosition(x, y++);
                Console.WriteLine("|                                      |");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("|             Yes     No               |");
                Console.SetCursorPosition(x, y++);
                if (selectedPosition == 0)
                    Console.WriteLine("|             [X]     [ ]              |");
                else if (selectedPosition == 1)
                    Console.WriteLine("|             [ ]     [X]              |");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("|                                      |");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("+--------------------------------------+");

            });

            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;
        }

        
        // Methods for each selection direction (up, down, left, right)
        override public void SelectLeft() => selectedPosition = (selectedPosition == 0) ? 1 : 0;
        override public void SelectRight() => SelectLeft();
        override public void SelectUp() { }
        override public void SelectDown() { }
        override public Option? SelectEnter() => options[selectedPosition];

    }
}
