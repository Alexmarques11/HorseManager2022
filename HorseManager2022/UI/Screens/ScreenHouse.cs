using HorseManager2022.Models;
using HorseManager2022.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class ScreenHouse : ScreenWithTopbar
    {
        // Properties
        private BoardMenu boardMenu;

        // Constructor
        public ScreenHouse(Topbar topbar, Screen? previousScreen = null)
            : base(topbar, previousScreen)
        {
            boardMenu = new BoardMenu(this);
        }

        override public Screen? Show(GameManager? gameManager)
        {
            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();
                
                topbar.Show(this, gameManager);
                DrawHouse();

                boardMenu.Show();

            });

            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;

        }


        private void DrawHouse() 
        {
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("   ___________________________________________________________________________________________");
            Console.WriteLine("  /        |             .-.                                                         |        \\       ");
            Console.WriteLine(" /         |       .-.   | |-.                                                       |         \\      ");
            Console.WriteLine("|          |       | |.-.|*| |                             .-.     .-.               |          |     ");
            Console.WriteLine("|          |       |º|| || |.|<\\                     .-.   | |-.   | |-.             |          |     ");
            Console.WriteLine("|          |       | ||-|| | | \\                     | |.-.|*| |.-.|*| |             |          |     ");
            Console.WriteLine("|          |       |º||-||+|.|  \\                    |º|| || |.|| || |.|             |          |     ");
            Console.WriteLine("|          |       | || || | |   \\>                  | ||-|| | ||-||+|.|             |          |     ");
            Console.WriteLine("|          |     \"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"                |º||-||+|.||-||+|.|             |          |     ");
            Console.WriteLine("|          |                                         | || || | || || | |             |          |     ");
            Console.WriteLine("|          |                                        \"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"            |          |     ");
            Console.WriteLine("|          |                                                                         |          |     ");
            Console.WriteLine("|          |                                               ,,,,,                     |          |     ");
            Console.WriteLine("|          |                                              (wwwww)                    |          |     ");
            Console.WriteLine("|          |                                             .` 0 0 `.                   |          |     ");
            Console.WriteLine("|          |                                              |  ^  |                    |          |     ");
            Console.WriteLine("|          |                                              _\\`-´/_                    |          |     ");
            Console.WriteLine("|          |                                          _.-´\\_____/`-.                 |          |     ");
            Console.WriteLine("|          |                                         /  _ \\     / _ \\                |          |     ");
            Console.WriteLine("|          |_______________                          | | | \\   / | | |               |          |     ");
            Console.WriteLine("|         /               /|                         | | |  \\ /  | | |               |          |     ");
            Console.WriteLine("|        /               / |                         | \\ |  N.B  | | |               |          |     ");
            Console.WriteLine("|       /               /  |                          \\ \\|_______| | |               |          |     ");
            Console.WriteLine("|      /               /   |                           \\_|_|_|_|_| |_|               |          |     ");
        }
    }
}
