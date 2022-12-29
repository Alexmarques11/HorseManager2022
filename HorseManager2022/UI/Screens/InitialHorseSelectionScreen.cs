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
    internal class InitialHorseSelectionScreen : Screen
    {
        // Constants
        private const int INITIAL_AFFINITY = 10;

        // Properties
        private readonly Arrow arrow;
        private readonly Card speedoCard, tornadoCard, hulkCard;
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
        public InitialHorseSelectionScreen(Screen nextScreen, GameManager? gameManager, Screen? previousScreen = null)
            : base(previousScreen)
        {
            arrow = new Arrow(35, -25, 2);

            Horse speedo = new("Speedo", 15, 100, 13, 0, 25, Rarity.Common);
            Horse tornado = new("Tornado", 20, 100, 12, 0, 20, Rarity.Common);
            Horse hulk = new("Hulk", 25, 100, 13, 0, 15, Rarity.Common);

            jockey = new(Rarity.Common, 15, 0);

            // Add options
            options.Add(new Option("Speedo", nextScreen, () => AddInitialTeam(gameManager, speedo))); ;
            options.Add(new Option("Tornado", nextScreen, () => AddInitialTeam(gameManager, tornado)));
            options.Add(new Option("Hulk", nextScreen, () => AddInitialTeam(gameManager, hulk)));

            // Add cards
            speedoCard = new(5, 13, speedo);
            tornadoCard = new(40, 13, tornado);
            hulkCard = new(75, 13, hulk);
        }
        
        private void AddInitialTeam(GameManager? gameManager, Horse horse)
        {
            gameManager?.Add<Horse, Player>(horse);
            gameManager?.Add<Jockey, Player>(jockey);
            gameManager?.Add<Team, Player>(new(horse, jockey, INITIAL_AFFINITY));
        }

        override public Screen? Show(GameManager? gameManager)
        {
            base.Show(gameManager);

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

            speedoCard.Draw();
            tornadoCard.Draw();
            hulkCard.Draw();
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
