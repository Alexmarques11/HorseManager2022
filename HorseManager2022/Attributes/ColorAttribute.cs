using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ColorAttribute : Attribute
    {
        public ConsoleColor color { get; set; }

        public ColorAttribute(ConsoleColor color)
        {
            this.color = color;
        }
    }
}
