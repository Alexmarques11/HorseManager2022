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
        private const int AFFINITY_MAX = 100;
        private const float LOW_MULTIPLIER_CHANCE = 0.7f;
        private const float MEDIUM_MULTIPLIER_CHANCE = 0.25f;
        private const float IMPROVE_STAT_CHANCE = 0.2f;

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
        

        private int GetStatMultiplier()
        {
            if (GameManager.GetRandomDouble() < LOW_MULTIPLIER_CHANCE)
                return 1;
            else if (GameManager.GetRandomDouble() < MEDIUM_MULTIPLIER_CHANCE)
                return 2;
            else
                return 3;
        }


        // Methods
        public List<string> UpdateStatsAfterRace()
        {
            List<string> rewards = new();

            // Afinity always improves
            int afinity = 1, speed = 0, resistance = 0;

            // stats have a chance of improving
            if (GameManager.GetRandomDouble() < IMPROVE_STAT_CHANCE)
                resistance = 1;
            if (GameManager.GetRandomDouble() < IMPROVE_STAT_CHANCE)
                speed = 1;

            afinity *= GetStatMultiplier();
            resistance *= GetStatMultiplier();
            speed *= GetStatMultiplier();

            // stats can't be over its limit
            if (horse.resistance + resistance > horse.GetStatMaxValue())
                resistance = 100 - horse.resistance;
            if (horse.speed + speed > horse.GetStatMaxValue())
                speed = 100 - horse.speed;
            if (afinity + this.afinity > AFFINITY_MAX)
                afinity = 100 - this.afinity;

            // Apply the improvements
            this.afinity += afinity;
            horse.speed += speed;
            horse.resistance += resistance;

            // Add the rewards to the list
            if (afinity > 0) rewards.Add(afinity + " Afinity");
            if (speed > 0) rewards.Add(speed + " Speed");
            if (resistance > 0) rewards.Add(resistance + " Resistance");

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
            return difficulty switch
            {
                Difficulty.Easy => 2,
                Difficulty.Normal => GameManager.GetRandomInt(3, 5),
                Difficulty.Hard => GameManager.GetRandomInt(4, 6),
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
            int affinity = GameManager.GetRandomInt(minAffinity, maxAffinity + 1);

            return affinity;
        }


        public override string ToString()
        {
            return $"{horseName} - {jockeyName}";
        }
    }
}
