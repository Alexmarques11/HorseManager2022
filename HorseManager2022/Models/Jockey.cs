using HorseManager2022.Attributes;
using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Jockey : IExchangeable
    {
        // Constants
        public const int MAX_SHOP_JOCKEYS = 5;
        
        [DisplayName("Name")]
        [Padding(22)]
        public string name { get; set; }

        [DisplayName("Rarity")]
        [Padding(12)]
        [IsRarity]
        public Rarity rarity { get; set; }

        [DisplayName("Handling")]
        [Color(ConsoleColor.DarkGray)]
        public int handling { get; set; }

        [DisplayName("Price")]
        [Padding(11)]
        [IsPrice]
        public int price { get; set; }
        

        public Jockey()  //Construtor vazio totalmente Random 
        {
            //this.id = RandomID();
            this.name = GenerateName();
            this.rarity = RarityExtensions.GetRandomRarity();
            this.handling = GenerateHandling(rarity);
            this.price = GetJockeyPrice(rarity, handling);
        }

        public Jockey(string name, Rarity rarity, int handling, int price)
        {
            this.name = name;
            this.rarity = rarity;
            this.handling = handling;
            this.price = price;
        }
        
        public Jockey(Rarity rarity, int handling, int price)
        {
            this.name = GenerateName();
            this.rarity = rarity;
            this.handling = handling;
            this.price = price;
        }

        public Jockey(Rarity rarity)
        {
            this.name = GenerateName();
            this.rarity = rarity;
            this.handling = GenerateHandling(rarity);
            this.price = GetJockeyPrice(rarity, handling);
        }

        static public List<Jockey> GenerateShopJockeys()
        {
            List<Jockey> jockeys = new();
            for (int i = 0; i < MAX_SHOP_JOCKEYS; i++)
                jockeys.Add(new());

            return jockeys;
        }
        

        public string GenerateName()  //Gerador do nome dos jockey(random) NOTA:Ainda falta alterar os nomes que estão no array
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

            
            return nameArray[GameManager.GetRandomInt(0, nameArray.Length)] + " " + nameArray[GameManager.GetRandomInt(0, nameArray.Length)];
        }

        
        public int GenerateHandling(Rarity rarity) //Gerador de handlings consoante a raridade do jocey
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return handling = GameManager.GetRandomInt(1, 31);
                case Rarity.Rare:
                    return handling = GameManager.GetRandomInt(30, 61);
                case Rarity.Epic:
                    return handling = GameManager.GetRandomInt(60, 81);
                case Rarity.Legendary:
                case Rarity.Special:
                    return handling = GameManager.GetRandomInt(80, 101);
                default:
                    return handling = 0;
            }
        }
        public int GetJockeyPrice(Rarity rarity, int handling) //Preço dos jockeys consoante a raridade e o handling
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return (handling <= 20) ? GameManager.GetRandomInt(100, 250) : GameManager.GetRandomInt(251, 500);

                case Rarity.Rare:
                    return (handling <= 40) ? GameManager.GetRandomInt(600, 1000) : GameManager.GetRandomInt(1001, 1500);

                case Rarity.Epic:
                    return (handling <= 60) ? GameManager.GetRandomInt(1600, 2300) : GameManager.GetRandomInt(2350, 3000);
                case Rarity.Legendary:
                case Rarity.Special:
                    return (handling <= 80) ? GameManager.GetRandomInt(3100, 5000) : GameManager.GetRandomInt(5050, 6000);

                default:
                    return price = 0;
            }
        }
    }
}
