using HorseManager2022.Enums;
using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class RaceParticipants
    {
        // Properties
        private int x, y;
        private List<RacingTeam> teams;


        // Constructor
        public RaceParticipants(int x, int y, List<RacingTeam> teams)
        {
            this.x = x;
            this.y = y;
            this.teams = teams;
        }


        // Methods
        public void Show(bool cleanable = false)
        {
            if (cleanable)
                Console.Clear();
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------------------------------------------------------------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                                   Participants                                                   |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------------------------------------------------------------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|        Horse        |        Jockey        |   Age   |   Speed    |   Resistance   |   Handling   |   Affinity   |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|------------------------------------------------------------------------------------------------------------------|");

            for (int i = 0; i < teams.Count; i++)
            {
                RacingTeam racingTeam = teams[i];
                
                Console.SetCursorPosition(x, y++);
                Console.Write("|");
                Console.ForegroundColor = racingTeam.team.horse.GetRarityColor();
                Console.Write(Utils.AlignCenter($"{racingTeam.team.horseName}", 21));
                Console.ResetColor();
                Console.Write("|");
                Console.ForegroundColor = racingTeam.team.jockey.GetRarityColor();
                Console.Write(Utils.AlignCenter($"{racingTeam.team.jockeyName}", 22));
                Console.ResetColor();
                Console.Write("|");
                Console.Write(Utils.AlignCenter($"{racingTeam.team.horseAge}", 9) + "|");
                Console.Write(Utils.AlignCenter($"{racingTeam.team.horseSpeed}", 12) + "|");
                Console.Write(Utils.AlignCenter($"{racingTeam.team.horseResistance}", 16) + "|");
                Console.Write(Utils.AlignCenter($"{racingTeam.team.jockeyHandling}", 14) + "|");
                Console.WriteLine(Utils.AlignCenter($"{racingTeam.team.afinity}", 14) + "|");

                if (i < teams.Count - 1)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.WriteLine("|                     |                      |         |            |                |              |              |");
                }
            }

            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------------------------------------------------------------------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
