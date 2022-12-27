using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class Card
    {
        // Properties
        public int x { get; set; }
        public int y { get; set; }
        public int initialY { get; set; }
        public Horse horse { get; set; }


        // Constructor
        public Card(int x, int y, Horse horse)
        {
            this.x = x;
            this.y = y;
            initialY = y;
            this.horse = horse;
        }


        // Methods
        public void Move(int x = 0)
        {
            this.x += x;
        }


        public void Draw()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.Write("| ");
            Console.ForegroundColor = horse.GetRarityColor();
            string _name = ("[" + horse.rarity + "]").PadRight(12);
            Console.Write(_name);
            Console.ResetColor();
            string _price = (horse.price + "€").ToString().PadLeft(10);
            Console.WriteLine(_price + " |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|              ,,        |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|             /(-\\       |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|        ,---' /`-'      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|       / ( )__))        |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|      /  //   \\\\        |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|         ``    ``       |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.Write("| Energy:           ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("100%");
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.Write("| Resistence:        ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _resistence = (horse.resistance).ToString().PadLeft(3);
            Console.Write(_resistence);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y++);
            Console.Write("| Speed:             ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _speed = (horse.speed).ToString().PadLeft(3);
            Console.Write(_speed);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y++);
            Console.Write("| Age:               ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _age = (horse.age).ToString().PadLeft(3);
            Console.Write(_age);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            y = initialY;
        }
    }
}
