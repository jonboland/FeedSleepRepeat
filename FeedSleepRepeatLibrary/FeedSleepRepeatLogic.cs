using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public static class FeedSleepRepeatLogic
    {
        public static List<string> AssembleFeedTypes()
        {
            var feedTypes = new List<string> { string.Empty };

            foreach (string feedType in Enum.GetNames(typeof(FeedType)))
            {
                feedTypes.Add(feedType);
            }

            return feedTypes;
        }

        public static string CalculateAge(DateTime currentBabyDateOfBirth, string today = null)
        {
            DateTime currentDate = DateTime.Now;

            // today may be set for testing purposes
            if (today != null)
            {
                currentDate = DateTime.Parse(today);
            }

            TimeSpan timeSpan = currentDate.Subtract(currentBabyDateOfBirth);
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
            DateTime truncated = pickerValue.AddTicks(-(pickerValue.Ticks % TimeSpan.TicksPerMinute));

            return truncated;
        }

        public static List<Activity> SortActivities(List<Activity> CurrentBabyDayActivities)
        {         
            return CurrentBabyDayActivities.OrderBy(a => a.Start.TimeOfDay).ToList();
        }
    }
}
