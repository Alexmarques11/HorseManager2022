using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Player
    {
        // Properties
        public List<Horse> horses { get; set; }
        public List<Event> events { get; set; }
        public List<Jockey> jockeys { get; set; }
        public List<Team> teams { get; set; }

        public Player()
        {
            horses = new();
            events = new();
            jockeys = new();
            teams = new();
        }
    }
}
