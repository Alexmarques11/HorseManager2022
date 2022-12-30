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
    internal class ScreenCity : ScreenWithTopbar
    {
        // Constants
        public const int SCREEN_SIZE_MINIMUM_CITY = 175;

        // Properties
        private readonly Arrow arrow;

        public override int selectedPosition
        {
            get
            {
                return base.selectedPosition;
            }
            set
            {
                if (menuMode == MenuMode.Down && value > options.Count)
                    value = options.Count;

                if (menuMode == MenuMode.Up && value > topbar.options.Count)
                    value = topbar.options.Count;

                topbar.selectedPosition = value;
                arrow.selectedPosition = value;
                base.selectedPosition = value;
            }
        }


        // Constructor
        public ScreenCity(Topbar topbar, Screen? previousScreen = null)
            : base(topbar, previousScreen)
        {

            List<(int, int)> customPositions = new()
            {
                (3, 10),
                (22, 8),
                (75, 8),
                (125, 12)
            };
            arrow = new Arrow(customPositions, -22, Topbar.TOPBAR_HEIGHT, 22);

        }


        override public Screen? Show(GameManager? gameManager)
        {
            // Reset positions
            base.Show(gameManager);

            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();

                topbar.Show(this, gameManager);
                if (Console.WindowWidth > SCREEN_SIZE_MINIMUM_CITY)
                    DrawCity();
                else
                    DrawSimplifiedCity();

                if (menuMode == MenuMode.Down)
                    arrow.Draw();

            });

            // Return next screen
            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;
        }

        
        private void DrawSimplifiedCity()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("                           _||____                                                                       ");
            Console.WriteLine("                          /- - - -\\                                                                     ");
            Console.WriteLine("                         /_________\\    8888                                                            ");
            Console.WriteLine("       _||_____         /|         |\\  888888      _||_______            ____||_                        ");
            Console.WriteLine("      /- - - - \\         |  []  [] |  88888888    /- - - - - \\          /- - - -\\                     ");
            Console.WriteLine("     /__________\\        |         |    || |     /____________\\        /_________\\                    ");
            Console.WriteLine("    /|    VET   |\\       |    SHOP |    |  |    /| STABLE     |\\      /|    RACE |\\                   ");
            Console.WriteLine("    /| [] ____  |\\       |    ____ |    |  |    /| ____   []  |\\      /| [] ____ |\\                   ");
            Console.WriteLine("     |    |. |  |        |    |. | |    | ||     | |. |       |        |    |. | |                       ");
            Console.WriteLine("_____|____|__|__|________|____|__|_|____|__|_____|_|__|_______|________|____|__|_|___                    ");
            Console.WriteLine("                                                                                                         ");
            Console.WriteLine("      _||______            _____          _________                                                      ");
            Console.WriteLine("_____/-|| - - -\\__________/- - -\\________/- - - - -\\__________________________________                ");
            Console.WriteLine("    /___________\\ -      /_______\\      /___________\\               ____                              ");
            Console.WriteLine("   /|           |\\      /|       |\\ -  /|           |\\  -    ____.-\"    \\___    -                   ");
            Console.WriteLine("    |           |        |       |      |           |    ___/              (_____   |    -        -      ");
            Console.WriteLine("    |___________|    -   |_______|   -  |___________|   (                        \"-.!||                 ");
            Console.WriteLine("                                                         \\       ~~          ~     ( !!|||  -  -    -   ");
            Console.WriteLine("  -         -                                     -    - :                         \"-.!!! |             ");
            Console.WriteLine("       -                -          -                      /               ~~            \\___!        -  ");
            Console.WriteLine("                                            -        ____)      ~                          \"-           ");
            Console.WriteLine("      -        -     -                              (     ~~                   ~~            \"-.  -     ");
            Console.WriteLine("                             -                       \\   ~         ~~                      __.-\"       ");
            Console.WriteLine("           -           -                 -            \\_____                    ~~      .-\"            ");
            Console.WriteLine("   -           -               -                            \"-.    ~                   \\        -      ");
            Console.WriteLine("                                                               \"-.______  ~        _____)     -         ");
            Console.WriteLine("                                                                        ´-.____.-´                       ");
        }
        

        private void DrawCity()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("                 ___                                 ");
            Console.WriteLine("               _/XXX\\                                                                                                                            __");
            Console.WriteLine("_             /XXXXXX\\_                                                        __                                    _             _            /XX\\");
            Console.WriteLine("X\\__    __   /X XXXX XX\\                                              _       /XX\\__      ___                 __    /X\\           /X\\          /X XX\\               ");                                          
            Console.WriteLine("    \\__/  \\_/__       \\ \\            _                              _/X\\__   /XX XXX\\____/XXX\\    ___       _/XX\\__/X X\\  __    _/X  \\   _    /XX XXX\\_____                   ");                  
            Console.WriteLine("  \\  ___   \\/  \\_      \\ \\          / \\  __                  __   _/      \\_/  _/  -   __  -  \\__/   \\     / \\    ___   \\/  \\__/      \\_/ \\__/  -  __   __ \\__                           ");               
            Console.WriteLine(" ___/   \\__/   \\ \\__     \\\\__º  ___/   \\/  \\                /  \\_//         \\     __  /  \\____//      \\___/   \\__/   \\____     \\_  _      __/     /  \\_/  \\   \\                              ");                  
            Console.WriteLine("/  __    \\  /     \\ \\_   _//º\\_/     __ \\___\\             _/   _//           \\___/  \\/       /     ___/  __    __         \\  _   \\/ \\____/      _/    /    \\   \\ __  ");


            Console.Write(" /  __   º\\  /     \\ \\_    ");
            Console.ResetColor();
            Console.Write("__TT_____");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("/  __    \\          _/    //            _____");
            Console.ResetColor();
            Console.Write("_.-^-._");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ___/     /  __/   \\__/  \\      ___\\/_\\_/_     _/    //      /      \\___/  \\                           ");


            Console.Write("/__/_______º________\\__\\__");
            Console.ResetColor();
            Console.Write("/________/\\");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("_/__\\____\\__     _/_____/_____________/____");
            Console.ResetColor();
            Console.Write("/.-^-. /|");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("________/__/______");
            Console.ResetColor();
            Console.Write("______");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("_\\____/__________\\_ _/_____/_______/________");
            Console.ResetColor();
            Console.Write("_______");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\\____");


            Console.ResetColor();
            Console.WriteLine("  --    _T_T____    --   /_________\\/|       --   /|\\     --      --     -      |  _  |/ '-.    --        /_____/\\________                          ________/\\______\\\\");
            Console.WriteLine("       /_______/\\     -  |         | |   -       / | \\       --        -     .-'  |_|  '-.  \\      -    - \\_\\_\\_\\ \\_____  \\____                ____/  _____/ /_/_/_//");
            Console.WriteLine(" --   /  V E T \\ \\       |  []  [] | |          /  |  \\  -         --   -   /     |_|     \\  \\             \\_\\_\\_\\/____/\\___   \\______________/   ___/\\____\\/_/_/_//");
            Console.WriteLine("     /__________\\/| --   |         | |     --  /   |   \\     -   o         /               \\  /       --    \\_\\_\\_\\_\\__\\/__/\\____             ___/\\__\\/__/_/_/_/_//");
            Console.WriteLine("  -  |          | |      |   SHOP  | | -      /    |    \\       /|\\  --   /|     _____     |\\/|    --        \\_\\_\\_\\_\\__\\__\\/___/|           /___\\/__/__/_/_/_/_/|-");
            Console.WriteLine("     | [] ____  | |      |    ____ | |       /     |     \\     |(=)|===|===|    |==|==|    |  |        --    |                  ||===========|                  ||    --");
            Console.WriteLine("   - |    |. |  | /    - |    |. | | /  -   /      |      \\-   |`='|===|===|    |==|==|    | /    -  -       |  []  []  []  []  ||           |  []  []  []  []  ||  -");
            Console.WriteLine("     |____|__|__|/  -    |____|__|_|       /       |       \\         --    |____|==|==|____|/         ---    |__________________|/---|---|---|__________________|/   --");
            Console.WriteLine("_________________________________________ /        |        \\________________________________________________________________________________________________________________");
            Console.WriteLine("                                                                                                                                                                             ");
            Console.WriteLine("      _T_T____               _T_T____                                                                        _T_T____               _T_T____               _T_T____                    ");
            Console.WriteLine("__   /_______/\\  __   __  __/_______/\\ __  __  __  __   __   __   __   __   __    __    __   __   __   __   /_______/\\  __  __   __/_______/\\  __  __  __ /_______/\\  __  __         ");
            Console.WriteLine("    /    O   \\ \\           /    O   \\ \\                                                                    /    O   \\ \\           /    O   \\ \\           /    O   \\ \\              ");
            Console.WriteLine("   /__________\\/|         /__________\\/|                                                                  /__________\\/|         /__________\\/|         /__________\\/|        ");
            Console.WriteLine("___|          | |_________|          | |__________________________________________________________________|          | |_________|          | |_________|          | |_______     ");
            Console.WriteLine("   | []    [] | |      -  | []    [] | |  --         --              -              -             --      | []    [] | |         | []    [] | |   -     | []    [] | |  -          ");
            Console.WriteLine(" - |          | /  --     |          | /      -             --                --           -              |          | /  --     |          | /    --   |          | /    -       ");
            Console.WriteLine("   |__________|/      -   |__________|/    -       --            -       -           --           --      |__________|/      -   |__________|/  -       |__________|/  -   ");

        }

        
    }
}
