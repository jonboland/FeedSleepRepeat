using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class BabyDay
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public int Weight { get; set; }
        public int WetNappies { get; set; } = 0;
        public int DirtyNappies { get; set; } = 0;
        public List<Feed> Feeds { get; set; } = new List<Feed>();
        public List<Sleep> Sleeps { get; set; } = new List<Sleep>();
    }
}
