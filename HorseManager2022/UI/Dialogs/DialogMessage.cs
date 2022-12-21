using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Dialogs
{
    internal class DialogMessage : Dialog
    {
        // Constants
        private const string title = "Warning";

        // Constructor
        public DialogMessage(int x, int y, string message, DialogType dialogType, Screen? previousScreen)
            : base(x, y, title, message, dialogType, previousScreen, new())
        {
        }


        // Methods
        override public Screen? Show()
        {
            DrawHeader();

            int currentLine = ShowMessage();

            Console.SetCursorPosition(x, y + 4 + currentLine);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y + 5 + currentLine);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y + 6 + currentLine);
            Console.WriteLine("| Press any key to continue!           |");
            Console.SetCursorPosition(x, y + 7 + currentLine);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y + 8 + currentLine);
            Console.WriteLine("+--------------------------------------+");
            Console.ReadKey(true);

            return previousScreen;
        }
    }
}
