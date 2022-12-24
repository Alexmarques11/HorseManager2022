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
        private List<Team> teams;

        // Constructor
        public RaceLeaderboard(int x, int y, List<Team> teams)
        {
            this.x = x;
            this.y = y;
            this.teams = teams;
        }

        // Methods
        public void Show()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+-------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                Leaderboard                |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+-------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("| Pos |    Horse    |    Team    |  Pontos  |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|-------------------------------------------|");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                           |");

            for (int i = 0; i < teams.Count; i++)
            {
                Team team = teams[i];
                
                Console.SetCursorPosition(x, y++);
                // Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("|" + Utils.AlignLeft($"{i + 1}", 5) + "|");
                Console.ResetColor();
                Console.Write(Utils.AlignLeft($"{team.horseName}", 15) + "|");
                Console.Write(Utils.AlignLeft($"{team.jockeyName}", 15) + "|");
                Console.Write(Utils.AlignLeft($"{00}", 5) + "|");
                Console.SetCursorPosition(x, y++);
                Console.WriteLine("|                                           |");
            }
            
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+-------------------------------------------+");
        }
    }
}
