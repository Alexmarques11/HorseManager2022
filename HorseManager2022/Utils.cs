using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022
{
    static internal class Utils
    {
        static public string AlignCenter(string text, int width)
        {
            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }

        static public string AlignRight(string text, int width) => text.PadLeft(width);
        

        static public string AlignLeft(string text, int width) => text.PadRight(width);


        // Discount of 25%
        static public int GetDiscountedPrice(int price) => (int)(price * 0.75);

        
        // Increase of 25%
        static public int GetIncreasedPrice(int price) => (int)(price * 1.25);

    }
}
