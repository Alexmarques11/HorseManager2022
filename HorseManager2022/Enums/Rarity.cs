using HorseManager2022.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Enums
{
    public enum Rarity
    {
        Common = 0,
        Rare = 1,
        Epic = 2,
        Legendary = 3
    }

    static public class RarityExtensions
    {
        static public ConsoleColor GetColor(Rarity? rarity)
        {
            return rarity switch
            {
                Rarity.Common => ConsoleColor.White,
                Rarity.Rare => ConsoleColor.Blue,
                Rarity.Epic => ConsoleColor.DarkMagenta,
                Rarity.Legendary => ConsoleColor.Yellow,
                _ => ConsoleColor.Gray,
            };
        }

        static public Rarity GetRandomRarity()
        {
            // Create a lookup table for the probability of each rarity
            var rarityProbabilityLookup = new Dictionary<Rarity, int>
            {
                { Rarity.Common, 75 },
                { Rarity.Rare, 20 },
                { Rarity.Epic, 4 },
                { Rarity.Legendary, 1 },
            };

            // Generate a random number between 0 and 100 (inclusive)
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 101);

            // Determine the rarity based on the probability in the lookup table
            int cumulativeProbability = 0;
            Rarity rarity = Rarity.Common;
            foreach (var kvp in rarityProbabilityLookup)
            {
                cumulativeProbability += kvp.Value;
                if (randomNumber < cumulativeProbability)
                {
                    rarity = kvp.Key;
                    break;
                }
            }

            return rarity;
        }

        static public Rarity GetRandomRarityByDifficulty(Difficulty? difficulty)
        {
            Random random = new();
            Rarity rarity = Rarity.Common;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    rarity = (Rarity)random.Next(0, 2);
                    break;
                case Difficulty.Normal:
                    rarity = (Rarity)random.Next(0, 3);
                    break;
                case Difficulty.Hard:
                    rarity = (Rarity)random.Next(1, 4);
                    break;
                case Difficulty.Extreme:
                    rarity = Rarity.Legendary;
                    break;
            }

            return rarity;
        }

        static public Rarity GetAverageRarity(Rarity[] rarities)
        {
            if (rarities.Length == 0)
                return Rarity.Common;

            int totalRarity = 0;
            foreach (Rarity rarity in rarities)
                totalRarity += (int)rarity;

            return (Rarity)(totalRarity / rarities.Length);
        }
    }
}
