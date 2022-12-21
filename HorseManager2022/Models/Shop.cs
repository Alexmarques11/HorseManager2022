using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Shop
    {
        // Properties
        public List<Horse> horses { get; set; }
        public List<Jockey> jockeys { get; set; }

        public Shop()
        {
            horses = new();
            jockeys = new();
        }
    }
}
