using HorseManager2022.Enums;
using HorseManager2022.Models;
using HorseManager2022.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Screens
{
    internal class HorseSelectionScreen : Screen
    {
        // Properties
        private readonly Arrow arrow;
        private readonly Horse speedo, tornado, hulk;
        private readonly Jockey jockey;

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
        public HorseSelectionScreen(Screen nextScreen, GameManager? gameManager, Screen? previousScreen = null)
            : base(previousScreen)
        {
            arrow = new Arrow(36, -21, 2);

            speedo = new("Speedo", 10, 100, 13, 0, 20, Rarity.Common);
            tornado = new("Tornado", 15, 100, 12, 0, 15, Rarity.Common);
            hulk = new("Hulk", 20, 100, 13, 0, 10, Rarity.Common);

            jockey = new(Rarity.Common, 10, 0);

            // Add options
            options.Add(new Option("Speedo", nextScreen, () => AddInitialTeam(gameManager, speedo))); ;
            options.Add(new Option("Tornado", nextScreen, () => AddInitialTeam(gameManager, tornado)));
            options.Add(new Option("Hulk", nextScreen, () => AddInitialTeam(gameManager, hulk)));

        }
        
        private void AddInitialTeam(GameManager? gameManager, Horse horse)
        {
            gameManager?.Add(horse);
            gameManager?.Add(jockey);
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
            selectedOption?.onEnter?.Invoke();
            return selectedOption?.nextScreen;
        }

        private void DrawCards()
        {
            Console.WriteLine("Pick your starting horse!");
            Console.WriteLine();

            DrawCard(5, 10, speedo);
            DrawCard(40, 10, tornado);
            DrawCard(75, 10, hulk);
        }

        public void DrawCard(int x, int y, Horse horse)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("| ");
            Console.ForegroundColor = horse.GetRarityColor();
            string _name = ("[" + horse.name + "]").PadRight(12);
            Console.Write(_name);
            Console.ResetColor();
            string _price = (horse.price + "€").ToString().PadLeft(10);
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
            string _resistence = (horse.resistance).ToString().PadLeft(3);
            Console.Write(_resistence);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y + 13);
            Console.Write("| Speed:             ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _speed = (horse.speed).ToString().PadLeft(3);
            Console.Write(_speed);
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.SetCursorPosition(x, y + 14);
            Console.Write("| Age:               ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string _age = (horse.age).ToString().PadLeft(3);
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
