using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    static class UI
    {

        public static void PopulateScreenWithSaveOptions(Screen screen, Screen nextScreen, GameManager gameManager)
        {
            screen.ClearOptions();
            foreach (string save in gameManager.saveManager.saves)
                screen.AddOption(save, nextScreen, () => gameManager.saveName = save);
        }
        
        // Show Credit Screen
        public static void ShowCreditScreen()
        {
            // Menu
            Console.Clear();
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("|       Game Developed by       |");
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("|                               |");
            Console.WriteLine("| 20115 - André Cerqueira       |");
            Console.WriteLine("|                               |");
            Console.WriteLine("| 20116 - Nuno Fernandes        |");
            Console.WriteLine("|                               |");
            Console.WriteLine("| 25968 - Alexandre Marques     |");
            Console.WriteLine("|                               |");
            Console.WriteLine("| 25977 - Miguel Sousa          |");
            Console.WriteLine("|                               |");
            Console.WriteLine("| 26531 - Gonçalo Gaspar        |");
            Console.WriteLine("|                               |");
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadLine();
        }

        
        // Show Create new save Screen
        public static void ShowCreateNewSaveScreen(Action<string> onComplete)
        {
            // Menu
            Console.Clear();
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine("|     Create new save game      |");
            Console.WriteLine("+-------------------------------+");

            // Get data
            Console.Write("Save name: ");
            string? savename = Console.ReadLine();

            if (savename == "" || savename == null)
                return;

            onComplete(savename);
        }
    }
}
