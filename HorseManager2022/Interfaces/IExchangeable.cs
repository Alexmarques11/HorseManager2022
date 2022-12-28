using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Interfaces
{
    internal interface IExchangeable
    {
        // Properties
        string name { get; set; }
        Rarity rarity { get; set; }
        int price { get; set; }
    }
}
