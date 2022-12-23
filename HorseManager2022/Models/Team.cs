using HorseManager2022.Attributes;
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

        [DisplayName("Jockey")]
        [Padding(22)]
        public string jockeyName { get => jockey.name; }

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
