using HorseManager2022.Attributes;
using HorseManager2022.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class Table<T>
    {
        // Constants
        public const int DEFAULT_TABLE_WIDTH = 72;

        // Properties
        string[] propertiesToExclude;

        // Constructor
        public Table(string[] propertiesToExclude)
        {
            this.propertiesToExclude = propertiesToExclude;
        }

        // Methods
        public void Show(GameManager? gameManager)
        {

            // Initial verifications
            if (gameManager == null)
                return;

            // Get data
            List<T> items = gameManager.GetList<T>();
            string tableName = GetTableName();
            List<string> headers = GetTableHeaders();

            if (items.Count == 0)
                headers.Add(Utils.PadCenter("Nothing to show.", DEFAULT_TABLE_WIDTH));

            int tableWidth = GetTableWidth(headers);

            // Show data
            Console.ResetColor();
            Console.WriteLine();

            DrawLine(tableWidth);

            // Title
            DrawTitle(tableName, tableWidth - 2);

            DrawLine(tableWidth);

            // Header
            DrawHeader(headers);

            DrawLine(tableWidth);

            // Content
            DrawContent(items, headers);

            if (items.Count != 0)
                DrawLine(tableWidth);
        }


        private string GetTableName() => typeof(T).Name.ToLower() + "s";


        private List<string> GetTableHeaders()
        {
            List<string> headers = new();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor property in properties)
            {
                if (propertiesToExclude.Contains(property.Name))
                    continue;

                PaddingAttribute? padding = property.Attributes.OfType<PaddingAttribute>().FirstOrDefault();

                int value = padding?.value ?? 0;
                string name = property.DisplayName;
                name = (value != 0) ? Utils.PadCenter($" {name} ", value) : $" {name} ";
                headers.Add(name);
            }

            return headers;
        }


        static private int GetTableWidth(List<string> headers)
        {
            int tableWidth = 0;
            foreach (string header in headers)
                tableWidth += header.Length;

            // Add bar "|" count for each
            tableWidth += headers.Count - 1;

            return tableWidth;
        }



        private void DrawHeader(List<string> headers)
        {
            foreach (string header in headers)
                Console.Write("|" + header);
            Console.WriteLine("|");
        }


        private void DrawContent(List<T> items, List<string> headers)
        {
            foreach (T item in items)
                DrawRow(item, headers);
        }


        private void DrawRow(T item, List<string> headers)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor property in properties)
                DrawColumn(item, property, headers);

            Console.WriteLine("|");
        }


        private void DrawColumn(T item, PropertyDescriptor property, List<string> headers)
        {
            if (propertiesToExclude.Contains(property.Name))
                return;

            string? header = headers.FirstOrDefault(h => h.Contains(property.DisplayName));
            int padding = header?.Length ?? 0;
            string? propertyValue = property.GetValue(item)?.ToString();

            if (propertyValue == null)
                return;

            bool isPercentage = property.Attributes.OfType<IsPercentageAttribute>().FirstOrDefault() != null;
            IsRarityAttribute? rarityAttribute = property.Attributes.OfType<IsRarityAttribute>().FirstOrDefault();
            IsEnergyAttribute? energyAttribute = property.Attributes.OfType<IsEnergyAttribute>().FirstOrDefault();
            ColorAttribute? colorAttribute = property.Attributes.OfType<ColorAttribute>().FirstOrDefault();
            ConsoleColor color = colorAttribute?.color ?? ConsoleColor.Gray;

            if (rarityAttribute != null)
            {
                Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), propertyValue);
                color = rarityAttribute.GetColor(rarity);
            }
            else if (energyAttribute != null)
            {
                color = energyAttribute.GetColor(int.Parse(propertyValue));
            }

            if (isPercentage)
                propertyValue += "%";

            string valueString = Utils.PadCenter($" {propertyValue} ", padding);
            Console.Write("|");
            Console.ForegroundColor = color;
            Console.Write(valueString);
            Console.ResetColor();
        }


        private void DrawLine(int width) => Console.WriteLine("+" + new string('-', width) + "+");


        private void DrawTitle(string title, int width) => Console.WriteLine("| " + Utils.PadCenter(title, width) + " |");




        /*
        public void DrawTableHorses(List<Horse> horses)
        {
        */

        /*
         * 

        for (int i = 0; i < items.Count; i++)
        {
            T horse = items[i];

            string name = Utils.PadCenter(horse.name, 17);
            string rarity = Utils.PadCenter(horse.rarity.ToString(), 10);
            string energy = Utils.PadCenter(horse.energy.ToString() + "%", 10);
            string resistance = Utils.PadCenter(horse.resistance.ToString(), 14);
            string speed = Utils.PadCenter(horse.speed.ToString(), 9);
            string age = Utils.PadCenter(horse.age.ToString(), 7);

            Console.Write($"|{name}|");

            Console.ForegroundColor = horse.RarityColor();
            Console.Write($"{rarity}");
            Console.ResetColor();
            Console.Write("|");

            Console.ForegroundColor = horse.GetEnergyColor();
            Console.Write($"{energy}");
            Console.ResetColor();
            Console.Write("|");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{resistance}");
            Console.ResetColor();
            Console.Write("|");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{speed}");
            Console.ResetColor();

            Console.WriteLine($"|{age}|");

            // gap between horses
            if (i < horses.Count - 1)
                Console.WriteLine("|                 |          |          |              |         |       |");
        }

        Console.WriteLine("+------------------------------------------------------------------------+");
        Console.WriteLine("Quantity: [" + horses.Count + "]");*/

        /*
        Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.WriteLine("|                              Player Horses                             |");
            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.WriteLine("|       Name      |  Rarity  |  Energy  |  Resistance  |  Speed  |  Age  |");
            Console.WriteLine("+------------------------------------------------------------------------+");

            for (int i = 0; i < horses.Count; i++)
            {
                Horse horse = horses[i];

                string name = Utils.PadCenter(horse.name, 17);
                string rarity = Utils.PadCenter(horse.rarity.ToString(), 10);
                string energy = Utils.PadCenter(horse.energy.ToString() + "%", 10);
                string resistance = Utils.PadCenter(horse.resistance.ToString(), 14);
                string speed = Utils.PadCenter(horse.speed.ToString(), 9);
                string age = Utils.PadCenter(horse.age.ToString(), 7);

                Console.Write($"|{name}|");

                Console.ForegroundColor = horse.RarityColor();
                Console.Write($"{rarity}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = horse.GetEnergyColor();
                Console.Write($"{energy}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{resistance}");
                Console.ResetColor();
                Console.Write("|");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{speed}");
                Console.ResetColor();

                Console.WriteLine($"|{age}|");

                // gap between horses
                if (i < horses.Count - 1)
                    Console.WriteLine("|                 |          |          |              |         |       |");
            }

            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.WriteLine("Quantity: [" + horses.Count + "]");
        }

        }*/
    }
}
