using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Vet
    {
        // Properties
        public string name;
        public int level;
        public int upgradeCost;
        public int proficiency;

        // Constructors
        public Vet()
        {
            name = "Doctor Gustavo Fring";
            level = 1;
            upgradeCost = 40;
            proficiency = 5;
        }

        public Vet(string name, int level, int upgradeCost, int proficiency)
        {
            this.name = name;
            this.level = level;
            this.upgradeCost = upgradeCost;
            this.proficiency = proficiency;
        }

        //Metodo
        public void Upgrade()
        {
            level++;
            upgradeCost = (int)(upgradeCost * 1.3);
        }
    }
}
