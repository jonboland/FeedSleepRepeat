using System;
using System.Collections.Generic;

namespace FeedSleepRepeatLibrary
{
    public class Baby
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        // Initialization prevents null exception when datePicker value change sets CurrentBabyDay
        public List<BabyDay> BabyDays { get; set; } = new();

        public string FullName
        {
            get
            {
                // Trimmed so a blank (default) baby name contains no whitespace
                return $"{FirstName} {LastName}".Trim();
            }
        }
    }
}
