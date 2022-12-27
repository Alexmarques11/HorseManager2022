using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    internal class Racetrack
    {
        // Constants
        private int HORSE_LENGTH = 9;
        private const string TRAINING_TRACK_NAME = "Cristiano Ronaldo Training Ground";
        private const float BASE_REAL_DISTANCE = 1.4f;
        private const int BASE_GAME_DISTANCE = 50;
        
        // Properties
        public string name;
        public int teamsQuantity;
        private string[,] racetrackLines;
        public Difficulty difficulty;

        public float realDistance 
        {
            get => difficulty switch
            {
                Difficulty.Easy => BASE_REAL_DISTANCE,
                Difficulty.Normal => 2.3f,
                Difficulty.Hard => 3.5f,
                Difficulty.Extreme => 4.2f,
                _ => 0,
            };
        }

        public int gameDistance
        {
            get => (int)(realDistance * BASE_GAME_DISTANCE / BASE_REAL_DISTANCE);
        }


        // Event Racetrack Constructor
        public Racetrack(Difficulty? difficulty, int teamsQuantity)
        {
            name = GetRandomName();
            this.difficulty = difficulty ?? Difficulty.Easy;
            this.teamsQuantity = teamsQuantity;
            racetrackLines = GetRacetrackLines();
        }
        

        // Training Racetrack Constructor
        public Racetrack()
        {
            name = TRAINING_TRACK_NAME;
            difficulty = Difficulty.Easy;
            teamsQuantity = 1;
            racetrackLines = GetRacetrackLines();
        }


        // Methods
        public void Show()
        {
            // Racetrack Start
            Console.WriteLine();
            Console.WriteLine();
            DrawRacetrackBorder();

            // Racetrack Lines
            for (int i = 0; i < teamsQuantity; i++)
            {
                for (int j = 0; j < 5; j++)
                    Console.WriteLine(racetrackLines[i, j]);
                DrawRacetrackBorder();
            }
        }


        private string[,] GetRacetrackLines()
        {
            string[,] lines = new string[teamsQuantity, 5];
            for (int i = 0; i < teamsQuantity; i++)
            {
                for (int j = 0; j < 5; j++)
                    lines[i, j] = GetRacetrackLine();
            }
            return lines;
        }


        private string GetRacetrackLine()
        {
            string line = "";
            int length = gameDistance + HORSE_LENGTH;
            for (int i = 0; i < length; i++)
            {
                bool isGrass = GameManager.GetRandomInt(0, 11) == 0;
                line += isGrass ? "~" : " ";
            }
            return line;
        }


        private void DrawRacetrackBorder() => Console.WriteLine(new string('=', gameDistance + HORSE_LENGTH));
        

        static private string GetRandomName()
        {
            string[] names = { "Belmont Park", "Churchill Downs", "Santa Anita Park", "Saratoga Race Course", "Epsom Downs",
                       "Ascot Racecourse", "Aintree Racecourse", "Goodwood Racecourse", "Haydock Park Racecourse", "Kempton Park Racecourse",
                       "Leopardstown Racecourse", "Lingfield Park Racecourse", "Newbury Racecourse", "Pontefract Racecourse", "York Racecourse" };

            int index = GameManager.GetRandomInt(0, names.Length);
            return names[index];
        }

    }
}
