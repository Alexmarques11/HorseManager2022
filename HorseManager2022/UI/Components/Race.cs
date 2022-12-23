using HorseManager2022.Enums;
using HorseManager2022.Models;
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
        // Constants
        private readonly int DISTANCE = 72;

        // Properties
        private readonly Screen currentScreen;
        private readonly int participants;
        private readonly string title;
        private List<Team> teams;
        private bool isRaceStarted;
        private RaceType raceType;
        // private int prize;
        // private Rarity difficulty;


        // Constructor
        public Race(string title, List<Team> teams, Screen currentScreen, RaceType raceType = RaceType.Training)
        {
            this.title = title;
            this.teams = teams;
            this.raceType = raceType;
            participants = teams.Count;
            isRaceStarted = raceType == RaceType.Training;
        }

        public Race()
        {
            raceType = RaceType.Training;
            this.title = "default";
            this.teams = Team.GenerateRandomTeams(1);
            participants = teams.Count;
            isRaceStarted = raceType == RaceType.Training;
        }


        public void Start()
        {
            // Race loop
            int x = 1, y = 6;
            do
            {
                Console.Clear();

                DrawHeader();
                
                DrawRaceTrack();
                DrawHorses(ref x, y);

                if (!isRaceStarted)
                    StartRace();

            } while (x < DISTANCE);

            if (isTraining) FinishRaceTrain(); else FinishRaceEvent();
        }


        private bool isTraining => raceType == RaceType.Training;


        // Race Start / Countdown / Music
        private void StartRace()
        {
            isRaceStarted = true;
            Audio.PlayRaceSong();
            DialogCounter dialogCounter = new(20, 8);
            dialogCounter.Show();
        }

        
        private void FinishRaceEvent()
        {
            RaceLeaderboard leaderboard = new(85, 7);
            leaderboard.Show();
            Audio.PlayRaceEndSong();
            Console.ReadKey();
            Audio.PlayTownSong();
        }


        private void FinishRaceTrain()
        {
            DialogRewards dialogRewards = new(
                x: 20,
                y: 8,
                message: "You have completed the training.",
                dialogType: DialogType.Information,
                previousScreen: currentScreen,
                rewards: new() { 
                    "10 Resistance" 
                },
                consequences: new() { 
                    "20 Energy" 
                }
            );

            dialogRewards.Show();

        }
        

        private void DrawRaceTrack()
        {
            // Racetrack Start
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("================================================================================");

            // Racetrack
            for (int i = 0; i < participants; i++)
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
        }


        private void DrawHorses(ref int x, int y)
        {
            // Draw Horses
            Random random = new();
            for (int i = 0; i < participants; i++)
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
        }

        
        private void DrawHeader()
        {
            Console.WriteLine("+-------------------------------------------------------------------------------+");
            Console.WriteLine("|                                                                               |");
            Console.WriteLine("|                     [Corrida Comum] (10Km)  > Prémio 100€ <                   |");
            Console.WriteLine("|                                                                               |");
            Console.WriteLine("+-------------------------------------------------------------------------------+");
        }
    }
}
