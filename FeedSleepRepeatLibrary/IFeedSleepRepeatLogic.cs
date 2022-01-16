using System;
using System.Collections.Generic;

namespace FeedSleepRepeatLibrary
{
    public interface IFeedSleepRepeatLogic
    {
        List<Baby> InsertDefaultBaby(List<Baby> babies);
        List<string> AssembleFeedTypes();
        string CalculateAge(DateTime currentBabyDateOfBirth, string today = null);
        string[] FormatName(string name);
        Activity GenerateActivityInstance(
            int babyDayId, ActivityType activityType, DateTime start, DateTime end,
            string sleepPlace = null, string feedAmount = null, string feedType = null);
        string RefreshTotalNappies(decimal wetNappiesValue, decimal dirtyNappiesValue);
        List<Activity> SortActivities(List<Activity> currentBabyDayActivities);
    }
}
