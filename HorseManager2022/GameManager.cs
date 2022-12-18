using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022
{
    internal class GameManager
    {

        // Properties
        public GameData gameData { get; private set; }
        public SaveManager saveManager { get; set; }

        // Getters & Setters
        public int money
        {
            get => gameData.money;

            set
            {
                gameData.money = value;
                SaveChanges();
            }
        }
        
        public Date? currentDate
        {
            get => gameData.currentDate;

            set
            {
                gameData.currentDate = value;
                SaveChanges();
            }
        }

        public string saveName
        {
            get => saveManager.saveName;
            set
            {
                saveManager.saveName = value;
                EmptySave();
                gameData = saveManager.LoadGame() ?? new();
            }
        }

        public GameManager()
        {
            gameData = new();
            saveManager = new();
        }


        // Methods
        public List<T> GetList<T>(string? listName = null) 
        {
            if (listName == null)
            {
                return typeof(T) switch
                {
                    Type t when t == typeof(Horse) => gameData.horses as List<T> ?? new List<T>(),
                    Type t when t == typeof(Event) => gameData.events as List<T> ?? new List<T>(),
                    Type t when t == typeof(Jockey) => gameData.joqueys as List<T> ?? new List<T>(),
                    Type t when t == typeof(Team) => gameData.teams as List<T> ?? new List<T>(),
                    _ => throw new Exception("Type not found"),
                };
            }
            else 
            { 
                return listName.ToLower() switch
                {
                    "horses" => gameData.horses as List<T> ?? new List<T>(),
                    "events" => gameData.events as List<T> ?? new List<T>(),
                    "joqueys" => gameData.joqueys as List<T> ?? new List<T>(),
                    "teams" => gameData.teams as List<T> ?? new List<T>(),
                    "shophorses" => gameData.shopHorses as List<T> ?? new List<T>(),
                    _ => throw new Exception("List not found"),
                };
            }
        }
        

        public void Add<T>(T item, string? listName = null)
        {
            List<T> list = GetList<T>(listName);
            list.Add(item);

            SaveChanges();
        }


        public void AddAll<T>(List<T> items, string? listName = null)
        {
            List<T> list = GetList<T>(listName);
            list.AddRange(items);

            SaveChanges();
        }


        public void Update<T>(T item, string? listName = null)
        {
            List<T> list = GetList<T>(listName);
            T? _item = list.FirstOrDefault(x => x.Equals(item));

            if (_item != null)
                list[list.IndexOf(_item)] = item;

            SaveChanges();
        }


        public void Remove<T>(T item, string? listName = null)
        {
            GetList<T>(listName).Remove(item);

            SaveChanges();
        }


        public void RemoveAll<T>(string? listName = null) => GetList<T>(listName).Clear();

        
        private void EmptySave() => gameData = new();


        public void SaveChanges() => saveManager.SaveGame(this.gameData);
        

        public void CreateNewSave(string savename)
        {
            EmptySave();
            saveManager.saveName = savename;
            AddAll(Event.GetNewYearEvents());
            AddAll(Horse.GenerateShopHorses(), "shopHorses");
            gameData.money = 10;
            gameData.currentDate = new();
            SaveChanges();
        }

    }
}
