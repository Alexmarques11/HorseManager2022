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
        public Date date;

        // Constructors
        public Event(string name, EventType type, Date date)
        {
            this.name = name;
            this.type = type;
            this.date = date;
        }

        public Event(List<Event>? events = null)
        {
            Random random = new();
            int randomType = random.Next(1, 3);
            type = (EventType)randomType;

            name = GenerateName();
            
            do
            {
                date = GenerateDate();
            } while (HasEventsOnDate(events, date));
        }
        

        // Methods
        public ConsoleColor GetEventTypeColor()
        {
            switch (type)
            {
                case EventType.Race:
                    return ConsoleColor.Red;
                case EventType.Demostration:
                    return ConsoleColor.Magenta;
                case EventType.Holiday:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.Gray;
            }
        }

        
        // File Crud Methods
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
                events.Add(new(events));

            return events;
        }
        

        private bool HasEventsOnDate(List<Event>? events, Date date) => (events != null) && events.Any(e => e.date.ToString() == date.ToString());


        // Generate Random data Methods
        private Date GenerateDate()
        {
            // Generate random date
            Random random = new();
            int randomDay = random.Next(1, 29);
            int randomMonth = random.Next(0, 4);
            
            // Create date
            Date randomDate = new(randomDay, (Month)randomMonth, 1);
            return randomDate;
        }

        
        private string GenerateName()
        {
            // Variables
            Random random = new Random();

            string[] list = {
                "Maverick Team",
                "Five Go Event",
                "Sage Glamorous Encore",
                "Fête Gold Happen",
                "Bonfire Green Ranch",
                "Perfect Sensations",
                "Proteus Live Entertainment",
                "Scenario Productions",
                "Crystal Eventualities",
                "Fête Bird",
                "Organically Attainment",
                "Fête Gold Happen",
                "Bonfire Red Living",
                "Every Science",
                "Land Planning",
                "Dazzling Of Evermore",
                "Divine Scenes",
                "Prince Lucky",
                "Glowing Happen",
                "Fourplan Event InStyle",
                "Little Happen",
                "Carpe Diem By Company",
                "Shindig Red Inc",
                "The Circumstances",
                "Seven Delight",
                "Sunset Good Logic",
                "Pomp Company",
                "Vintage Team",
                "Blowout Your Tree",
                "Proteus Live Entertainment",
                "Castles Together Prep",
                "Enchanting The Eventerprises",
                "Divine Main Paradise",
                "Famous Blue Touch",
                "Cirque Bird",
                "Dreams Big Factor",
                "Princess Curators",
                "Staging Shindigger",
                "Classy Factor",
                "Gold Décor",
                "Princess Touch",
                "Classy Factor",
                "Oh How Scenarios",
                "Organization Dream Sensations",
                "Crystal Your InStyle",
                "Dreams Gurus",
                "Big E Up Scenes",
            };

            return list[random.Next(0, list.Length)];
        }

    }
}