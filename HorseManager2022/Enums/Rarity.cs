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
        Legendary = 3,
        Special = 4
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
                Rarity.Special => ConsoleColor.Red,
                _ => ConsoleColor.Gray,
            };
        }

        static public Rarity GetRandomRarity(bool isLootBox = false)
        {
            // Create a lookup table for the probability of each rarity
            var rarityProbabilityLookup = !isLootBox ? new Dictionary<Rarity, int>
            {
                { Rarity.Common, 70 },
                { Rarity.Rare, 20 },
                { Rarity.Epic, 7 },
                { Rarity.Legendary, 3 },
                { Rarity.Special, 0 },
            } : new Dictionary<Rarity, int>
            {
                { Rarity.Common, 45 },
                { Rarity.Rare, 30 },
                { Rarity.Epic, 13 },
                { Rarity.Legendary, 8 },
                { Rarity.Special, 4 },
            };

            // Generate a random number between 0 and 100 (inclusive)
            int randomNumber = GameManager.GetRandomInt(0, 101);

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
            Rarity rarity = Rarity.Common;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    // 20% chance of getting a rare, 80% chance of getting a common
                    rarity = GameManager.GetRandomInt(0, 101) < 20 ? Rarity.Rare : Rarity.Common;
                    break;
                case Difficulty.Normal:
                    // 20% chance of getting a epic, 80% chance of getting a rare
                    rarity = GameManager.GetRandomInt(0, 101) < 20 ? Rarity.Epic : Rarity.Rare;
                    break;
                case Difficulty.Hard:
                    // 20% chance of getting a Legendary, 80% chance of getting a Epic
                    rarity = GameManager.GetRandomInt(0, 101) < 20 ? Rarity.Legendary : Rarity.Epic;
                    break;
                case Difficulty.Extreme:
                    // 20% chance of getting a special, 80% chance of getting a legendary
                    rarity = GameManager.GetRandomInt(0, 101) < 20 ? Rarity.Special : Rarity.Legendary;
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
