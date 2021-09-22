using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FeedSleepRepeatLibrary;

namespace FeedSleepRepeatUI
{
    partial class FeedForm
    {
        private void SetDatePickerMaxValues()
        {
            dateOfBirthPicker.MaxDate = DateTime.Today;
            datePicker.MaxDate = DateTime.Today;
        }

        private void AddActivitiesKeyDownEventHandler()
        {
            activitiesListBox.KeyDown += new KeyEventHandler(activitiesListBox_KeyDown);
        }

        private void ResetFeedValues()
        {
            feedStartPicker.Value = DateTime.Now;
            feedEndPicker.Value = DateTime.Now;
            feedAmountBox.Text = String.Empty;
            feedTypeCombo.SelectedItem = String.Empty;
        }

        private void ResetSleepValues()
        {
            sleepStartPicker.Value = DateTime.Now;
            sleepEndPicker.Value = DateTime.Now;
            sleepPlaceBox.Text = String.Empty;
        }

        private void ResetBabyDayValues()
        {
            weightBox.Text = string.Empty;
            wetNappiesNumericUpDown.Value = 0;
            dirtyNappiesNumericUpDown.Value = 0;
            nappiesTotal.Text = "0";
            activitiesListBox.DataSource = null;
            ResetFeedValues();
            ResetSleepValues();
        }
        
        private void ResetAllValues()
        {
            CurrentBaby = null;
            SetDatePickerMaxValues();
            dateOfBirthPicker.Value = DateTime.Today.Date;
            ageBox.Text = "0y 0m 0d";
            datePicker.Value = DateTime.Today.Date;
            ResetBabyDayValues();
        }

        public void LoadBabyList()
        {
            Babies = SqliteDataAccess.LoadBabies();
            // Inserts default baby that can be used to reset all values
            Babies.Insert(0, new Baby()
            {
                FirstName = String.Empty,
                LastName = String.Empty,
                DateOfBirth = DateTime.Today.Date,
            });
        }

        private void ConnectBabyNameCombo()
        {
            babyNameCombo.DataSource = null;
            babyNameCombo.DisplayMember = "FullName";
            babyNameCombo.DataSource = Babies;
        }

        private void RefreshBabyDayValues(BabyDay babyDay)
        {
            weightBox.Text = babyDay.Weight;
            wetNappiesNumericUpDown.Value = babyDay.WetNappies;
            dirtyNappiesNumericUpDown.Value = babyDay.DirtyNappies;
            nappiesTotal.Text = RefreshTotalNappies();
        }

        private string RefreshTotalNappies()
        {
            decimal totalNappies = wetNappiesNumericUpDown.Value + dirtyNappiesNumericUpDown.Value;
            return totalNappies.ToString();
        }

        private void AddFeedTypeDropdownValues()
        {
            feedTypeCombo.DataSource = new List<string> { String.Empty, "Bottle", "Breast", "Solid" };
        }

        private Baby GenerateBabyInstance()
        {
            string[] name = babyNameCombo.Text.Split();

            Baby baby = new()
            {
                FirstName = name[0],
                LastName = name[1],
                DateOfBirth = dateOfBirthPicker.Value.Date,
            };

            return baby;
        }

        private string CalculateAge(DateTime dateOfBirth)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateOfBirth);
            DateTime age = DateTime.MinValue + timeSpan;
            return $"{age.Year - 1}y {age.Month - 1}m {age.Day - 1}d";
        }

        private void UpdateSelectedBabyDay(BabyDay currentBabyDay)
        {
            currentBabyDay.Date = datePicker.Value;
            currentBabyDay.Weight = weightBox.Text;
            currentBabyDay.WetNappies = wetNappiesNumericUpDown.Value;
            currentBabyDay.DirtyNappies = dirtyNappiesNumericUpDown.Value;
        }
        
        private BabyDay GenerateBabyDayInstance()
        {
            BabyDay day = new()
            {
                Date = datePicker.Value,
                Weight = weightBox.Text,
                WetNappies = wetNappiesNumericUpDown.Value,
                DirtyNappies = dirtyNappiesNumericUpDown.Value,
            };

            return day;
        }

        private Activity GenerateFeedActivityInstance()
        {
            Activity feed = new()
            {
                ActivityType = "Feed",
                // TODO: Create time truncation extension method
                Start = feedStartPicker.Value.AddTicks(-(feedStartPicker.Value.Ticks % TimeSpan.TicksPerMinute)),
                End = feedEndPicker.Value.AddTicks(-(feedEndPicker.Value.Ticks % TimeSpan.TicksPerMinute)),
                FeedAmount = feedAmountBox.Text,
                FeedType = feedTypeCombo.Text,
            };

            if (CurrentBabyDay != null)
            {
                feed.BabyDayId = CurrentBabyDay.Id;
            }

            return feed;
        }

        private Activity GenerateSleepActivityInstance()
        {
            Activity sleep = new()
            {
                ActivityType = "Sleep",
                Start = sleepStartPicker.Value.AddTicks(-(sleepStartPicker.Value.Ticks % TimeSpan.TicksPerMinute)),
                End = sleepEndPicker.Value.AddTicks(-(sleepEndPicker.Value.Ticks % TimeSpan.TicksPerMinute)),
                SleepPlace = sleepPlaceBox.Text,
            };

            if (CurrentBabyDay != null)
            {
                sleep.BabyDayId = CurrentBabyDay.Id;
            }

            return sleep;
        }

        private void AddActivity(Activity activity)
        {
            CurrentBabyDayActivities.Add(activity);
            CurrentBabyDayActivities = CurrentBabyDayActivities.OrderBy(a => a.Start.TimeOfDay).ToList();
        }

        private void RefreshActivitiesListbox()
        {
            activitiesListBox.DataSource = null;
            activitiesListBox.DataSource = CurrentBabyDayActivities;
            activitiesListBox.DisplayMember = "ActivitySummary";
        }

        // TODO: Configure activity pickers so activities are always added to the correct date
        // (EndDate.Date - StartDate.Date).Days
    }
}
