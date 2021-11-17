using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedSleepRepeatLibrary
{
    public static class FeedSleepRepeatLogic
    {
        /// <summary>
        /// Inserts a default baby at the start of a list of babies.
        /// </summary>
        /// <param name="babies">List of baby instances.</param>
        /// <returns>List of baby instances with default baby added.</returns>
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
        /// Splits a name string and removes any whitespace characters.
        /// </summary>
        /// <param name="name">The baby name in string format.</param>
        /// <returns>Array of strings containing baby's first and last name.</returns>
        public static string[] FormatName(string name)
        {
            return name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Calculates a baby's age based on its date of birth.
        /// </summary>
        /// <param name="currentBabyDateOfBirth">DateTime representing a baby's date of birth.</param>
        /// <param name="today">String used to define today's date for testing purposes.</param>
        /// <returns>Baby's age in string format.</returns>
        public static string CalculateAge(DateTime currentBabyDateOfBirth, string today = null)
        {
            DateTime currentDate = DateTime.Today;

            if (today != null)
            {
                currentDate = DateTime.Parse(today);
            }

            int months = currentDate.Month - currentBabyDateOfBirth.Month;
            int years = currentDate.Year - currentBabyDateOfBirth.Year;

            if (currentDate.Day < currentBabyDateOfBirth.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (currentDate - currentBabyDateOfBirth.AddMonths((years * 12) + months)).Days;

            return $"{years}y {months}m {days}d";
        }

        /// <summary>
        /// Totals the number of wet and dirty nappies.
        /// </summary>
        /// <param name="wetNappiesValue">Decimal value representing number of wet nappies.</param>
        /// <param name="dirtyNappiesValue">Decimal value representing number of dirty nappies.</param>
        /// <returns>A string representing the total number of nappies.</returns>
        public static string RefreshTotalNappies(decimal wetNappiesValue, decimal dirtyNappiesValue)
        {
            decimal totalNappies = wetNappiesValue + dirtyNappiesValue;

            return totalNappies.ToString();
        }

        /// <summary>
        /// Compile list of feedtypes from FeedType enum for use in feedtype dropdown menu.
        /// </summary>
        /// <returns>List of feedtypes in string format with blank string at index 0.</returns>
        public static List<string> AssembleFeedTypes()
        {
            var feedTypes = new List<string> { string.Empty };

            foreach (string feedType in Enum.GetNames(typeof(FeedType)))
            {
                feedTypes.Add(feedType);
            }

            return feedTypes;
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
            int babyDayId, ActivityType activityType, DateTime start, DateTime end,
            string sleepPlace = null, string feedAmount = null, string feedType = null)
        {
            var activity = new Activity 
            { 
                BabyDayId = babyDayId, 
                ActivityType = activityType,
                Start = TruncateTime(start),
                End = TruncateTime(end),
                FeedType = feedType,
                FeedAmount = feedAmount,
                SleepPlace = sleepPlace,
            };

            activity.End = AddDayIfEndBeforeStart(activity.Start, activity.End);

            return activity;
        }

        /// <summary>
        /// Removes seconds, milliseconds and ticks from a DateTime object.
        /// </summary>
        /// <param name="pickerValue">DateTime object to be truncated.</param>
        /// <returns>A truncated DateTime object.</returns>
        private static DateTime TruncateTime(DateTime pickerValue)
        {
            DateTime truncated = pickerValue.AddTicks(-(pickerValue.Ticks % TimeSpan.TicksPerMinute));

            return truncated;
        }

        /// <summary>
        /// Adds one day to the End DateTime object if its value is earlier than the Start object's value.
        /// </summary>
        /// <param name="Start">DateTime object representing when an activity started.</param>
        /// <param name="End">DateTime object representing when an activity ended.</param>
        /// <returns>Processed DateTime object representing when an activity ended.</returns>
        private static DateTime AddDayIfEndBeforeStart(DateTime Start, DateTime End)
        {
            if (End < Start)
            {
                End = End.AddDays(1);
            }

            return End;
        }

        /// <summary>
        /// Sorts a list of activity instances by the time of day of the Start property.
        /// </summary>
        /// <param name="CurrentBabyDayActivities">List of activity instances prior to sorting.</param>
        /// <returns>Sorted list of activity instances.</returns>
        public static List<Activity> SortActivities(List<Activity> CurrentBabyDayActivities)
        {
            return CurrentBabyDayActivities.OrderBy(a => a.Start.TimeOfDay).ToList();
        }
    }
}
