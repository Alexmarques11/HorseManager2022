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
        private const int HEADER_LENGTH = 85;

        // Properties
        private readonly Screen currentScreen;
        private bool isRaceStarted;
        private List<RacingTeam> participants;
        private Team playerTeam;
        private RaceType raceType;
        private Racetrack racetrack;
        private Event? @event;


        // Race Event Constructor
        public Race(Team playerTeam, List<Team> teams, Screen currentScreen, Event @event)
        {
            // Set properties
            this.currentScreen = currentScreen;
            raceType = RaceType.Event;
            isRaceStarted = raceType == RaceType.Training;

            // Set participants
            this.playerTeam = playerTeam;
            participants = new() { new(playerTeam) };
            participants.AddRange(teams.Select(team => new RacingTeam(team)));

            // Set racetrack
            this.@event = @event;
            racetrack = new(@event.difficulty, participants.Count);
        }

        // Race Training Constructor
        public Race(Team playerTeam, Screen currentScreen)
        {
            // Set properties
            this.currentScreen = currentScreen;
            raceType = RaceType.Training;
            isRaceStarted = raceType == RaceType.Training;

            // Set participants
            this.playerTeam = playerTeam;
            participants = new() { new(playerTeam) };

            // Set racetrack
            racetrack = new();
        }


        public void Start(GameManager? gameManager)
        {
            // Race loop
            while (!HaveAllHorsesFinished())
            {
                Console.Clear();

                DrawHeader();

                racetrack.Show();
                
                DrawHorses();

                if (!isRaceStarted)
                    StartRace();
            } 

            if (isTraining) FinishRaceTraining(); else FinishRaceEvent();

            // Finish day if event  --REMOVE THIS IN DEBUG MODE
            //if (!isTraining)
            //    gameManager?.currentDate.NextDay(gameManager);
        }


        private bool HaveAllHorsesFinished()
        {
            return participants.All(participant => participant.x >= racetrack.gameDistance);
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
            List<Team> teams = participants.Select(participant => participant.team).ToList();
            RaceLeaderboard leaderboard = new(85, 7, teams);
            leaderboard.Show();
            Audio.PlayRaceEndSong();
            Console.ReadKey();
            Audio.PlayTownSong();
        }


        private void FinishRaceTraining()
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


        private void DrawHorses()
        {
            // Variables
            int y = 6;
            Random random = new();
            
            // Draw Horses
            foreach (RacingTeam team in participants)
            {
                // Highlight Player Team
                if (team.team == playerTeam)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                
                int x = team.x;
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
                team.x += random.Next(1, 4);

                if (team.x >= racetrack.gameDistance)
                    team.x = racetrack.gameDistance;

                // Reset Color
                Console.ResetColor();
            }
            Thread.Sleep(120);
        }

        
        private void DrawHeader()
        {
            int reward = @event?.GetReward(participants.Count) ?? 0;
            string title;
            
            if (isTraining)
                title = Utils.AlignCenter($"[Training {playerTeam.horseName} & {playerTeam.jockeyName}] ({racetrack.realDistance}Km)", HEADER_LENGTH);
            else
                title = Utils.AlignCenter($"[{@event?.difficulty} {@event?.type}] ({racetrack.realDistance}Km) > {reward},00 € Reward <", HEADER_LENGTH);

            Console.WriteLine("+" + new string('-', HEADER_LENGTH) + "+");
            Console.WriteLine("|" + new string(' ', HEADER_LENGTH) + "|");
            Console.Write("|");
            Console.Write(title);
            Console.WriteLine("|");
            Console.WriteLine("|" + new string(' ', HEADER_LENGTH) + "|");
            Console.WriteLine("+" + new string('-', HEADER_LENGTH) + "+");
        }

        
        
    }
}
