using HorseManager2022.Attributes;
using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using System.ComponentModel;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Horse : IExchangeable
    {
        // Constants
        static public readonly int ENERGY_RECOVERY_MIN = 5;
        static public readonly int ENERGY_RECOVERY_MAX = 20;
        static public readonly int BASE_ENERGY_CONSUMED_PER_KM = 500; // Will take into account the horse's resistance
        public const int MAX_SHOP_HORSES = 5;

        // Properties
        [DisplayName("Name")]
        [Padding(22)]
        public string name { get; set; }

        [DisplayName("Rarity")]
        [Padding(12)]
        [IsRarity]
        public Rarity rarity { get; set; }

        private int _energy;
        [DisplayName("Energy")]
        [IsPercentage]
        [IsEnergy]
        public int energy 
        {
            get => _energy;
            set
            {
                _energy = value;
                if (_energy > 100)
                    _energy = 100;
                else if (_energy < 0)
                    _energy = 0;
            }
        }

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
        [Padding(11)]
        [IsPrice]
        public int price { get; set; }

        
        // Constructor
        public Horse()  //Construtor Random
        {
            rarity = RarityExtensions.GetRandomRarity();
            speed = GenerateStatValue();
            name = GenerateHorseName();
            price = GetHorsePrice();
            resistance = GenerateStatValue();
            energy = 100;
            age = 10; // Do random
        }


        public Horse(bool isLootBox)  //Construtor Random
        {
            rarity = RarityExtensions.GetRandomRarity(isLootBox);
            speed = GenerateStatValue();
            name = GenerateHorseName();
            price = GetHorsePrice();
            resistance = GenerateStatValue();
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

        
        public Horse(Rarity rarity)
        {
            Random random = new Random();

            // Set the horse's properties based on the rarity
            this.rarity = rarity;
            this.name = GenerateHorseName();
            this.speed = GenerateStatValue();
            this.price = GetHorsePrice();
            this.resistance = GenerateStatValue();
            this.energy = 100;
            this.age = random.Next(10, 21); // Generate a random age between 10 and 20
        }


        static public List<Horse> GenerateShopHorses()
        {
            List<Horse> horses = new();
            for (int i = 0; i < MAX_SHOP_HORSES; i++)
                horses.Add(new());
            
            return horses;
        }
        

        public ConsoleColor GetRarityColor() => RarityExtensions.GetColor(rarity);


        private int GenerateStatValue() //Gerador de velocidades consoante a raridade do cavalo
        {
            Random random = new Random();
            switch (rarity)
            {
                case Rarity.Common:
                    return speed = random.Next(10, 20);
                case Rarity.Rare:
                    return speed = random.Next(20, 40);
                case Rarity.Epic:
                    return speed = random.Next(40, 60);
                case Rarity.Legendary:
                    return speed = random.Next(60, 80);
                case Rarity.Special:
                    return speed = random.Next(80, 101);
                default:
                    return speed = 0;
            }
        }
        

        private int GetHorsePrice() //Preço dos cavalos consoante a raridade 
        {
            int statValue = (int)Math.Round((speed + resistance) / 2f);
            Random random = new Random();
            switch (rarity)
            {
                case Rarity.Common:
                    return (statValue <= 10) ? random.Next(100, 250) : random.Next(251, 500);

                case Rarity.Rare:
                    return (statValue <= 30) ? random.Next(600, 1000) : random.Next(1001, 1500);

                case Rarity.Epic:
                    return (statValue <= 50) ? random.Next(1600, 2300) : random.Next(2350, 3000);
                case Rarity.Legendary:
                case Rarity.Special:
                    return (statValue <= 70) ? random.Next(3100, 5000) : random.Next(5050, 6000);

                default:
                    return price = 0;
            }
        }


        private string GenerateHorseName()  //Gerador do nome dos cavalos(random)
        {
            string[] nameArray;
            
            if (rarity == Rarity.Special)
            {
                nameArray = new string[] { 
                    "André Cerqueira", 
                    "Nuno Fernandes", 
                    "Alexandre Marques",
                    "Miguel Sousa",
                    "Gonçalo Gaspar"};
            }
            else { 

                nameArray = new string[] { "Abbey", "Ace", "Aesop", "Afrika", "Aggie", "Ajax","Alpha","Alfie","Ali","Aladdin","Alibaba",
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
                                  "Travis","Treasure","Trevor","Trickster","Trigger","Trinket","Troubador","Trucker","Trusty","Tucker","Tuff","Turbo","Twister","Ty",
                                  "Umber","Ulysses","Uno","UriUtah",
                                  "Val","Van Gogh","Vargas","Vegas","Venus","Vesta","Victory",
                                  "Willard","Willie","Willow","Winchester","Windy","Wing","Winston","Winter","Wolf","Wrangler",
                                  "Xavier",
                                  "Yakama","Yankie","Yeller","Yeti","Yoda","Yonkers",
                                  "Zahara","Zara","Zelda","Zenia","Zia","Zipper","Zodiac","Zoe","Zoey","Zoro","Zeus","Zuza"};
            
            }

            Random random = new Random();
            if (rarity != Rarity.Special)
                return nameArray[random.Next(0, nameArray.Length)] + " " + nameArray[random.Next(0, nameArray.Length)];
            else
                return nameArray[random.Next(0, nameArray.Length)];
        }
    }
}
