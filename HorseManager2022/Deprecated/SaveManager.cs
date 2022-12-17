using HorseManager2022.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Deprecated
{
    /*
    static public class SaveManagerOld
    {
        static private string rootPath => Directory.GetCurrentDirectory() + "\\saves\\";
        static private string PATH => rootPath + Game.saveName + "\\"; // replace with actual path
        static private string DELIMITER = ";"; // replace with actual delimiter


        static private T GetInstance<T>(string itemStr) where T : ISavable => (T)Activator.CreateInstance(typeof(T), new object[] { itemStr })!;


        static private string GetPath<T>() where T : ISavable => PATH + typeof(T).Name + ".txt";


        static public T? Get<T>(int id) where T : ISavable
        {
            string path = GetPath<T>();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                T _item = GetInstance<T>(lines[i]);

                if (_item.id == id)
                    return _item;
            }

            return default;
        }


        static public List<T> Get<T>() where T : ISavable
        {
            string path = GetPath<T>();
            string[] lines = File.ReadAllLines(path);
            List<T> items = new();

            foreach (string itemStr in lines)
            {
                items.Add(GetInstance<T>(itemStr));
            }

            return items;
        }


        static public void Add<T>(T item) where T : ISavable
        {
            string path = GetPath<T>();
            item.id = GetAutoIncrementId<T>();
            string itemStr = item.ToSaveFormat();

            File.AppendAllText(path, itemStr + Environment.NewLine);
        }


        static public void Update<T>(T item) where T : ISavable
        {
            string path = GetPath<T>();
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                T _item = GetInstance<T>(lines[i]);

                if (_item.id == item.id)
                {
                    lines[i] = item.ToSaveFormat();
                }
            }

            File.WriteAllLines(path, lines);
        }


        static private int GetAutoIncrementId<T>() where T : ISavable
        {
            string path = GetPath<T>();
            string[] lines = File.ReadAllLines(path);

            try
            {
                return GetInstance<T>(lines[^1]).id + 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }*/
}