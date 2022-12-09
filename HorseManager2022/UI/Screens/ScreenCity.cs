﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class ScreenCity : ScreenWithTopbar
    {
        // Properties
        private Arrow arrow;

        public override int selectedPosition { 
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
        public ScreenCity(string title, Topbar topbar, Screen? previousScreen = null)
            : base(title, topbar, previousScreen)
        {
            arrow = new Arrow(22, -12, Topbar.TOPBAR_HEIGHT);
            menuMode = MenuMode.Down;
        }
        

        override public Screen? Show()
        {

            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();

                topbar.Draw(this);
                DrawCity();

                if (menuMode == MenuMode.Down)
                    arrow.Draw();

            });
            
            selectedOption.onEnter();
            return selectedOption.nextScreen;

        }
        
        
        public void DrawCity()
        {
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

    }
}
