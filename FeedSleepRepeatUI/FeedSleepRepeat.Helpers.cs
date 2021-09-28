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

        private void RefreshBabyDayValues()
        {
            weightBox.Text = CurrentBabyDay.Weight;
            wetNappiesNumericUpDown.Value = CurrentBabyDay.WetNappies;
            dirtyNappiesNumericUpDown.Value = CurrentBabyDay.DirtyNappies;
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

        private void SetBabyValues()
        {
            string[] name = babyNameCombo.Text.Split();
            CurrentBaby.FirstName = name[0];
            CurrentBaby.LastName = name[1];
            CurrentBaby.DateOfBirth = dateOfBirthPicker.Value.Date;
        }

        private string CalculateAge()
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(CurrentBaby.DateOfBirth);
            DateTime age = DateTime.MinValue + timeSpan;
            return $"{age.Year - 1}y {age.Month - 1}m {age.Day - 1}d";
        }

        private void SetBabyDayValues()
        {
            CurrentBabyDay.Date = datePicker.Value;
            CurrentBabyDay.Weight = weightBox.Text;
            CurrentBabyDay.WetNappies = wetNappiesNumericUpDown.Value;
            CurrentBabyDay.DirtyNappies = dirtyNappiesNumericUpDown.Value;
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

            if (CurrentBabyDay.BabyId != 0)
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

            if (CurrentBabyDay.BabyId != 0)
            {
                sleep.BabyDayId = CurrentBabyDay.Id;
            }

            return sleep;
        }

        private void AddActivity(Activity activity)
        {
            CurrentBabyDay.Activities.Add(activity);
            CurrentBabyDay.Activities = CurrentBabyDay.Activities.OrderBy(a => a.Start.TimeOfDay).ToList();
        }

        private void RefreshActivitiesListbox()
        {
            activitiesListBox.DataSource = null;
            activitiesListBox.DataSource = CurrentBabyDay.Activities;
            activitiesListBox.DisplayMember = "ActivitySummary";
        }

        // TODO: Configure activity pickers so activities are always added to the correct date
        // (EndDate.Date - StartDate.Date).Days
    }
}
