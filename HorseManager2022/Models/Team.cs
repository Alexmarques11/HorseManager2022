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
        }*/

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
