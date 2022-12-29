using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HorseManager2022.Deprecated;
using HorseManager2022.Models;

namespace HorseManager2022
{
    internal class SaveManager
    {
        // Properties
        private readonly string extension = ".hm";
        public string saveName = "default";
        
        private string rootPath => Directory.GetCurrentDirectory() + "\\saves\\";
        private string savePath => rootPath + saveName + extension;

        // Get all folder names in rootPath folder
        public string[] saves
        {
            get
            {
                string[] paths;

                try
                {
                    paths = Directory.GetFiles(rootPath);
                }
                catch (Exception)
                {
                    Directory.CreateDirectory(rootPath);
                    paths = Directory.GetFiles(rootPath);
                }

                // Remove rootPath from paths
                for (int i = 0; i < paths.Length; i++) 
                {
                    paths[i] = paths[i].Replace(rootPath, "");
                    paths[i] = paths[i].Replace(extension, "");
                }
                return paths;
            }
        }

        // Constructor
        public SaveManager() { }


        // Methods
        public void SaveGame(GameData gameData)
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            
            using (var stream = File.Open(savePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, gameData);
            }
        }

        
        public GameData? LoadGame()
        {
            if (!File.Exists(savePath) || File.ReadAllBytes(savePath).Length == 0)
                return new();

            using (var stream = File.Open(savePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                GameData gameData = (GameData)formatter.Deserialize(stream);
                return gameData;
            }
        }

    }
}
