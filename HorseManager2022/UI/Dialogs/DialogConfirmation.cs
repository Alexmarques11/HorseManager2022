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
        private string message { get; set; }

        // Constructor
        public DialogConfirmation(int x, int y, string title, string message, Screen previousScreen, Action onConfirm, Action onCancel) : base(x, y, title, previousScreen, new List<Option>())
        {
            this.message = message;
            options.Add(new Option("Yes", previousScreen, () => onConfirm()));
            options.Add(new Option("No", previousScreen, () => onCancel()));
        }

        // Methods
        override public Screen? Show()
        {

            // Wait for option
            Option? selectedOption = WaitForOption(() => {

                DrawHeader();

                // Write message if the message is too long add more necessary lines
                int lines = message.Length / (WIDTH - 4);
                for (int i = 0; i <= lines; i++)
                {
                    Console.SetCursorPosition(x, y + 3 + i);
                    Console.Write("| ");
                    Console.Write(message.Substring(i * (WIDTH - 4), Math.Min((WIDTH - 4), message.Length - i * (WIDTH - 4))).PadRight(WIDTH - 4));
                    Console.WriteLine(" |");
                }

                Console.SetCursorPosition(x, y + 4 + lines);
                Console.WriteLine("|                                      |");
                Console.SetCursorPosition(x, y + 5 + lines);
                Console.WriteLine("|             Yes     No               |");
                Console.SetCursorPosition(x, y + 6 + lines);
                if (selectedPosition == 0)
                    Console.WriteLine("|             [X]     [ ]              |");
                else if (selectedPosition == 1)
                    Console.WriteLine("|             [ ]     [X]              |");
                Console.SetCursorPosition(x, y + 7 + lines);
                Console.WriteLine("|                                      |");
                Console.SetCursorPosition(x, y + 8 + lines);
                Console.WriteLine("+--------------------------------------+");

            });

            selectedOption.onEnter();
            return selectedOption.nextScreen;
        }

        
        // Methods for each selection direction (up, down, left, right)
        override public void SelectLeft() => selectedPosition = (selectedPosition == 0) ? 1 : 0;
        override public void SelectRight() => SelectLeft();
        override public void SelectUp() { }
        override public void SelectDown() { }
        override public Option? SelectEnter() => options[selectedPosition];

    }
}
