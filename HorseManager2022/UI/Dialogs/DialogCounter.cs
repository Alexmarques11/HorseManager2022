using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI.Dialogs
{
    internal class DialogCounter
    {
        // Constants
        private const int DELAY_TIME = 1000;
        private const int MAX_VALUE = 3;
        
        // Properties
        private int x { get; set; }
        private int y { get; set; }
        private int initialY { get; set; }

        // Constructor
        public DialogCounter(int x, int y)
        {
            this.x = x;
            this.y = y;
            initialY = y;
        }

        // Methods
        public void Show()
        {
            int value = MAX_VALUE;
            while (value > 0)
            {
                switch (value)
                {
                    case 1:
                        Show1();
                        break;

                    case 2:
                        Show2();
                        break;

                    case 3:
                        Show3();
                        break;

                    default:
                        break;
                }
                value--;
                Thread.Sleep(DELAY_TIME);
            }

            ShowGo();
            Thread.Sleep(DELAY_TIME);

        }

        public void Show1()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|         The Race starts in...        |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|--------------------------------------|");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                 __                   |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            ...-'  |`.                |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            |      |  |               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            ....   |  |               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|              -|   |  |               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|               |   |  |               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            ...'   `--'               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            |         |`.             |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            ` --------\\ |             |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|             `---------'              |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            y = initialY;
        }

        public void Show2()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|         The Race starts in...        |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|--------------------------------------|");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                  .-''-.              |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                .' .-.  )             |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|               / .'  / /              |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|              (_/   / /               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                   / /                |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                  / /                 |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                 . '                  |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                / /    _.-')          |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|              .' '  _.'.-''           |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|             /  /.-'_.'               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            /    _.'                  |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|           ( _.-'                     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            y = initialY;
        }

        public void Show3()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|         The Race starts in...        |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|--------------------------------------|");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|           ..-'''-.                   |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|           \\.-'''\\ \\                  |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                  | |                 |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|               __/ /                  |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|              |_  '.                  |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                 `.  \\                |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                   \\ '.               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                    , |               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                    | |               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                   / ,'               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|           -....--'  /                |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|           `.. __..-'                 |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                                      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            y = initialY;
        }
    
        public void ShowGo()
        {
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|                 Run!!!               |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|--------------------------------------|");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|             .-'''-.        ___       |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|            '   _    \\   .'/   \\      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|          /   /` '.   \\ / /     \\     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|   .--./).   |     \\  ' | |     |     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  /.''\\\\ |   '      |  '| |     |     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("| | |  | |\\    \\     / / |/`.   .'     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  \\`-' /  `.   ` ..' /   `.|   |      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  /(\"'`      '-...-'`     ||___|      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  \\ '---.                 |/___/      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  /(\"'`      '-...-'`     ||___|      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  \\ '---.                 |/___/      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|   /'\"\"'.\\                .'.--.      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  ||     ||              | |    |     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|  \\'. __//               \\_\\    /     |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("|   `'---'                 `''--'      |");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("+--------------------------------------+");
            y = initialY;
        }
    }
}
