using HorseManager2022.UI.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class Race
    {

        public Race()
        {
        }

        public void Start()
        {

            const int HORSES = 4;

            // Race start loop
            int x = 1;
            bool isRaceStarted = false;
            do
            {
                int y = 6;

                Console.Clear();

                // LeaderBoard
                Console.WriteLine("+-------------------------------------------------------------------------------+");
                Console.WriteLine("|                                                                               |");
                Console.WriteLine("|                     [Corrida Comum] (10Km)  > Prémio 100€ <                   |");
                Console.WriteLine("|                                                                               |");
                Console.WriteLine("+-------------------------------------------------------------------------------+");
                Console.WriteLine();
                Console.WriteLine();

                // Racetrack Start
                Console.WriteLine("================================================================================");

                // Racetrack
                for (int i = 0; i < HORSES; i++)
                {
                    // Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("      ~        ~      ~                            ~                       ~    ");
                    Console.WriteLine("  ~        ~                   ~          ~               ~      ~              ");
                    Console.WriteLine("                  ~     ~           ~            ~                    ~         ");
                    Console.WriteLine("         ~                    ~            ~            ~      ~             ~  ");
                    Console.WriteLine("   ~            ~      ~             ~              ~                  ~        ");
                    // Console.WriteLine("           ~                   ~              ~               ~                 ");
                    Console.WriteLine("================================================================================");
                }

                // Draw Horses
                Random random = new();
                for (int i = 0; i < HORSES; i++)
                {
                    Console.SetCursorPosition(x + 8, y);
                    Console.Write(",,");
                    Console.SetCursorPosition(x + 7, y + 1);
                    Console.Write("/(-\\");
                    Console.SetCursorPosition(x + 2, y + 2);
                    Console.Write(",---' /`-'");
                    Console.SetCursorPosition(x + 1, y + 3);
                    Console.Write("/");
                    Console.SetCursorPosition(x + 3, y + 3);
                    Console.Write("( )__))");
                    Console.SetCursorPosition(x, y + 4);
                    Console.Write("/");
                    Console.SetCursorPosition(x + 3, y + 4);
                    Console.Write("//");
                    Console.SetCursorPosition(x + 8, y + 4);
                    Console.Write("\\\\");
                    Console.SetCursorPosition(x + 3, y + 5);
                    Console.Write("``");
                    Console.SetCursorPosition(x + 9, y + 5);
                    Console.Write("``");
                    y += 6;
                }
                x += 3;
                Thread.Sleep(120);

                // Race Start / Countdown / Music
                if (!isRaceStarted)
                {
                    isRaceStarted = true;
                    Audio.PlayRaceSong();
                    DialogCounter dialogCounter = new(20, 8);
                    dialogCounter.Show();
                }

            } while (x < 72);

            RaceLeaderboard leaderboard = new(85, 7);
            leaderboard.Show();
            Audio.PlayRaceEndSong();
            Console.ReadKey();
            Audio.PlayTownSong();

        }
    }
}
