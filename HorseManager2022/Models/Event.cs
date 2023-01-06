using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseManager2022.Deprecated;
using HorseManager2022.Enums;
using HorseManager2022.Interfaces;

namespace HorseManager2022.Models
{
    [Serializable]
    internal class Event
    {
        // Constants
        private const int EVENT_QUANTITY_YEAR = 20;

        // Properties
        public string name;
        public EventType type;
        public Difficulty? difficulty;
        public Date date;

        
        // Constructors
        public Event(string name, EventType type, Date date, Difficulty? difficulty = null)
        {
            this.name = name;
            this.type = type;
            this.difficulty = difficulty;
            this.date = date;
        }

        
        // Random event Constructor
        public Event(List<Event>? events = null, int year = 1)
        {
            type = (EventType)GameManager.GetRandomInt(1, 3);
            difficulty = DifficultyExtensions.GetRandomDifficulty();
            name = $"{difficulty} {type}"; //GenerateName();

            do
            {
                date = GenerateDate(year);
            } while (HasEventsOnDate(events, date));
        }


        // Methods
        public ConsoleColor GetEventColor()
        {
            if (type == EventType.Holiday)
                return ConsoleColor.Cyan;
            else
                return DifficultyExtensions.GetColor(difficulty);
        }

        
        public int GetEntryCost()
        {
            if (type != EventType.Race)
                return 0;

            return difficulty switch
            {
                Difficulty.Easy => 100,
                Difficulty.Normal => 200,
                Difficulty.Hard => 400,
                Difficulty.Extreme => 700,
                _ => 0,
            };
        }


        // Return the reward money value based on participants count and race difficulty
        public int GetReward(int teamQuantity)
        {
            int reward = 0;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    reward = 100;
                    break;
                case Difficulty.Normal:
                    reward = 200;
                    break;
                case Difficulty.Hard:
                    reward = 400;
                    break;
                case Difficulty.Extreme:
                    reward = 700;
                    break;
            }

            if (type == EventType.Demostration) reward /= 2;

            return reward * teamQuantity;
        }


        static public Event? GetTodayEvent(GameManager? gameManager)
        {
            if (gameManager == null)
                return null;

            Date currentDate = gameManager.currentDate;
            List<Event> events = gameManager.GetList<Event, Player>();
            foreach (var @event in events)
            {
                if (@event.date.day == currentDate.day
                    && @event.date.month == currentDate.month
                    && @event.date.year == currentDate.year)
                    return @event;
            }

            return null;
        }

        
        static public List<Event> GetNewYearEvents(int year = 1)
        {
            
            // Add Holiday Events
            List<Event> events = new()
            {
                new("New Year", EventType.Holiday, new(1, Month.Spring, year)),
                new("Easter", EventType.Holiday, new(15, Month.Spring, year)),
                new("Thanksgiving", EventType.Holiday, new(23, Month.Summer, year)),
                new("Diwali", EventType.Holiday, new(7, Month.Summer, year)),
                new("Halloween", EventType.Holiday, new(21, Month.Autumn, year)),
                new("Black Friday", EventType.Holiday, new(28, Month.Autumn, year)),
                new("Hanukkah", EventType.Holiday, new(18, Month.Winter, year)),
                new("Christmas", EventType.Holiday, new(25, Month.Winter, year))
            };

            // Add Random Events
            for (int i = 0; i < EVENT_QUANTITY_YEAR; i++)
                events.Add(new(events, year));

            return events;
        }
        

        private bool HasEventsOnDate(List<Event>? events, Date date) => (events != null) && events.Any(e => e.date.ToString() == date.ToString());


        // Generate Random data Methods
        private Date GenerateDate(int year = 1)
        {
            // Generate random date
            int randomDay = GameManager.GetRandomInt(1, 29);
            int randomMonth = GameManager.GetRandomInt(0, 4);
            
            // Create date
            Date randomDate = new(randomDay, (Month)randomMonth, year);
            return randomDate;
        }


        public override string ToString()
        {
            return $"{name} - {date}";
        }

    }
}