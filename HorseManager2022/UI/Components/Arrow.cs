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

        // Constructor
        public Arrow(int margin, int offsetX = 0, int offsetY = 0)
        {
            this.margin = margin;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            selectedPosition = 0;
        }

        // Methods
        public void Draw()
        {
            int x = (selectedPosition + 1) * margin + offsetX;
            int y = offsetY;

            Console.SetCursorPosition(x, y);
            Console.Write("__");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("||");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("||");
            Console.SetCursorPosition(x - 2, y + 3);
            Console.Write("__||__");
            Console.SetCursorPosition(x - 2, y + 4);
            Console.Write("\\ || /");
            Console.SetCursorPosition(x - 1, y + 5);
            Console.Write("\\  /");
            Console.SetCursorPosition(x, y + 6);
            Console.Write("\\/");
            Console.SetCursorPosition(x, y);
        }

    }
}
