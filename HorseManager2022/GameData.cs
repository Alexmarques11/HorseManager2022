using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022
{
    [Serializable]
    internal class GameData
    {
        // Properties
        public int money;
        public Date? currentDate;
        public List<Horse> horses;
        public List<Event> events;

        public GameData()
        {
            this.money = 10;
            this.currentDate = new();
            this.horses = new();
            this.events = new();
        }
        
    }
}
