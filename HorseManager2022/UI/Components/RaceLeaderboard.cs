using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class RaceLeaderboard
    {
        // Properties
        private int x, y;
        private List<RacingTeam> teams;
        private Team playerTeam;
        
        // Constructor
        public RaceLeaderboard(int x, int y, Team playerTeam, List<RacingTeam> teams)
        {
            this.x = x;
            this.y = y;
            this.playerTeam = playerTeam;
            this.teams = teams;
        }

        
        // Methods
        public void Show()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+-------------------------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                         Leaderboard                         |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+-------------------------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("| Pos |        Horse        |        Jockey        |   Time   |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|-------------------------------------------------------------|");

            for (int i = 0; i < teams.Count; i++)
            {
                RacingTeam racingTeam = teams[i];
                string lapTime = Utils.GetElapsedTime(racingTeam.startTime, racingTeam.endTime).ToString(@"ss\.fff");

                if (racingTeam.team == playerTeam)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.SetCursorPosition(x, y++);
                Console.Write("| " + Utils.AlignLeft($"{i + 1}.", 4) + "|");
                Console.Write(Utils.AlignCenter($"{racingTeam.team.horseName}", 21) + "|");
                Console.Write(Utils.AlignCenter($"{racingTeam.team.jockeyName}", 22) + "|");
                Console.Write(Utils.AlignLeft($"  {lapTime}", 10) + "|");
                Console.ResetColor();

                if (i < teams.Count - 1)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.WriteLine("|     |                     |                      |          |");
                }
            }
            
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+-------------------------------------------------------------+");
        }
    }
}
