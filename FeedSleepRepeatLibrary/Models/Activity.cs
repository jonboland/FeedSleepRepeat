using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class Activity
    {
        public int BabyDayId { get; set; }
        public string ActivityType { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string FeedAmount { get; set; }
        public string FeedType { get; set; }
        public string SleepPlace { get; set; }

        public (int Hours, int Minutes) ActivityLength
        {
            get
            {
                TimeSpan diff = End - Start;
                TimeSpan roundedDiff = TimeSpan.FromMinutes(Math.Round(diff.TotalMinutes));

                if (roundedDiff.Hours < 0)
                {
                    roundedDiff += TimeSpan.FromDays(1);
                }

                return (roundedDiff.Hours, roundedDiff.Minutes);
            }
        }

        public string FeedUnit
        {
            get
            {
                return FeedType == "Solid" ? "g" : "ml";
            }
        }

        public string ActivitySummary
        {
            get
            {
                if (ActivityType == "Sleep")
                {
                    return $" Activity Type: {ActivityType}    Start: {Start:HH:mm}  End: {End:HH:mm}     "
                        + $"Length: {ActivityLength.Hours}h {ActivityLength.Minutes}m\tPlace: {SleepPlace}";
                }
                
                return $" Activity Type: {ActivityType}     Start: {Start:HH:mm}  End: {End:HH:mm}     "
                    + $"Length: {ActivityLength.Hours}h {ActivityLength.Minutes}m\t"
                    + $"Feed Type: {FeedType}\tAmount: {FeedAmount}{FeedUnit}";
            }
        }

    }
}
