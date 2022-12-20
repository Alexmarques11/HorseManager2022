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

        // Constructor
        public RaceLeaderboard(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Methods
        public void Show()
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("+-------------------------------------------+");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|                Leaderboard                |");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("+-------------------------------------------+");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("| Pos |    Horse    |    Team    |  Pontos  |");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("|-------------------------------------------|");
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("|                                           |");

            Console.SetCursorPosition(x, y + 6);
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("  1º   ");
            Console.ResetColor();
            Console.Write("horse name    team name       26    |");
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("|                                           |");

            Console.SetCursorPosition(x, y + 8);
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("  2º   ");
            Console.ResetColor();
            Console.Write("horse name    team name       26    |");
            Console.SetCursorPosition(x, y + 9);
            Console.WriteLine("|                                           |");

            Console.SetCursorPosition(x, y + 10);
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("  3º   ");
            Console.ResetColor();
            Console.Write("horse name    team name       26    |");
            Console.SetCursorPosition(x, y + 11);
            Console.WriteLine("|                                           |");

            Console.SetCursorPosition(x, y + 12);
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  4º   ");
            Console.ResetColor();
            Console.Write("horse name    team name       26    |");
            Console.SetCursorPosition(x, y + 13);
            Console.WriteLine("|                                           |");

            Console.SetCursorPosition(x, y + 14);
            Console.WriteLine("+-------------------------------------------+");
        }
    }
}
