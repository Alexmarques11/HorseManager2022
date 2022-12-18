using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class IsRarityAttribute : Attribute
    {
        public ConsoleColor GetColor(Rarity rarity) => RarityExtensions.GetColor(rarity);
    }
}
