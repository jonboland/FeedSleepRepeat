using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class Feed : Activity
    {
        public string Amount { get; set; }
        public string Type { get; set; }

        public string ActivitySummary
        {
            get
            {
                return $"Activity Type: Feed   Start: {Start:HH:mm}   End: {End:HH:mm}   Amount: {Amount}ml   Feed Type: {Type}";
            }
        }
    }
}
