using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Dialogs
{
    internal class DialogRewards : Dialog
    {
        // Properties
        private readonly List<string> rewards;
        private readonly List<string> consequences;

        // Constructor
        public DialogRewards(int x, int y, string message, DialogType dialogType, Screen? previousScreen, List<string> rewards, List<string> consequences, string title = "Rewards")
            : base(x, y, title, message, dialogType, previousScreen, new())
        {
            this.rewards = rewards;
            this.consequences = consequences;
        }


        // Methods
        override public Screen? Show()
        {
            DrawHeader();
            
            ShowMessage();

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");

            for (int i = 0; i < rewards.Count; i++)
            {
                string reward = rewards[i];
                Console.SetCursorPosition(x, y++);
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" + " + Utils.AlignLeft(reward, WIDTH - 5));
                Console.ResetColor();
                Console.WriteLine("|");
            }

            for (int i = 0; i < consequences.Count; i++)
            {
                string consequence = consequences[i];
                Console.SetCursorPosition(x, y++);
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" - " + Utils.AlignLeft(consequence, WIDTH - 5));
                Console.ResetColor();
                Console.WriteLine("|");
            }

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("| Press any key to continue!           |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            Console.ReadKey();

            return previousScreen;
        }
    }
}
