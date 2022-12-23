using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Dialogs
{
    internal abstract class Dialog : SelectableObject
    {
        // Constants
        protected const int WIDTH = 40;

        // Properties
        protected int x { get; set; }
        protected int y { get; set; }
        private string title { get; set; }
        private string message { get; set; }
        protected Screen? previousScreen { get; set; }
        private DialogType dialogType { get; set; }

        // Constructor
        public Dialog(int x, int y, string title, string message, DialogType dialogType, Screen? previousScreen, List<Option> options)
        {
            this.x = x;
            this.y = y;
            this.title = title;
            this.message = message;
            this.previousScreen = previousScreen;
            this.options = options;
            this.dialogType = dialogType;
        }

        // Methods
        abstract public Screen? Show();

        
        protected void DrawHeader()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");

            // Write title
            Console.SetCursorPosition(x, y++);
            Console.Write("| ");
            ShowDialogIcon();
            Console.Write(" ");
            Console.Write(title.PadLeft((WIDTH / 2) + (title.Length / 2) - 6).PadRight(WIDTH - 15));
            Console.WriteLine(" - [] X |");

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|--------------------------------------|");
        }


        private void ShowDialogIcon()
        {
            Console.ForegroundColor = DialogTypeExtensions.GetColor(dialogType);
            Console.Write($"[{DialogTypeExtensions.GetIcon(dialogType)}]");
            Console.ResetColor();
        }
        
        
        protected void ShowMessage()
        {
            // Write message if the message is too long add more necessary lines
            int lines = message.Length / (WIDTH - 4);
            for (int i = 0; i <= lines; i++)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("| ");
                Console.Write(message.Substring(i * (WIDTH - 4), Math.Min((WIDTH - 4), message.Length - i * (WIDTH - 4))).PadRight(WIDTH - 4));
                Console.WriteLine(" |");
            }
        }
    }
}
