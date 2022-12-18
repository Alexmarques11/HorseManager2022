using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class IsPercentageAttribute : Attribute
    {
    }
}
