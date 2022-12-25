using HorseManager2022.Enums;
using HorseManager2022.Models;
using HorseManager2022.UI.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
        private List<RacingTeam> leaderboardList;
        private Team playerTeam;
        private RaceType raceType;
        public Racetrack racetrack;
        private Event? @event;
        public List<string> rewards, consequences; // Info that going to be displayed at the reward dialog

        private int dialogPositionX => racetrack.gameDistance / 2 - 10;
        private int dialogPositionY => participants.Count * 4 / 2 + 2;
        private bool isTraining => raceType == RaceType.Training;


        // Race Event Constructor
        public Race(Team playerTeam, List<Team> teams, Screen currentScreen, Event @event)
        {
            // Set properties
            this.currentScreen = currentScreen;
            raceType = RaceType.Event;
            isRaceStarted = raceType == RaceType.Training;
            rewards = new();
            consequences = new();

            // Set participants
            this.playerTeam = playerTeam;
            participants = new() { new(playerTeam) };
            participants.AddRange(teams.Select(team => new RacingTeam(team)));
            // Shuffle participants
            participants = participants.OrderBy(x => Guid.NewGuid()).ToList();
            leaderboardList = new();

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
            rewards = new();
            consequences = new();

            // Set participants
            this.playerTeam = playerTeam;
            participants = new() { new(playerTeam) };
            leaderboardList = new();

            // Set racetrack
            racetrack = new();
        }


        public void Start(GameManager? gameManager)
        {
            // Initial verifications
            if (gameManager == null)
                return;

            // Race loop
            while (!HaveAllHorsesFinished())
            {
                DrawRace();
                
                if (!isRaceStarted)
                    StartRace();
            }

            GetRewards(gameManager);
            if (isTraining) ShowRewardDialog(); else FinishRaceEvent();

            // Finish day if race event  --REMOVE THIS IN DEBUG MODE
            //if (!isTraining && isRace)
            //    gameManager?.currentDate.NextDay(gameManager);
        }


        private bool HaveAllHorsesFinished()
        {
            return participants.All(participant => participant.x >= racetrack.gameDistance);
        }

        
        // Race Start / Countdown / Music
        private void StartRace()
        {
            isRaceStarted = true;
            Audio.PlayRaceSong();
            DialogCounter dialogCounter = new(dialogPositionX, dialogPositionY);
            dialogCounter.Show();

            // Start team times
            foreach (RacingTeam team in participants)
                team.startTime = DateTime.Now;
        }


        private void DrawRace()
        {
            Console.Clear();
            DrawHeader();
            racetrack.Show();
            DrawHorses();
        }

        
        private void FinishRaceEvent()
        {
            // Show leaderboard
            RaceLeaderboard leaderboard = new(dialogPositionX, dialogPositionY, playerTeam, leaderboardList);
            leaderboard.Show();
            Audio.PlayRaceEndSong();
            Console.ReadKey();

            // Show rewards
            DrawRace();
            ShowRewardDialog();

            Audio.PlayTownSong();
        }


        private void GetRewards(GameManager gameManager)
        {
            // Get money reward and payment
            int moneyReward = @event?.GetReward(participants.Count) ?? 0;
            int entryCost = @event?.GetEntryCost() ?? 0;

            // Get resistence multiplier
            int resistance = playerTeam.horse.resistance;
            if (resistance == 0) resistance = 1;

            // Get energy loss percentage
            int energyLoss = (int)Math.Round(racetrack.realDistance * Horse.BASE_ENERGY_CONSUMED_PER_KM / resistance);
            if (energyLoss > 100) energyLoss = 100;

            // Update energy
            List<string> rewards = playerTeam.UpdateStatsAfterRace();
            playerTeam.horse.energy -= energyLoss;
            gameManager.Update<Team, Player>(playerTeam);

            // Update money (Only if not training)
            if (!isTraining)
            {
                if (@event?.type == EventType.Race)
                {
                    // 1º place wins moneyReward
                    if (leaderboardList[0].team == playerTeam)
                    {
                        gameManager.money += moneyReward;
                        this.rewards.Add(moneyReward + " €");
                    }
                    // 2º place keep entryCost
                    // Other positions lose entryCost
                    else if (leaderboardList[1].team != playerTeam)
                    {
                        gameManager.money -= entryCost;
                        consequences.Add(entryCost + " €");
                    }
                }
                else if (@event?.type == EventType.Demostration)
                {
                    // Win moneyReward proportional to the energy and resistence of the horse

                    moneyReward = moneyReward * (100 - playerTeam.horse.energy) / 100;
                    // moneyReward = (int)Math.Round(100.0 * (playerTeam.horse.energy + playerTeam.horse.resistance) / 200.0);
                    gameManager.money += moneyReward;
                    this.rewards.Add(moneyReward + " €");
                    
                    // Get horse & joquery average rarity diff - rar    4 - 4

                }
            }

            // Add rewards & consequences
            this.rewards.AddRange(rewards);
            consequences.Add(energyLoss + "% Energy");
        }


        private void ShowRewardDialog()
        {
            // Show reward dialog
            DialogRewards dialogRewards = new(
                x: dialogPositionX,
                y: dialogPositionY,
                message: $"You have completed the {raceType}.",
                dialogType: DialogType.Information,
                previousScreen: currentScreen,
                rewards: rewards,
                consequences: consequences
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
                {
                    team.x = racetrack.gameDistance;

                    if (!leaderboardList.Contains(team))
                    {
                        team.endTime = DateTime.Now;
                        leaderboardList.Add(team);
                    }
                }

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
