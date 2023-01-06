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
        public int level;
        public int proficiency;
        public int upgradeCost
        {
            get
            {
                double temp = (level + 2) * 0.5;
                return (int)Math.Round(25 * Math.Pow(temp, 2) - 25 * temp);
            }
        }


        // Constructors
        public Vet()
        {
            level = 1;
            proficiency = 0;
        }
        

        public Vet(int level, int proficiency)
        {
            this.level = level;
            this.proficiency = proficiency;
        }

        
        //Metodo
        public bool Upgrade(GameManager gameManager)
        {
            if (gameManager.money < upgradeCost || proficiency >= 50)
                return false;

            gameManager.money -= upgradeCost;
            level++;
            proficiency += GetNextLevelProficiency();

            gameManager.SaveChanges();
            return true;
        }
        

        public bool IsProficiencyAtMax() => proficiency >= 50;
        

        public int GetNextLevelProficiency() => (proficiency < 30) ? 2 : 1;


        public override string ToString() => $"Level: {level} - Proficiency: {proficiency}";
    }
}
