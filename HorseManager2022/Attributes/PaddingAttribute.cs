using System.ComponentModel;

namespace HorseManager2022.Attributes 
{
    
    [AttributeUsage(AttributeTargets.Property)]
    internal class PaddingAttribute : Attribute
    {
        public int value { get; set; }

        public PaddingAttribute(int value)
        {
            this.value = value;
        }
    }
    
}
