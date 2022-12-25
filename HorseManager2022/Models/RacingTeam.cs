using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    internal class RacingTeam
    {
        // Properties
        public Team team { get; set; }
        public int x { get; set; }
        public DateTime startTime;
        public DateTime endTime;


        // Constructor
        public RacingTeam(Team team)
        {
            this.team = team;
            x = 1;
        }


        // Methods
        public TimeSpan GetLapTime() => endTime - startTime;
        
    }
}
