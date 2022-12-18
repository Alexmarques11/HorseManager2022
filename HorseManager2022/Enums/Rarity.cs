using HorseManager2022.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Enums
{
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    static public class RarityExtensions
    {
        static public ConsoleColor GetColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.Common => ConsoleColor.White,
                Rarity.Rare => ConsoleColor.Blue,
                Rarity.Epic => ConsoleColor.DarkMagenta,
                Rarity.Legendary => ConsoleColor.Yellow,
                _ => ConsoleColor.White,
            };
        }
    }
}
