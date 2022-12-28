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
        public bool isSelected { get; set; }
        public bool isLootBox { get; set; }


        // Constructor
        public Card(int x, int y, Horse horse, bool isLootBox = false)
        {
            this.x = x;
            this.y = y;
            initialY = y;
            this.horse = horse;
            this.isLootBox = isLootBox;
        }


        // Methods
        public void Move(int x = 0)
        {
            this.x += x;
        }


        public void Draw()
        {
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.ResetColor();

            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.Write("| ");
            Console.ResetColor();
            Console.ForegroundColor = horse.GetRarityColor();

            if (!isLootBox)
            {
                string _name = ("[" + horse.name + "]").PadRight(12);
                Console.Write(_name);
                Console.ResetColor();
                string _price = (horse.price).ToString("C").PadLeft(10);
                Console.Write(_price);
            }
            else
            {
                string _name = ("[" + horse.name + "]").PadRight(22);
                Console.Write(_name);
                Console.ResetColor();
            }
            
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" |");
            Console.ResetColor();


            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.ResetColor();


            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.Write("|");
            Console.ResetColor();
            Console.Write("              ,,        ");
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|");
            Console.ResetColor();


            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("|");
            Console.ResetColor();
            Console.Write("             /(-\\       ");
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|");
            Console.ResetColor();
            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("|");
            Console.ResetColor();
            Console.Write("        ,---' /`-'      ");
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|");
            Console.ResetColor();
            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("|");
            Console.ResetColor();
            Console.Write("       / ( )__))        ");
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|");
            Console.ResetColor();
            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("|");
            Console.ResetColor();
            Console.Write("      /  //   \\\\        ");
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|");
            Console.ResetColor();
            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("|");
            Console.ResetColor();
            Console.Write("         ``    ``       ");
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|");
            Console.ResetColor();

            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.ResetColor();


            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("| ");
            Console.ResetColor();
            Console.Write("Energy:           ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("100%");
            Console.ResetColor();
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" |");
            Console.ResetColor();


            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.ResetColor();

            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.Write("| ");
            Console.ResetColor();
            Console.Write("Resistence:        ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _resistence = (horse.resistance).ToString().PadLeft(3);
            Console.Write(_resistence);
            Console.ResetColor();


            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" |");
            Console.ResetColor();

            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.Write("| ");
            Console.ResetColor();
            Console.Write("Speed:             ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _speed = (horse.speed).ToString().PadLeft(3);
            Console.Write(_speed);
            Console.ResetColor();
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" |");
            Console.ResetColor();
            Console.SetCursorPosition(x, y++);
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("| ");
            Console.ResetColor();
            Console.Write("Age:               ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _age = (horse.age).ToString().PadLeft(3);
            Console.Write(_age);
            Console.ResetColor();
            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" |");
            Console.ResetColor();


            if (isSelected) Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+------------------------+");
            Console.ResetColor();
            y = initialY;
        }
    }
}
