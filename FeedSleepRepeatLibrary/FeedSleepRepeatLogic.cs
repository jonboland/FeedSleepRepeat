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

        public static List<Activity> SortActivities(List<Activity> CurrentBabyDayActivities)
        {         
            return CurrentBabyDayActivities.OrderBy(a => a.Start.TimeOfDay).ToList();
        }

        public static string[] FormatName(string name)
        {
            return name.Trim().Split();
        }

        public static List<Baby> InsertDefaultBaby(List<Baby> babies)
        {
            babies.Insert(0, new Baby()
            {
                FirstName = String.Empty,
                LastName = String.Empty,
                DateOfBirth = DateTime.Today.Date,
            });

            return babies;
        }

        /// <summary>
        /// Generates a feed or sleep activity instance and sets the instance's BabyDayId 
        /// to the current baby day's Id.
        /// 
        /// The Id will be 0 if the current baby day hasn't yet been created.
        /// (The correct Id will then be inserted into the activity when it's added to the database - 
        /// at the point that the update button is clicked to update the baby's records.)
        /// </summary>
        /// <returns>An activity instance of type feed or sleep, populated with activity data.</returns>
        public static Activity GenerateActivityInstance(
            ActivityType activityType, int dayId, 
            DateTime sleepStart = default, DateTime sleepEnd = default, string sleepPlace = null,
            DateTime feedStart = default, DateTime feedEnd = default, string feedAmount = null, string feedType = null)
        {
            var activity = new Activity { BabyDayId = dayId, ActivityType = activityType };

            if (activityType == ActivityType.Sleep)
            {
                activity.Start = TruncateTime(sleepStart);
                activity.End = TruncateTime(sleepEnd);
                activity.SleepPlace = sleepPlace;
            }
            else
            {
                activity.Start = TruncateTime(feedStart);
                activity.End = TruncateTime(feedEnd);
                activity.FeedAmount = feedAmount;
                activity.FeedType = feedType;
            }

            activity.End = AddDayIfEndBeforeStart(activity.Start, activity.End);

            return activity;
        }

        private static DateTime TruncateTime(DateTime pickerValue)
        {
            DateTime truncated = pickerValue.AddTicks(-(pickerValue.Ticks % TimeSpan.TicksPerMinute));

            return truncated;
        }

        private static DateTime AddDayIfEndBeforeStart(DateTime Start, DateTime End)
        {
            if (End < Start)
            {
                End = End.AddDays(1);
            }

            return End;
        }
    }
}
