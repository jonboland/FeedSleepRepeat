using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class BabyDay
    {
        public int Id { get; }
        public int BabyId { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Weight { get; set; }
        public decimal WetNappies { get; set; } = 0;
        public decimal DirtyNappies { get; set; } = 0;
        public List<Feed> Feeds { get; set; } = new List<Feed>();
        public List<Sleep> Sleeps { get; set; } = new List<Sleep>();
    }
}
