using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HorseManager2022.Enums;

namespace HorseManager2022.UI.Components
{
    internal class BoardMenu
    {
        // Constants
        private const int BOARD_MENU_X = 40;
        private const int BOARD_MENU_Y = 34;

        // Properties
        private int x, y;
        private ScreenWithTopbar currentScreen;

        private List<Option> options
        {
            get
            {
                return currentScreen.options;
            }
        }

        private int selectedPosition
        {
            get
            {
                return currentScreen.selectedPosition;
            }
            set
            {
                currentScreen.selectedPosition = value;
            }
        }

        private bool isSelectingDown
        {
            get
            {
                return currentScreen.menuMode == MenuMode.Down;
            }
        }

        private int padding
        {
            get
            {
                return options.Count == 0 ? 0 : 50 / options.Count;
            }
        }

        // Constructor
        public BoardMenu(ScreenWithTopbar currentScreen)
        {
            x = BOARD_MENU_X;
            y = BOARD_MENU_Y;
            this.currentScreen = currentScreen;
        }


        public void Show()
        {
            DrawBoard();
            DrawOptions();
            DrawSelectionButtons();
        }


        public void DrawBoard()
        {
            Console.WriteLine("|     /               /    |             ________________|_______|__U________________|____      |     ");
            Console.WriteLine("|    /               /     |____________/                                                 \\     |    ");
            Console.WriteLine("|   /               /      /           /                                                   \\    |    ");
            Console.WriteLine("|  /               /      /           /                                                     \\   |    ");
            Console.WriteLine("| /               /      /           /                                                       \\  |    ");
            Console.WriteLine("|/_______________/      /           /_________________________________________________________\\ |    ");
            Console.WriteLine("||               |     /            |                                                          ||     ");
            Console.WriteLine("||               |    /             |                                                          ||     ");
            Console.WriteLine("||               |   /              |                                                          ||     ");
            Console.WriteLine("||               |  /               |                                                          ||     ");
            Console.WriteLine("||               | /                |                                                          ||     ");
            Console.WriteLine("||_______________|/_________________|__________________________________________________________||     ");
        }


        public void DrawOptions()
        {
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < options.Count; i++)
            {
                string text = options[i].text.PadLeft(padding / 2 + options[i].text.Length / 2).PadRight(padding);
                Console.Write(text);
            }
        }


        public void DrawSelectionButtons()
        {
            Console.SetCursorPosition(x, y + 2);
            for (int i = 0; i < options.Count; i++)
            {
                string mark = i == selectedPosition && isSelectingDown ? "[X]" : "[ ]";
                mark = mark.PadLeft(padding / 2 + mark.Length / 2).PadRight(padding);
                Console.Write(mark);
            }
        }

    }
}
