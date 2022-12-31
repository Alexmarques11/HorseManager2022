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
        public void Move()
        {
            int horseSpeed = team.horse.speed - (int)Math.Round(team.horse.speed * team.horse.age / 100f);
            int statAverage = (horseSpeed + team.jockey.handling + team.afinity) / 3;
            int speed = (int)Math.Round(statAverage / 15f);
            int randomOffset = GameManager.GetRandomInt(-2, 3);

            int distance = speed + randomOffset;
            if (distance < 0)
                distance = 0;

            x += distance;
        }

    }
}
