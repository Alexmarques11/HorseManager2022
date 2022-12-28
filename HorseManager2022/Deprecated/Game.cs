using HorseManager2022.Models;
using HorseManager2022.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Deprecated
{
    /*
    static internal class Game
    {
        // Properties
        static public string DELIMITER = ";";
        static public string saveName = "default";
        static private string rootPath => Directory.GetCurrentDirectory() + "\\saves\\";
        static private string savePath => rootPath + saveName + "\\";

        // Save Paths
        static public string playerPath => savePath + "player.txt";
        static public string horsePath => savePath + "horses.txt";
        static public string jockeyPath => savePath + "jockeys.txt";
        static public string horseJockeyPath => savePath + "horseJockeys.txt";
        static public string eventPath => savePath + "events.txt";

        // Get all folder names in rootPath folder
        static public string[] saves
        {
            get
            {
                string[] paths;

                try
                {
                    paths = Directory.GetDirectories(rootPath);
                }
                catch (Exception)
                {
                    Directory.CreateDirectory(rootPath);
                    paths = Directory.GetDirectories(rootPath);
                }

                // Remove rootPath from paths
                for (int i = 0; i < paths.Length; i++)
                    paths[i] = paths[i].Replace(rootPath, "");

                return paths;
            }
        }


        public static void PopulateScreenWithSaveOptions(Screen screen, Screen nextScreen)
        {
            screen.ClearOptions();
            foreach (string save in saves)
                screen.AddOption(save, nextScreen, () => saveName = save);
        }

        static public void CreateNewSave()
        {
            // Create save folder
            CreateFolder();

            // Create all files
            CreateFile("player.txt");
            CreateFile("horses.txt");
            CreateFile("horseJockeys.txt");
            CreateFile("jockeys.txt");
            CreateFile("staff.txt");
            CreateFile("events.txt");

            // Create objects
            SaveManager.Add<Player>(new());
            Event.CreateSave();

        }


        static public void DeleteSave() => Directory.Delete(savePath, true);

        static private void CreateFolder() => Directory.CreateDirectory(savePath);

        static private void CreateFile(string fileName)
        {
            using (StreamWriter sw = File.CreateText(savePath + fileName))
            {
                sw.Write("");
            }
        }


    }*/
}
