using HorseManager2022.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class Lootbox
    {
        // Constants
        private const int CARD_QUANTITY = 4;
        private const int DELAY = 120;
        private const int DURATION = 300;
        private const int POSITION_Y = 10;
        
        // Properties
        private DateTime startTime { get; set; }
        private List<Card> cards { get; set; }
        private int movementCount { get; set; }


        // Constructor
        public Lootbox()
        {
            startTime = DateTime.Now;
            cards = new List<Card>();

            for (int i = 0; i < CARD_QUANTITY; i++)
                AddCard();
        }

        
        // Methods
        public void AddCard() => cards.Add(new Card(GetStartingPosition(), POSITION_Y, new()));


        public void RemoveFirstCard() => cards.RemoveAt(0);
        

        public int GetStartingPosition()
        {
            int position = 10;

            if (cards.Count > 0)
                position = cards.Last().x + 30;

            return position;
        }

        
        public void Open()
        {

            while (movementCount < DURATION)
            {
                Console.Clear();

                for (int i = cards.Count - 1; i >= 0; i--)
                {
                    Card card = cards[i];

                    card.Move(-5);

                    if (card.x >= 5)
                        card.Draw();
                    else
                    {
                        RemoveFirstCard();
                        AddCard();
                    }
                }

                Thread.Sleep(DELAY);
                movementCount += 5;
            }


            Console.ReadKey();
        }
    }
}
