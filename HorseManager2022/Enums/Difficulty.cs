using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Enums
{
    public enum Difficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        Extreme = 3
    }

    static public class DifficultyExtensions
    {
        static public ConsoleColor GetColor(Difficulty? difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => ConsoleColor.Green,
                Difficulty.Normal => ConsoleColor.DarkYellow,
                Difficulty.Hard => ConsoleColor.Red,
                Difficulty.Extreme => ConsoleColor.DarkMagenta,
                _ => ConsoleColor.Gray,
            };
        }

        
        static public Difficulty GetRandomDifficulty()
        {
            // Create a lookup table for the probability of each difficulty level
            var difficultyProbabilityLookup = new Dictionary<Difficulty, int>
            {
                { Difficulty.Easy, 40 },
                { Difficulty.Normal, 30 },
                { Difficulty.Hard, 20 },
                { Difficulty.Extreme, 10 },
            };

            // Generate a random number between 0 and 100 (inclusive)
            int randomNumber = GameManager.GetRandomInt(0, 101);

            // Determine the difficulty level based on the probability in the lookup table
            int cumulativeProbability = 0;
            Difficulty difficulty = Difficulty.Easy;
            foreach (var kvp in difficultyProbabilityLookup)
            {
                cumulativeProbability += kvp.Value;
                if (randomNumber < cumulativeProbability)
                {
                    difficulty = kvp.Key;
                    break;
                }
            }

            return difficulty;
        }
    }
}
