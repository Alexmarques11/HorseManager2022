﻿using HorseManager2022;
using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Horse2
{
    internal class Horse
    {
        // Constants
        private const string PATH = "horses.txt";

        // Properties
        public int id;
        public string name;
        public int resistance;
        public int energy;
        public int age;
        public int price;
        public int speed;
        public Rarity rarity;

        // Constructor
        public Horse()  //Construtor Random vazio
        {
            id = GetAutoIncrementId();
            rarity = RandomRarity();
            speed = GenerateSpeed(rarity);
            name = GenerateHorseName();
            price = HorsePrice(rarity, speed);
            resistance = CalculateResistence(speed,age);
            energy = 100;
            age = 10; // Fazer random
        }

        public Horse(int id, string name, int resistance, int energy, int age, int price, int speed, Rarity rarity)  //Construtor com todos os atributos
        {
            this.id = id;
            this.name = name;
            this.resistance = resistance;
            this.energy = energy;
            this.age = age;
            this.price = price;
            this.speed = speed;
            this.rarity = rarity;
        }

        public Horse(string save)  //Construtor Random vazio
        {
            string D = Game.DELIMITER;
            string[] parts = save.Split(D);

            id = int.Parse(parts[0]);
            name = parts[1];
            resistance = int.Parse(parts[2]);
            energy = int.Parse(parts[3]);
            age = int.Parse(parts[4]);
            price = int.Parse(parts[5]);
            speed = int.Parse(parts[6]);
            rarity = (Rarity)int.Parse(parts[7]);
        }

        
        // File Crud Methods
        static public Horse GetSave(int id)
        {
            string D = Game.DELIMITER;
            string horseStr = File.ReadAllLines(PATH).Where(x => x.Split(D)[0] == id.ToString()).First();

            return new Horse(horseStr);
        }

        static public List<Horse> GetSave()
        {
            string[] horseStr = File.ReadAllLines(PATH);

            List<Horse> horses = new();

            foreach (string horse in horseStr)
                horses.Add(new Horse(horse));

            return horses;
        }

        static public void AddSave(Horse horse) => File.AppendAllText(Game.horsePath, horse.ToSaveFormat());

        
        static public void UpdateSave(Horse horse)
        {
            // Variables
            string[] lines = File.ReadAllLines(PATH);

            // Iterate over the lines in the file
            for (int i = 0; i < lines.Length; i++)
            {
                Horse _horse = new(lines[i]);

                if (_horse.id == horse.id)
                    lines[i] = horse.ToSaveFormat();
            }


            // Write the updated lines to the file
            File.WriteAllLines(PATH, lines);
        }

        
        public string ToSaveFormat()
        {
            string D = Game.DELIMITER;
            return id + D + name + D + resistance + D + energy + D + age + D + price + D + speed + D + rarity + Environment.NewLine;
        }

        
        static private int GetAutoIncrementId()
        {
            string horseStr = File.ReadAllLines(PATH).Last();
            Horse horse = new(horseStr);
            return horse.id + 1;
        }






        // Methods
        public int RandomID()  //Provisório depois vai ser alterado
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 10000);
            return i;
        }
        
        public string GenerateHorseName()  //Gerador do nome dos cavalos(random)
        {
            string[] nameArray ={ "Abbey", "Ace", "Aesop", "Afrika", "Aggie", "Ajax","Alpha","Alfie","Ali","Aladdin","Alibaba",
                                  "Bishop","Birdie","Blossom", "Moon", "Bo", "Boaz", "Bodhi", "Bogart", "Bonnie", "Booker", "Boomer", "Boon",
                                  "Clyde","Cochise","Coco","Cocolo","Cole","Conan","Concho","Cookie","Cooper","Casper","Cecil","Champ","Chance","Charcoal",
                                  "Dollar","Dolly","Dominic","Dominator","Dora","Dorado","Drake","Dream","Dreamer","Drifter","Duce","Duchess","Duke","Dunny","Durango","Duster","Dusty",
                                  "Easter","Ebony"," Echo","Eclipse","Eddie","Eldorado","Eleazar","Eli", "Elixir","Ellie","Elvis","Ember","Epona","Esperanza","Esteban", "Excalibur",
                                  "Fancy","Fargo","Felise","Festus","Fiddle","Fifty","Fiona",
                                  "Gracie","Grit","Guapo","Gucci","Gulliver","Gunner","Gus","Gypsy",
                                  "Houdini","Howdy","Huck","Huckleberry","Huey","Hurricane",
                                  "Kansas","Kate","Katy","Brown","Keisha","Kemosabe","Keno","Kendra",
                                  "Lacey","Lady","Lakota","Legend","Legacy","Lena","Levi","Leo","Lexy","Liberty",
                                  "Money","Montana","Monty","Moon","Moondance","Moonshine","Moose","Mordecai","Morgan","Moxie","Mystic","Mystery",
                                  "Nacho","Nala","Natacha","Navajo","Nemo","Neptune","Nero","Nevada","Night","Niner","Nyx",
                                  "Oliver","Ollie","Oncore","Onyx","Opal","Oreo","Outlaw","Ozzy",
                                  "Paco","Pablo","Paige","Paisley","Panama","Pandora","Papoose","Paprika","Partner","Patches",
                                  "Queball","Queen","Queenie","Quervo","Quest","Quincy",
                                  "Rojo","Rolly","Roman","Rono","Rooster","Rounder","Rowdy","Rowen","Roy","Ruby","Rumi","Rumor","Rustler","Rusty","Ruth",
                                  "Sabino","Sabrina","Sage","Sahara","Sailor","Saint","Sally","Salty","Sammy","Sampson","Sandy","Sargent","Sassy","Savanna","Scamper","Scarlet",
                                  "Travis","Treasure","Trevor","Trickster","Trigger","Trinket","Troubadour","Trucker","Trusty","Tucker","Tuff","Turbo","Twister","Ty",
                                  "Umber","Ulysses","Uno","UriUtah",
                                  "Val","Van Gogh","Vargas","Vegas","Venus","Vesta","Victory",
                                  "Willard","Willie","Willow","Winchester","Windy","Wing","Winston","Winter","Wolf","Wrangler",
                                  "Xavier",
                                  "Yakama","Yankie","Yeller","Yeti","Yoda","Yonkers",
                                  "Zahara","Zara","Zelda","Zenia","Zia","Zipper","Zodiac","Zoe","Zoey","Zoro","Zeus","Zuza"};
            
            
            Random random = new Random();
            return nameArray[random.Next(0, nameArray.Length)] + " "+ nameArray[random.Next(0, nameArray.Length)];
        }

        public Rarity RandomRarity()  //Raridades(random)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 11);
            switch (i)
            {
                case 0: case 1: case 2: case 3: case 4:
                    return Rarity.Common;
                case 5: case 6:case 7:
                    return Rarity.Rare;
                case 8: case 9:
                    return Rarity.Epic;
                case 10:
                    return Rarity.Legendary;
                default:
                    return rarity = 0;
            }
        }

        public ConsoleColor RarityColor(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return ConsoleColor.White;
                case Rarity.Rare:
                    return ConsoleColor.Blue;
                case Rarity.Epic:
                    return ConsoleColor.DarkMagenta;
                case Rarity.Legendary:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.Gray;
            }
        }
        
        public int GenerateSpeed(Rarity rarity) //Gerador de velocidades consoante a raridade do cavalo
        {
            Random random = new Random();
            switch (rarity)
            {
                case Rarity.Common:
                    return speed=random.Next(5, 11);
                case Rarity.Rare:
                    return speed = random.Next(10,16 );
                case Rarity.Epic:
                    return speed = random.Next(15, 21);
                case Rarity.Legendary:
                    return speed = random.Next(20, 31);
                default:
                    return speed = 0;
            }   
        }

        public int HorsePrice(Rarity rarity, int speed) //Preço dos cavalos consoante a raridade 
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return (speed <= 7) ? price=500: price=600;

                case Rarity.Rare:
                    return (speed <= 12) ? price = 700 : price = 800;

                case Rarity.Epic:
                    return (speed <= 17) ? price = 900 : price = 1000;
                case Rarity.Legendary:
                    return price = 1300;

                default:
                    return price = 0;
            }
        }
        public int CalculateResistence(int speed, int age)  //Calcular a Resistência
        {
            int x;
            x = (speed + age) / 2;
            return x;
        }
    }
}
