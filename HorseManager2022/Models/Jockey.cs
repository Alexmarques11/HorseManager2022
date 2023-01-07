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
            this.handling = GenerateHandling();
            this.price = GetJockeyPrice();
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
            this.rarity = rarity;
            this.name = GenerateName();
            this.handling = handling;
            this.price = price;
        }

        
        public Jockey(Rarity rarity)
        {
            this.rarity = rarity;
            this.name = GenerateName();
            this.handling = GenerateHandling();
            this.price = GetJockeyPrice();
        }


        public Jockey(bool isLootBox)
        {
            rarity = RarityExtensions.GetRandomRarity(isLootBox);
            name = GenerateName();
            handling = GenerateHandling();
            price = GetJockeyPrice();
        }

        
        static public List<Jockey> GenerateShopJockeys()
        {
            List<Jockey> jockeys = new();
            for (int i = 0; i < MAX_SHOP_JOCKEYS; i++)
                jockeys.Add(new());

            return jockeys;
        }


        public ConsoleColor GetRarityColor() => RarityExtensions.GetColor(rarity);


        public string GenerateName()
        { 

            if (rarity == Rarity.Special)
                return "Moisés Herculano";

            string[] nameArray = { "Maria", "José","António","João","Francisco","Ana","Luís","Paulo","Carlos","André","Manuel","Pedro",
                                   "Francisca","Marcos","Raimundo","Ramiro","Sebastião","Marcelo","Jorge","Márcia","Geraldo","Adriana","Sandra",
                                   "Fernando","Fábio","Flábio","Gaspar","Miguel","Alex","Alexandre","Nuno","Roberto","Márcio","Mario","Luigi",
                                   "Sérgio","Josefa","Josefina","Genobeba","Patricia","Roberto","Daniel","Rodrigo","Rafael","Vasco","Joaquim",
                                   "Vera","Ricardo","Eduardo","Teresa","Sonia","Luciana","Claudio","Rosa","Benedito","Leandro","Raimunda" };

            string[] apelidoArray = {"Silva","Pereira","Marques","Sousa","Gaspar","Santos","Ferreira","Cerqueira","Oliveira","Costa","Rodrigues",
                                     "Costa","Martins","Jesus","Fernandes","Gonçalves","Gomes","Lopes","Alves","Almeida","Ribeiro",
                                     "Pinto","Carvalho","Teixeira","Moreira","Correia","Mendes","Nunes","Soares","Vieira","Monteiro","Cardoso","Rocha" };

            return nameArray[GameManager.GetRandomInt(0, nameArray.Length)] + " " + apelidoArray[GameManager.GetRandomInt(0, apelidoArray.Length)];

        }

        
        public int GenerateHandling() //Gerador de handlings consoante a raridade do jocey
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
        

        public int GetJockeyPrice()
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return (handling <= 10) ? GameManager.GetRandomInt(100, 200) : GameManager.GetRandomInt(201, 400);

                case Rarity.Rare:
                    return (handling <= 30) ? GameManager.GetRandomInt(400, 700) : GameManager.GetRandomInt(701, 1000);

                case Rarity.Epic:
                    return (handling <= 50) ? GameManager.GetRandomInt(1000, 2000) : GameManager.GetRandomInt(2001, 3000);
                case Rarity.Legendary:
                case Rarity.Special:
                    return (handling <= 70) ? GameManager.GetRandomInt(3000, 5000) : GameManager.GetRandomInt(5001, 7000);

                default:
                    return price = 0;
            }
        }


        public override string ToString()
        {
            return $"{name} - {rarity} - {handling} - {price}";
        }
    }
}
