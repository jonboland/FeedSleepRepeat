using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class Feed
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
    }
}
