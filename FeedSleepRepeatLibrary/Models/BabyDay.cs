using System;
using System.Collections.Generic;

namespace FeedSleepRepeatLibrary
{
    public class BabyDay
    {
        public int Id { get; }
        public int BabyId { get; set; }
        public DateTime Date { get; set; }
        public string Weight { get; set; }
        public decimal WetNappies { get; set; }
        public decimal DirtyNappies { get; set; }
        // Initialization prevents null exception when AddActivity() is called
        public List<Activity> Activities { get; set; } = new();
    }
}
