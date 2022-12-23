using HorseManager2022.Enums;
using HorseManager2022.Interfaces;
using HorseManager2022.Models;
using System.Reflection;

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
        
        
        public Date currentDate
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
        public List<T> GetList<T, U>()
        {
            Type storageType = typeof(U);
            Type listType = typeof(T);
            PropertyInfo? storageProperty;
            
            var storage = gameData.GetType().GetProperty(storageType.Name.ToLower()).GetValue(gameData);
            storageProperty = storage.GetType().GetProperty(listType.Name.ToLower() + "s");
            var list = storageProperty.GetValue(storage) as List<T>;

            return list;
        }
        

        public void Add<T, U>(T item)
        {
            List<T> list = GetList<T, U>();
            list.Add(item);

            SaveChanges();
        }


        public void AddAll<T, U>(List<T> items)
        {
            List<T> list = GetList<T, U>();
            list.AddRange(items);

            SaveChanges();
        }


        public void Update<T, U>(T item)
        {
            List<T> list = GetList<T, U>();
            T? _item = list.FirstOrDefault(x => x.Equals(item));

            if (_item != null)
                list[list.IndexOf(_item)] = item;

            SaveChanges();
        }


        public void Remove<T, U>(T item)
        {
            GetList<T, U>().Remove(item);

            SaveChanges();
        }


        public void RemoveAll<T, U>() => GetList<T, U>().Clear();

        
        private void EmptySave() => gameData = new();


        public void SaveChanges() => saveManager.SaveGame(this.gameData);


        public bool Exchange<T, U>(T item) where T : IExchangeable
        {
            // Exchange failure
            if (item.price > money)
                return false;

            if (typeof(U) == typeof(Shop))
                money -= item.price;
            else
                money += item.price;

            if (typeof(U) == typeof(Shop))
                Add<T, Player>(item);
            else
                Add<T, Shop>(item);

            Remove<T, U>(item);

            // Exchange completed
            return true;
        }


        public void CreateNewSave(string savename)
        {
            EmptySave();
            saveManager.saveName = savename;
            AddAll<Event, Player>(Event.GetNewYearEvents());
            AddAll<Horse, Shop>(Horse.GenerateShopHorses());
            AddAll<Jockey, Shop>(Jockey.GenerateShopJockeys());

            gameData.money = 1000000;
            gameData.currentDate = new();
            SaveChanges();
        }

    }
}
