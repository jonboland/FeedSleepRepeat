using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public static class FeedSleepRepeatLogic
    {
        public static string CalculateAge(DateTime CurrentBabyDateOfBirth)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(CurrentBabyDateOfBirth);
            DateTime age = DateTime.MinValue + timeSpan;

            return $"{age.Year - 1}y {age.Month - 1}m {age.Day - 1}d";
        }

        public static string RefreshTotalNappies(decimal wetNappiesValue, decimal dirtyNappiesValue)
        {
            decimal totalNappies = wetNappiesValue + dirtyNappiesValue;

            return totalNappies.ToString();
        }

        public static DateTime TruncateTime(DateTime pickerValue)
        {
            return pickerValue.AddTicks(-(pickerValue.Ticks % TimeSpan.TicksPerMinute));
        }

        public static List<Activity> SortActivities(List<Activity> CurrentBabyDayActivities)
        {         
            return CurrentBabyDayActivities.OrderBy(a => a.Start.TimeOfDay).ToList();
        }
    }
}
