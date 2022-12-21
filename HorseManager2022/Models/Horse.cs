using HorseManager2022.Attributes;
using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Horse : ISelectable
    {
        // Constants
        public const int MAX_SHOP_HORSES = 10;

        // Properties
        [DisplayName("Name")]
        [Padding(20)]
        public string name { get; set; }

        [DisplayName("Rarity")]
        [Padding(12)]
        [IsRarity]
        public Rarity rarity { get; set; }

        [DisplayName("Energy")]
        [IsPercentage]
        [IsEnergy]
        public int energy { get; set; }

        [DisplayName("Resistance")]
        [Color(ConsoleColor.DarkGray)]
        public int resistance { get; set; }

        [DisplayName("Speed")]
        [Color(ConsoleColor.DarkGray)]
        public int speed { get; set; }

        [DisplayName("Age")]
        [Padding(7)]
        public int age { get; set; }
        
        [DisplayName("Price")]
        [Padding(9)]
        [IsPrice]
        public int price { get; set; }

        
        // Constructor
        public Horse()  //Construtor Random
        {
            rarity = GetRandomRarity();
            speed = GenerateSpeed(rarity);
            name = GenerateHorseName();
            price = GetHorsePrice(rarity, speed);
            resistance = CalculateResistence(speed,age);
            energy = 100;
            age = 10; // Do random
        }


        public Horse(string name, int resistance, int energy, int age, int price, int speed, Rarity rarity)  //Construtor with all
        {
            this.name = name;
            this.resistance = resistance;
            this.energy = energy;
            this.age = age;
            this.price = price;
            this.speed = speed;
            this.rarity = rarity;
        }


        static public List<Horse> GenerateShopHorses()
        {
            List<Horse> horses = new();
            for (int i = 0; i < MAX_SHOP_HORSES; i++)
                horses.Add(new());
            
            return horses;
        }
        

        public Rarity GetRandomRarity()  //Raridades(random)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 11);
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    return Rarity.Common;
                case 5:
                case 6:
                case 7:
                    return Rarity.Rare;
                case 8:
                case 9:
                    return Rarity.Epic;
                case 10:
                    return Rarity.Legendary;
                default:
                    return rarity = 0;
            }
        }


        public ConsoleColor GetRarityColor() => RarityExtensions.GetColor(rarity);
     
     
        public int GenerateSpeed(Rarity rarity) //Gerador de velocidades consoante a raridade do cavalo
        {
            Random random = new Random();
            switch (rarity)
            {
                case Rarity.Common:
                    return speed = random.Next(10, 31);
                case Rarity.Rare:
                    return speed = random.Next(30, 61);
                case Rarity.Epic:
                    return speed = random.Next(60, 81);
                case Rarity.Legendary:
                    return speed = random.Next(80, 101);
                default:
                    return speed = 0;
            }
        }

        public int GetHorsePrice(Rarity rarity, int speed) //Preço dos cavalos consoante a raridade 
        {
            Random random = new Random();
            switch (rarity)
            {
                case Rarity.Common:
                    return (speed <= 20) ? random.Next(100, 250) : random.Next(251, 500);

                case Rarity.Rare:
                    return (speed <= 40) ? random.Next(600, 1000) : random.Next(1001, 1500);

                case Rarity.Epic:
                    return (speed <= 60) ? random.Next(1600, 2300) : random.Next(2350, 3000);
                case Rarity.Legendary:
                    return (speed <= 80) ? random.Next(3100, 5000) : random.Next(5050, 6000);

                default:
                    return price = 0;
            }
        }
        public int CalculateResistence(int speed, int age)  //Calcular a Resistência
        {
            int x;
            x = (speed * age) / 7;
            return x;
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
            return nameArray[random.Next(0, nameArray.Length)] + " " + nameArray[random.Next(0, nameArray.Length)];
        }
    }
}
