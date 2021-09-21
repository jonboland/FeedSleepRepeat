using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class Activity
    {
        public int Id { get; }
        public int BabyDayId { get; set; }
        public string ActivityType { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string FeedType { get; set; }
        public string FeedAmount { get; set; }
        public string SleepPlace { get; set; }

        public (int Hours, int Minutes) ActivityLength
        {
            get
            {
                TimeSpan diff = End - Start;

                if (diff.Hours < 0)
                {
                    diff += TimeSpan.FromDays(1);
                }

                return (diff.Hours, diff.Minutes);
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
