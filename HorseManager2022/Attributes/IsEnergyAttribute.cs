using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class IsEnergyAttribute : Attribute
    {
        public ConsoleColor GetColor(int energy)
        {
            if (energy < 33)
                return ConsoleColor.Red;
            else if (energy < 66)
                return ConsoleColor.Yellow;
            else
                return ConsoleColor.Green;
        }
    }
}
