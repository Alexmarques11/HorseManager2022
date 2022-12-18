using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class GameData
    {
        // Properties
        public int money;
        public Date? currentDate;
        public List<Horse> horses;
        public List<Event> events;
        public List<Jockey> joqueys;
        public List<Team> teams;
        // public List<Horse> shopHorses;

        public GameData()
        {
            money = 10;
            currentDate = new();
            horses = new();
            events = new();
            joqueys = new();
            teams = new();
            // shopHorses = new();
        }

    }
}
