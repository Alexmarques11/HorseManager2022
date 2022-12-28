using HorseManager2022.Enums;
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
        private HouseType houseType;

        // Constructor
        public ScreenHouse(Topbar topbar, HouseType houseType, Screen? previousScreen = null)
            : base(topbar, previousScreen)
        {
            this.houseType = houseType;
            boardMenu = new BoardMenu(this);
        }

        override public Screen? Show(GameManager? gameManager)
        {
            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();
                
                topbar.Show(this, gameManager);

                switch (houseType)
                {
                    case HouseType.Vet:
                        DrawVet();
                        break;
                    case HouseType.Shop:
                        DrawShop();
                        break;
                    case HouseType.Stable:
                        DrawHouseDefault();
                        break;
                    case HouseType.Racetrack:
                        DrawHouseDefault();
                        break;
                }


                boardMenu.Show();
            });

            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;

        }


        private void DrawHouseDefault() 
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
            Console.WriteLine("|     /               /    |             ________________|_______|__U________________|____      |     ");
            Console.WriteLine("|    /               /     |____________/                                                 \\     |    ");
            Console.WriteLine("|   /               /      /           /                                                   \\    |    ");
            Console.WriteLine("|  /               /      /           /                                                     \\   |    ");
            Console.WriteLine("| /               /      /           /                                                       \\  |    ");
            Console.WriteLine("|/_______________/      /           /_________________________________________________________\\ |    ");
            Console.WriteLine("||               |     /            |                                                          ||     ");
            Console.WriteLine("||               |    /             |                                                          ||     ");
            Console.WriteLine("||               |   /              |                                                          ||     ");
            Console.WriteLine("||               |  /               |                                                          ||     ");
            Console.WriteLine("||               | /                |                                                          ||     ");
            Console.WriteLine("||_______________|/_________________|__________________________________________________________||     ");

        }


        private void DrawVet()
        {
            Console.WriteLine("   ___________________________________________________________________________________________ ");
            Console.WriteLine("  /        |                                                                         |        \\        ");
            Console.WriteLine(" /         |                                                                         |         \\       "); 
            Console.WriteLine("|          |                                                                         |          |   ");
            Console.WriteLine("|          |        ___________________                 .-.     .-.   .-.            |          |      ");
            Console.WriteLine("|          |       / \\                 \\                | |     | |   | |-.          |          | ");
            Console.WriteLine("|          |      |   |   ~~~~~~~~~~   |.               | |.-.|*| |.-.|*| |          |          |      ");
            Console.WriteLine("|          |       \\_/|   ~~~~~~~~~~   |.               |º|| || |.|| || |.|          |          |      ");
            Console.WriteLine("|          |          |   ~~~~~~~~~~   |.               | ||-|| | ||-||+|.|          |          |      ");
            Console.WriteLine("|          |          |   ~~~~~~~~~~   |.               |º||-||+|.||-||+|.|          |          |      ");
            Console.WriteLine("|          |          |   ~~~~~~~~~~   |.               | || || | || || | |          |          |      ");
            Console.WriteLine("|          |          |   ~~~~~~~~~~   |.              \"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"         |          |      ");
            Console.WriteLine("|          |          |   ~~~~~~~~~~   |.                                            |          |      ");
            Console.WriteLine("|          |          |   _____________|.__                 \\\\||||//                 |          |      ");
            Console.WriteLine("|          |          |  /               /.                 =       =                |          |      ");
            Console.WriteLine("|          |          \\_/dc_____________/.                  | \"\" \"\" |                |          |      ");
            Console.WriteLine("|          |                                                -<0>^<0>-                |          |      ");
            Console.WriteLine("|          |                                                |   j   |                |          |      ");
            Console.WriteLine("|          |                                                 \\ ___ /                 |          |      ");
            Console.WriteLine("|          |                                                  \\ - /                  |          |      ");
            Console.WriteLine("|          |_______________                            _______|\\_/|______            |          |      ");
            Console.WriteLine("|         /               /|                          (       \\   /      )           |          |      ");
            Console.WriteLine("|        /               / |                          |        \\ /       |           |          |       ");
            Console.WriteLine("|       /               /  |                          |   |     V    |   |           |          |      ");
            Console.WriteLine("|      /               /   |                          |   |       vet|   |           |          |      ");
            Console.WriteLine("|     /               /    |             _____________|___|__________|___|___________|____      |      ");
            Console.WriteLine("|    /               /     |____________/                                                 \\     |     ");
            Console.WriteLine("|   /               /      /           /                                                   \\    | ");
            Console.WriteLine("|  /               /      /           /                                                     \\   |     ");
            Console.WriteLine("| /               /      /           /                                                       \\  | ");
            Console.WriteLine("|/_______________/      /           /_________________________________________________________\\ |     ");
            Console.WriteLine("||               |     /            |                                                          ||      ");
            Console.WriteLine("||               |    /             |                                                          || ");
            Console.WriteLine("||               |   /              |                                                          ||      ");
            Console.WriteLine("||               |  /               |                                                          ||      ");
            Console.WriteLine("||               | /                |                                                          ||      ");
            Console.WriteLine("||_______________|/_________________|__________________________________________________________||     ");

        }
        

        private void DrawShop()
        {
            Console.WriteLine("   ___________________________________________________________________________________________");
            Console.WriteLine("  /        |                                                                         |        \\       ");
            Console.WriteLine(" /         |                                                                         |         \\      ");
            Console.WriteLine("|          |                                   ___________________________________   |          |     ");
            Console.WriteLine("|          |                                  (__| |_________________________| |__)  |          |     ");
            Console.WriteLine("|          |                                     | |            {)           | |     |          |     ");
            Console.WriteLine("|          |                                     | |         c==//\\          | |     |          |     ");
            Console.WriteLine("|          |                                     | |     _-~~/-._|-|         | |     |          |     ");
            Console.WriteLine("|          |  :::::::::::::::::::::::::::::::    | |    /'_,/,   //'~~\\;;,   | |     |          |     ");
            Console.WriteLine("|          |  :::::::::::::::::::::::::::::::    | |    `~  _(_||_..\\ | ';;  | |     |          |     ");
            Console.WriteLine("|          |  ::                 .''       ::    | |      /'~|/ ~' `\\<\\> ;   | |     |          |     ");
            Console.WriteLine("|          |  ::         ._.-__.' (`\\      ::    | |        \"  |      /  |   | |     |          |     ");
            Console.WriteLine("|          |  ::        //(      ( `'      ::    | |          \"      \"  \"    | |     |          |     ");
            Console.WriteLine("|          |  ::       '/ )\\ )._. )        ::  __| |_________________________| |__   |          |     ");
            Console.WriteLine("|          |  ::       ' <' `\\ ._/'\\       :: (__|_|_________________________|_|__)  |          |     ");
            Console.WriteLine("|          |  ::          `   \\     \\      ::                                        |          |     ");
            Console.WriteLine("|          |  :::::::::::::::::::::::::::::::                                        |          |     ");
            Console.WriteLine("|          |  :::::::::::::::::::::::::::::::               ,...,                    |          |     ");
            Console.WriteLine("|          |                                                |. .|                    |          |     ");
            Console.WriteLine("|          |                                                q - p                    |          |     ");
            Console.WriteLine("|          |_______________                                 |\\'/|                    |          |     ");
            Console.WriteLine("|         /               /|                             .-''---''-.                 |          |     ");
            Console.WriteLine("|        /               / |                            /           \\                |          |     ");
            Console.WriteLine("|       / +------+      /  |                           /             \\               |          |     ");
            Console.WriteLine("|      .' Wish  '|     /   |                          /   /\\      /\\  \\              |          |");
            Console.WriteLine("|     +------+'  |    /    |             _____________\\___\\_|____|_/__/______________|____      |     ");
            Console.WriteLine("|    /|      |   |   /     |____________/                                                 \\     |    ");
            Console.WriteLine("|   / | Loot |   +  /      /           /                                                   \\    |    ");
            Console.WriteLine("|  /  |      | .'  /      /           /                                                     \\   |    ");
            Console.WriteLine("| /   +------+'   /      /           /                                                       \\  |    ");
            Console.WriteLine("|/_______________/      /           /_________________________________________________________\\ |    ");
            Console.WriteLine("||               |     /            |                        _          | '_ \\                 ||     ");
            Console.WriteLine("||               |    /             |                    __ | |__   ___ | |_) \\                ||     ");
            Console.WriteLine("||               |   /              |                   / __|  _  \\/   \\|  __/                 ||     ");
            Console.WriteLine("||               |  /               |                   \\__ \\ | | | (_) |  |                   ||     ");
            Console.WriteLine("||               | /                |                   |___/_| |_|\\___/|__|                   ||     ");
            Console.WriteLine("||_______________|/_________________|__________________________________________________________|| ");
        }
    }
}
