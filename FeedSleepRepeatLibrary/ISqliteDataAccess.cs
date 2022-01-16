using System.Collections.Generic;

namespace FeedSleepRepeatLibrary
{
    public interface ISqliteDataAccess
    {
        void CreateBaby(Baby baby, BabyDay babyDay);
        void CreateBabyDay(BabyDay babyDay);
        void DeleteBaby(Baby baby);
        List<Activity> LoadActivities(BabyDay currentBabyDay);
        List<Baby> LoadBabies();
        List<BabyDay> LoadBabyDays(Baby currentBaby);
        void UpdateBabyDay(BabyDay babyDay);
        void UpdateDateOfBirth(Baby baby);
    }
}
