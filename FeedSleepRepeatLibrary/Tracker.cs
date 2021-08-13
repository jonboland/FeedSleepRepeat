using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class Tracker
    {
        public Tracker()
        {
            Name = "FeedSleepRepeat";
            Babies = new List<Baby>();
        }

        public string Name { get; set; }
        public List<Baby> Babies { get; set; }
    }
}
