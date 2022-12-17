using HorseManager2022.Enums;
using HorseManager2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.UI
{
    internal class Calendar
    {
        // Constants
        static readonly public int BASE_YEAR = 2021;
        private const int DAYS_IN_WEEK = 7;
        private const int WEEKS_IN_MONTH = 4;
        private const int DEFAULT_MOUSE_POSITION_Y = 22;

        // Properties
        public List<Event> events { get; set; }
        public Date currentDate { get; set; }

        // Constructor
        public Calendar(Date? currentDate, List<Event> events)
        {
            this.currentDate = currentDate ?? new Date();
            this.events = events;
        }
        
        public Calendar()
        {
            events = new List<Event>();
            currentDate = new();
        }

        // Methods
        public void AddEvent(string name, EventType type, Date date) =>
            events.Add(new Event(name, type, date));

        
        public void AddEvent(List<Event> events) => this.events.AddRange(events);


        public void ClearEvents() => events.Clear();

        
        public void Show(Month monthPage, int yearPage, int page)
        {
            ShowCalendar(monthPage, yearPage, page);
            ShowEvents(monthPage, yearPage);
            SetMouseDefaultPosition();
        }


        private void ShowCalendar(Month monthPage, int yearPage, int page)
        {
            // Set title
            string title = monthPage + " " + (yearPage + BASE_YEAR);
            title = title.PadLeft((30 / 2) + (title.Length / 2)).PadRight(30);

            // Draw previous output but mark current day and events
            Console.WriteLine("+------------------------------+");
            Console.WriteLine("|" + title + "|");
            Console.WriteLine("|------------------------------|");
            Console.WriteLine("|  Su  Mo  Tu  We  Th  Fr  Sa  |");
            Console.WriteLine("|                              |");
            
            int day = 1;
            for (int i = 1; i <= WEEKS_IN_MONTH; i++) 
            {
                Console.Write("| ");
                
                for (int j = 1; j <= DAYS_IN_WEEK; j++)
                    Console.Write(GetDayInCalendar(day++, monthPage, yearPage));

                Console.ResetColor();
                Console.WriteLine(" |");
                Console.WriteLine("|                              |");
            }

            Console.WriteLine("+------------------------------+");
            Console.WriteLine("[<] Back [>] Next     [Page " + (page + 1) + "/4]");
            Console.WriteLine();

        }
        

        private string GetDayInCalendar(int day, Month month, int year)
        {
            Console.ResetColor();
            string result, resultFormmated;

            // Day less than 10 check
            if (day < 10)
                result = "0" + day;
            else
                result = day.ToString();
            
            Event? eventInDay = GetEventInDay(day, month, year);
            if (eventInDay != null)
                Console.ForegroundColor = eventInDay.GetEventTypeColor();

            // Event in day check
            if (eventInDay != null)
                resultFormmated = "[" + result + "]";
            else
                resultFormmated = " " + result + " ";

            // Day equal to current day check
            if (day == currentDate.day && month == currentDate.month && year == currentDate.year)
            {
                if (eventInDay == null)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                resultFormmated = "(" + result + ")";
            }

            if (Date.IsDateBeforeDate(new Date(day, month, year), currentDate))
                Console.ForegroundColor = ConsoleColor.DarkGray;
            
            return resultFormmated;
        }

        
        private Event? GetEventInDay(int day, Month month, int year)
        {
            foreach (Event e in events)
                if (e.date.day == day && e.date.month == month && e.date.year == year)
                    return e;

            return null;
        }

        
        private void ShowEvents(Month monthPage, int yearPage)
        {
            // Draw Event List on the right
            int x = 34, y = Topbar.TOPBAR_HEIGHT;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("+--------------------------------------+");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|                Events                |");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|--------------------------------------|");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|                                      |");

            // Order events by day
            List<Event> orderedEvents = events.OrderBy(e => e.date.day).ToList();

            // Display Events
            int pos = 4;
            foreach (Event e in orderedEvents)
            {
                // Check if the event is in the current month and year and not passed
                if (!Date.IsDateInThisMonth(e.date, monthPage, yearPage) ||
                    Date.IsDateBeforeDate(e.date, currentDate))
                    continue;
                
                // Get Line values
                string eventName = e.name.PadRight(29);
                string day = e.date.day.ToString();
                if (e.date.day < 10)
                    day = "0" + day;

                // Display Values
                Console.SetCursorPosition(x, y + pos);
                Console.Write("| ");
                Console.ForegroundColor = e.GetEventTypeColor();
                Console.Write("•");
                Console.ResetColor();
                Console.WriteLine(" " + day + " -> " + eventName + "|");
                Console.SetCursorPosition(x, y + pos + 1);
                Console.WriteLine("|                                      |");
                pos += 2;
            }

            // no events message
            if (pos == 4)
            {
                Console.SetCursorPosition(x, y + pos);
                Console.WriteLine("| No more events in this month! :c     |");
                Console.SetCursorPosition(x, y + pos + 1);
                Console.WriteLine("|                                      |");
                pos += 2;
            }

            // Display Bottom List
            Console.SetCursorPosition(x, y + pos);
            Console.WriteLine("+--------------------------------------+");
        }

        
        private void SetMouseDefaultPosition() =>
            Console.SetCursorPosition(0, DEFAULT_MOUSE_POSITION_Y);



    }
}
