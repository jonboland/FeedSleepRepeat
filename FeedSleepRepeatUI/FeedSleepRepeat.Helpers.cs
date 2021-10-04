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
            CurrentBabyDay = new();
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
            CurrentBaby = new();
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
            nappiesTotal.Text = FeedSleepRepeatLogic.RefreshTotalNappies(
                wetNappiesNumericUpDown.Value, dirtyNappiesNumericUpDown.Value);
        }

        private void SetFeedTypeDropdownValues()
        {
            feedTypeCombo.DataSource = FeedSleepRepeatLogic.AssembleFeedTypes();
        }

        private void SetCurrentBabyValues()
        {
            string[] name = babyNameCombo.Text.Split();
            CurrentBaby.FirstName = name[0];
            CurrentBaby.LastName = name[1];
            CurrentBaby.DateOfBirth = dateOfBirthPicker.Value.Date;
        }

        private void SetCurrentBabyDayValues()
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
                ActivityType = ActivityType.Feed,
                Start = FeedSleepRepeatLogic.TruncateTime(feedStartPicker.Value),
                End = FeedSleepRepeatLogic.TruncateTime(feedEndPicker.Value),
                FeedAmount = feedAmountBox.Text,
                FeedType = feedTypeCombo.Text,
            };

            // TODO: Check whether baby id can be set as part of feed instance creation
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
                ActivityType = ActivityType.Sleep,
                Start = FeedSleepRepeatLogic.TruncateTime(sleepStartPicker.Value),
                End = FeedSleepRepeatLogic.TruncateTime(sleepEndPicker.Value),
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
            CurrentBabyDay.Activities = FeedSleepRepeatLogic.SortActivities(CurrentBabyDay.Activities);
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
