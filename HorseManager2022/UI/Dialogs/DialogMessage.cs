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
        // Constructor
        public DialogMessage(int x, int y, string message, DialogType dialogType, Screen? previousScreen, string title = "Warning")
            : base(x, y, title, message, dialogType, previousScreen, new())
        {
        }


        // Methods
        override public Screen? Show()
        {
            DrawHeader();

            ShowMessage();

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("| Press any key to continue!           |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            Console.ReadKey(true);

            return previousScreen;
        }
    }
}
