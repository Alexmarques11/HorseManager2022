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
        public List<T> GetList<T>() 
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

        
        public T? Get<T>(int id) where T : IIdentifiable => GetList<T>().FirstOrDefault(x => x.id == id);
        

        public void Add<T>(T item) where T : IIdentifiable
        {
            List<T> list = GetList<T>();
            item.id = GetNewId<T>(list);
            GetList<T>().Add(item);

            SaveChanges();
        }


        public void AddAll<T>(List<T> items) where T : IIdentifiable
        {
            // Set auto increment ids
            foreach (T item in items) {
                item.id = GetNewId<T>(items);
            }

            List<T> list = GetList<T>();
            list.AddRange(items);

            SaveChanges();
        }


        public void Update<T>(T item) where T : IIdentifiable
        {
            List<T> list = GetList<T>();
            T? _item = list.FirstOrDefault(x => x.id == item.id);

            if (_item != null)
                list[list.IndexOf(_item)] = item;

            SaveChanges();
        }


        public void Remove<T>(T item)
        {
            GetList<T>().Remove(item);

            SaveChanges();
        }


        public void Remove<T>(int id) where T : IIdentifiable
        {
            List<T> list = GetList<T>();
            T? _item = GetList<T>().FirstOrDefault(x => x.id == id);

            if (_item != null)
                list.Remove(_item);

            SaveChanges();
        }


        public void RemoveAll<T>() => GetList<T>().Clear();


        static public int GetNewId<T>(List<T> list) where T : IIdentifiable
        {
            if (list == null || list.Count == 0)
                return 1;

            int maxId = list.Max(x => x.id);
            return maxId + 1;
        }


        private void EmptySave() => gameData = new();


        public void SaveChanges() => saveManager.SaveGame(this.gameData);
        

        public void CreateNewSave(string savename)
        {
            EmptySave();
            saveManager.saveName = savename;
            AddAll(Event.GetNewYearEvents());
            gameData.money = 10;
            gameData.currentDate = new();
            SaveChanges();
        }

    }
}
