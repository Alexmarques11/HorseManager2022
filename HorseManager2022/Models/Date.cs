using HorseManager2022.Enums;
using HorseManager2022.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    /*
        In game date class, used to keep track of the date in game. Each year have 4 months, one for each season, each month have 30 days.
     */
    [Serializable]
    internal class Date
    {
        // Properties
        public int day;
        public Month month;
        public int year;

        // Constructor
        public Date(int day, Month month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Constructor for 1ºst day of the calendar
        public Date()
        {
            day = 1;
            month = Month.Spring;
            year = 1;
        }


        // Methods
        public void NextDay(GameManager gameManager)
        {
            day++;
            if (day % 7 == 0) // Refresh shop once every week
            {
                gameManager.RemoveAll<Horse, Shop>();
                gameManager.RemoveAll<Jockey, Shop>();
                gameManager.AddAll<Horse, Shop>(Horse.GenerateShopHorses());
                gameManager.AddAll<Jockey, Shop>(Jockey.GenerateShopJockeys());
            }
            
            if (day > 28)
            {
                day = 1;
                month++;
                if ((int)month > 3)
                {
                    month = Month.Spring;
                    year++;

                    // TODO: Add events for new year
                    gameManager.RemoveAll<Event, Player>();
                    gameManager.AddAll<Event, Player>(Event.GetNewYearEvents(year));
                }
            }

            // Restore 20% of energy for all horses
            Random random = new();
            foreach (Horse horse in gameManager.GetList<Horse, Player>())
                horse.energy += random.Next(Horse.ENERGY_RECOVERY_MIN, Horse.ENERGY_RECOVERY_MAX);
            
            gameManager.SaveChanges();
        }

        // Check if date is after a date
        static public bool IsDateBeforeDate(Date date1, Date date2)
        {
            if (date1.year < date2.year)
            {
                return true;
            }
            else if (date1.year == date2.year)
            {
                if (date1.month < date2.month)
                {
                    return true;
                }
                else if (date1.month == date2.month)
                {
                    if (date1.day < date2.day)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        
        // Check if date is the given month calendar page
        static public bool IsDateInThisMonth(Date date, Month month, int year) =>
            (date.year == year && date.month == month);
      

        /*
            Get the date as a string
         */
        public override string ToString()
        {
            string date = "";

            // Add year
            date += day + " ";

            // Add month
            date += ToString(month);

            // Add day
            date += " " + (year + Calendar.BASE_YEAR);
            
            return date;
        }
        
        public string? ToString(Month month) => Enum.GetName(typeof(Month), month);
    }
}
