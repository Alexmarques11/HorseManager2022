using HorseManager2022.Enums;
using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Screens
{
    internal class HorseSelectionScreen : Screen
    {
        // Properties
        private readonly Arrow arrow;

        public override int selectedPosition
        {
            get
            {
                return base.selectedPosition;
            }
            set
            {
                if (value > options.Count)
                    value = options.Count;

                arrow.selectedPosition = value;
                base.selectedPosition = value;
            }
        }
        
        // Constructor
        public HorseSelectionScreen(Screen nextScreen, Screen? previousScreen = null)
            : base(previousScreen)
        {
            arrow = new Arrow(36, -21, 2);

            // Add options
            options.Add(new Option("Speedo", nextScreen, () => { }));
            options.Add(new Option("Tornado", nextScreen, () => { }));
            options.Add(new Option("Hulk", nextScreen, () => { }));

        }

        override public Screen? Show(GameManager? gameManager)
        {
            // Reset positions
            selectedPosition = 0;

            // Wait for option
            Option? selectedOption = WaitForOption(() =>
            {
                Console.Clear();

                DrawCards();
                
                arrow.Draw();

            });

            // Return next screen
            selectedOption.onEnter();
            return selectedOption?.nextScreen;
        }

        private void DrawCards()
        {
            Console.WriteLine("Pick your starting horse!");
            Console.WriteLine();

            DrawCard(5, 10, 0, Rarity.Common, "Speedo", 10, 20, 13);
            DrawCard(40, 10, 0, Rarity.Common, "Tornado", 15, 15, 12);
            DrawCard(75, 10, 0, Rarity.Common, "Hulk", 20, 10, 13);
        }

        public void DrawCard(int x, int y, int price, Rarity rarity, string name, int resistence, int speed, int age)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("| ");
            Console.ForegroundColor = RarityExtensions.ToColor(rarity);
            string _name = ("[" + name + "]").PadRight(12);
            Console.Write(_name);
            Console.ResetColor();
            string _price = (price + "€").ToString().PadLeft(10);
            Console.WriteLine(_price + " |");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|              ,,        |");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("|             /(-\\       |");
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("|        ,---' /`-'      |");
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("|       / ( )__))        |");
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("|      /  //   \\\\        |");
            Console.SetCursorPosition(x, y + 8);
            Console.WriteLine("|         ``    ``       |");
            Console.SetCursorPosition(x, y + 9);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y + 10);
            Console.Write("| Energy:           ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("100%");
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y + 11);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y + 12);
            Console.Write("| Resistence:        ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _resistence = (resistence).ToString().PadLeft(3);
            Console.Write(_resistence);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y + 13);
            Console.Write("| Speed:             ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _speed = (speed).ToString().PadLeft(3);
            Console.Write(_speed);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y + 14);
            Console.Write("| Age:               ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _age = (age).ToString().PadLeft(3);
            Console.Write(_age);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y + 15);
            Console.WriteLine("+------------------------+");
        }

        override public void SelectLeft()
        {
            if (this.selectedPosition > 0)
                this.selectedPosition--;
            else
            {
                this.selectedPosition = this.options.Count - 1;
            }
        }


        override public void SelectRight()
        {
            if (this.selectedPosition < this.options.Count - 1)
                this.selectedPosition++;
            else
                this.selectedPosition = 0;
        }


        override public void SelectUp() { }


        override public void SelectDown() { }


        override public Option? SelectEnter()
        {
            if (this.selectedPosition == this.options.Count)
            {
                return Option.GetBackOption(this.previousScreen);
            }
            else
                return this.options[this.selectedPosition];
        }
    }
}
