using System;
using System.Collections.Generic;
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
        public int afinity;

        // Constructor
        public Team(Horse horse, Jockey jockey, int afinity)
        {
            this.horse = horse;
            this.jockey = jockey;
            this.afinity = afinity;
        }

        // Methods
    }
}
