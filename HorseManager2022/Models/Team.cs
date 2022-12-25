using HorseManager2022.Attributes;
using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Team
    {
        // Constants
        private const int IMPROVE_STAT_CHANCE = 20;

        // Properties
        public Horse horse;
        public Jockey jockey;

        [DisplayName("Horse")]
        [Padding(22)]
        public string horseName { get => horse.name; }

        [DisplayName("Energy")]
        [IsPercentage]
        [IsEnergy]
        public int horseEnergy { get => horse.energy; }


        [DisplayName("Resistance")]
        [Color(ConsoleColor.DarkGray)]
        public int horseResistance { get => horse.resistance; }

        [DisplayName("Speed")]
        [Color(ConsoleColor.DarkGray)]
        public int horseSpeed { get => horse.speed; }

        [DisplayName("Age")]
        [Padding(7)]
        public int horseAge { get => horse.age; }


        [DisplayName("Rarity")]
        [Padding(12)]
        [IsRarity]
        public Rarity horseRarity { get => horse.rarity; }

        [DisplayName("Jockey")]
        [Padding(22)]
        public string jockeyName { get => jockey.name; }
        
        [DisplayName("Rarity")]
        [Padding(12)]
        [IsRarity]
        public Rarity jockeyRarity { get => jockey.rarity; }

        [DisplayName("Handling")]
        [Color(ConsoleColor.DarkGray)]
        public int jockeyHandling { get => jockey.handling; }

        /*
        [DisplayName("Average Rarity")]
        [IsRarity]
        public Rarity averageRarity
        {
            get
            {
                int rarity = (int)horse.rarity + (int)jockey.rarity;
                rarity /= 2;
                return (Rarity)rarity;
            }
        }
        
        
        [DisplayName("Overall")]
        [Color(ConsoleColor.DarkGray)]
        public int overall
        {
            get
            {
                int speed = horse.speed;
                int resistance = horse.resistance;
                int handling = jockey.handling;
                int affinity = afinity;
                int age = horse.age;

                int overall = (speed + resistance + handling + affinity) / 4;

                // Apply the age multiplier
                return (int)Math.Round(overall * (1.0 - age / 100.0));
            }
        }

         */

        [DisplayName("Afinity")]
        [Padding(11)]
        [Color(ConsoleColor.DarkGray)]
        public int afinity { get; set; }

        
        // Constructor
        public Team(Horse horse, Jockey jockey, int afinity)
        {
            this.horse = horse;
            this.jockey = jockey;
            this.afinity = afinity;
        }


        // Methods
        public List<string> UpdateStatsAfterRace()
        {
            Random random = new();
            List<string> rewards = new();

            // Afinity always improves
            int afinity = 1, speed = 0, resistance = 0, handling = 0;

            // stats have a chance of improving
            if (random.NextDouble() < IMPROVE_STAT_CHANCE)
                afinity = 1;
            if (random.NextDouble() < IMPROVE_STAT_CHANCE)
                speed = 1;
            if (random.NextDouble() < IMPROVE_STAT_CHANCE)
                handling = 1;

            // Stats can improve by a random amount between 1 and 3
            afinity *= random.Next(1, 4);
            speed *= random.Next(1, 4);
            resistance *= random.Next(1, 4);
            handling *= random.Next(1, 4);

            // Apply the improvements
            this.afinity += afinity;
            horse.speed += speed;
            horse.resistance += resistance;
            jockey.handling += handling;

            // Add the rewards to the list
            rewards.Add(afinity + " Afinity");
            if (speed > 0) rewards.Add(speed + " Speed");
            if (resistance > 0) rewards.Add(resistance + " Resistance");
            if (handling > 0) rewards.Add(handling + " Handling");

            return rewards;
        }


        static public List<Team> GenerateEventTeams(Event @event)
        {
            Difficulty difficulty = @event.difficulty ?? Difficulty.Easy;
            Rarity horseRarity = RarityExtensions.GetRandomRarityByDifficulty(difficulty);
            Rarity joqueyRarity = RarityExtensions.GetRandomRarityByDifficulty(difficulty);
            int amount = GetEventTeamAmount(difficulty);
            int affinity = GetRandomAffinityByRarity(horseRarity, joqueyRarity);

            List<Team> teams = new();
            for (int i = 0; i < amount; i++)
                teams.Add(new Team(new(horseRarity), new(joqueyRarity), affinity));

            return teams;
        }


        // Set the range of possible teams based on the event rarity
        static private int GetEventTeamAmount(Difficulty difficulty)
        {
            Random random = new();
            return difficulty switch
            {
                Difficulty.Easy => 2,
                Difficulty.Normal => random.Next(3, 5),
                Difficulty.Hard => random.Next(4, 6),
                Difficulty.Extreme => 6,
                _ => 0,
            };
        }


        static private int GetRandomAffinityByRarity(Rarity horseRarity, Rarity jockeyRarity)
        {
            // Create a lookup table for the min and max affinities for each combination of rarities
            var affinityRangeLookup = new Dictionary<Rarity, (int, int)>
            {
                { Rarity.Common, (5, 20) },
                { Rarity.Rare, (15, 40) },
                { Rarity.Epic, (40, 70) },
                { Rarity.Legendary, (70, 100) },
            };

            // Look up the min and max affinities for the given rarities
            Rarity avgRarity = RarityExtensions.GetAverageRarity(new Rarity[] { horseRarity, jockeyRarity });
            var affinityRange = affinityRangeLookup[avgRarity];
            int minAffinity = affinityRange.Item1;
            int maxAffinity = affinityRange.Item2;

            // Generate a random affinity within the determined range
            Random random = new();
            int affinity = random.Next(minAffinity, maxAffinity + 1);

            return affinity;
        }
    }
}
