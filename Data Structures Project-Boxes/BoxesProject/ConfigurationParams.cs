using System;
using System.Collections.Generic;
using System.Text;

namespace BoxesProject
{
    public class ConfigurationParams
    {  
        public ConfigurationParams(int maxCapacityPerBox, double searchDivMargin, int alertUnitLimit, 
            int expirationInterval, string deleteTimeOfDay)
        {
            MaxCapacityPerBox = maxCapacityPerBox;
            SearchDivMargin = searchDivMargin;
            AlertUnitLimit = alertUnitLimit;
            ExpirationInterval = expirationInterval;
            DeleteTimeOfDay = CovertStringToDateTime(deleteTimeOfDay);
        }
        public int MaxCapacityPerBox { get; set; }
        public double SearchDivMargin { get; set; }
        public int AlertUnitLimit { get; set; }
        public int ExpirationInterval { get; set; } // in days
        public DateTime DeleteTimeOfDay { get; private set; }//"HH:MM" 24 hour clock
        private DateTime CovertStringToDateTime(string deleteTimeOfDay)
        {
            string[] hourParts = deleteTimeOfDay.Split(':');
            if (int.TryParse(hourParts[0], out int partHour) && partHour >= 0 && partHour <= 23 &&
                int.TryParse(hourParts[1], out int partMinutes) && partMinutes >= 0 && partMinutes <= 59)
            {
                DateTime deleteTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, partHour, partMinutes, 0);
                if (deleteTime.Subtract(DateTime.Now).TotalHours < 0)
                    deleteTime = deleteTime.AddDays(1);
                return deleteTime;
            }
            else
                throw new ArgumentException("deleteTimeOfDay should be in format HH:MM 24 hour clock");
        }
        public void SetDeleteTimeOfDay (string deleteTimeOfDay)
        {
            DeleteTimeOfDay = CovertStringToDateTime(deleteTimeOfDay);
        }

    }
}
