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
        public static List<Team> GenerateRandomTeams(int amount = 4)
        {
            List<Team> teams = new();
            for (int i = 0; i < amount; i++)
                teams.Add(new Team(new(), new(), 0));

            return teams;
        }
        
    }
}
