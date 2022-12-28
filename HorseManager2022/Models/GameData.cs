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
        public int money { get; set; }
        public Date currentDate { get; set; }
        public Player player { get; set; }
        public Shop shop { get; set; }
        public Vet vet { get; set; }

        public GameData()
        {
            money = 10;
            currentDate = new();
            player = new();
            shop = new();
            vet = new();
        }

    }
}
