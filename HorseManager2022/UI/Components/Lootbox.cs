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
        private const int DURATION = 300;
        private const int POSITION_Y = 10;
        public static readonly int LOOTBOX_PRICE = 1000;

        // Properties
        private List<Card> cards { get; set; }
        private int movementCount { get; set; }
        private int delay = 40;


        // Constructor
        public Lootbox()
        {
            cards = new List<Card>();

            for (int i = 0; i < CARD_QUANTITY; i++)
                AddCard();
        }

        
        // Methods
        public void AddCard()
        {
            cards.Add(new Card(GetStartingPosition(), POSITION_Y, new(true), true));
            // cards[^1].horse.price = (int)cards[^1].horse.rarity * 250;
        }


        public void RemoveFirstCard() => cards.RemoveAt(0);
        

        public int GetStartingPosition()
        {
            int position = 10;

            if (cards.Count > 0)
                position = cards.Last().x + 30;

            return position;
        }

        
        public Horse Open()
        {
            Arrow arrow = new(0, 52, 1);

            while (movementCount < DURATION)
            {
                Console.Clear();

                arrow.Draw();

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

                Thread.Sleep(delay);
                movementCount += 5;

                if (movementCount % 10 == 0)
                    delay += 10;
            }

            Console.ReadKey();
            Console.Clear();

            // Remove all cards except the second one
            Card selectedCard = cards[1];
            selectedCard.isSelected = true;
            cards.Clear();
            cards.Add(selectedCard);
            selectedCard.Draw();
            
            Console.ReadKey();

            return selectedCard.horse;
        }
    }
}
