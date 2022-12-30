using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Components
{
    internal class Arrow
    {
        // Properties
        private int offsetX { get; set; }
        private int offsetY { get; set; }
        public int selectedPosition { get; set; }
        private int margin { get; set; }
        private List<(int, int)> customPositions { get; set; }

        // Constructor
        public Arrow(int margin, int offsetX = 0, int offsetY = 0)
        {
            this.margin = margin;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            selectedPosition = 0;
            customPositions = new();
        }


        public Arrow(List<(int, int)> customPositions, int offsetX = 0, int offsetY = 0, int margin = 0)
        {
            this.margin = margin;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.customPositions = customPositions;
            selectedPosition = 0;
        }


        // Methods
        public void Draw()
        {
            int x = (selectedPosition + 1) * margin + offsetX;
            int y = offsetY;

            if (customPositions.Count > 0 && ScreenCity.SCREEN_SIZE_MINIMUM_CITY < Console.WindowWidth) 
            {
                x = customPositions[selectedPosition].Item1;
                y = customPositions[selectedPosition].Item2;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.SetCursorPosition(x + 7, y++);
            Console.WriteLine(" __ ");
            Console.SetCursorPosition(x + 6, y++);
            Console.WriteLine(" /_/| ");
            Console.SetCursorPosition(x + 6, y++);
            Console.WriteLine(" | || ");
            Console.SetCursorPosition(x + 3, y++);
            Console.WriteLine(" ___| ||____ ");
            Console.SetCursorPosition(x + 2, y++);
            Console.WriteLine(" /___|_|/___// ");
            Console.SetCursorPosition(x + 2, y++);
            Console.WriteLine(" \\         // ");
            Console.SetCursorPosition(x + 3, y++);
            Console.WriteLine(" \\       // ");
            Console.SetCursorPosition(x + 4, y++);
            Console.WriteLine(" \\     //  ");
            Console.SetCursorPosition(x + 5, y++);
            Console.WriteLine(" \\   // ");
            Console.SetCursorPosition(x + 6, y++);
            Console.WriteLine(" \\_// ");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("    ");

            Console.ResetColor();

        }

    }
}
