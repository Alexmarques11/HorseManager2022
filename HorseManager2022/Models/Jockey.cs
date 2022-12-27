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
        

        public string GenerateName(){ //Gerador do nome dos jockey(random) 
            string[] nameArray = { "Maria", "José","António","João","Francisco","Ana","Luís","Paulo","Carlos","André","Moisés","Manuel","Pedro",
                                   "Francisca","Marcos","Raimundo","Ramiro","Sebastião","Marcelo","Jorge","Márcia","Geraldo","Adriana","Sandra",
                                   "Fernando","Fábio","Flábio","Gaspar","Miguel","Alex","Alexandre","Nuno","Roberto","Márcio","Mario","Luigi",
                                   "Sérgio","Josefa","Josefina","Genobeba","Patricia","Roberto","Daniel","Rodrigo","Rafael","Vasco","Joaquim",
                                   "Vera","Ricardo","Eduardo","Teresa","Sonia","Luciana","Claudio","Rosa","Benedito","Leandro","Raimunda" };

            string[] apelidoArray = {"Silva","Pereira","Marques","Sousa","Gaspar","Santos","Ferreira","Cerqueira","Oliveira","Costa","Rodrigues",
                                     "Costa","Martins","Jesus","Fernandes","Herculano","Gonçalves","Gomes","Lopes","Alves","Almeida","Ribeiro",
                                     "Pinto","Carvalho","Teixeira","Moreira","Correia","Mendes","Nunes","Soares","Vieira","Monteiro","Cardoso","Rocha" };

            Random random = new Random();
            return nameArray[random.Next(0, nameArray.Length)] + " " + apelidoArray[random.Next(0, apelidoArray.Length)];
        }

        
        public int GenerateHandling(Rarity rarity) //Gerador de handlings consoante a raridade do jocey
        {
            Random random = new Random();
            switch (rarity)
            {

                case Rarity.Common:
                    return handling = random.Next(1, 31);
                case Rarity.Rare:
                    return handling = random.Next(30, 61);
                case Rarity.Epic:
                    return handling = random.Next(60, 81);
                case Rarity.Legendary:
                case Rarity.Special:
                    return handling = random.Next(80, 101);
                default:
                    return handling = 0;
            }
        }
        public int GetJockeyPrice(Rarity rarity, int handling) //Preço dos jockeys consoante a raridade e o handling
        {
            Random random = new Random();
            switch (rarity)
            {
                case Rarity.Common:
                    return (handling <= 20) ? random.Next(100, 250) : random.Next(251, 500);

                case Rarity.Rare:
                    return (handling <= 40) ? random.Next(600, 1000) : random.Next(1001, 1500);

                case Rarity.Epic:
                    return (handling <= 60) ? random.Next(1600, 2300) : random.Next(2350, 3000);
                case Rarity.Legendary:
                case Rarity.Special:
                    return (handling <= 80) ? random.Next(3100, 5000) : random.Next(5050, 6000);

                default:
                    return price = 0;
            }
        }
    }
}
