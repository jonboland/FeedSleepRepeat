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
        private void DisableGraphButtons()
        {
            dayGraphButton.Enabled = false;
            weekGraphButton.Enabled = false;
            monthGraphButton.Enabled = false;
        }

        private void DisableButtons()
        {
            deleteButton.Enabled = false;
            createButton.Enabled = false;
            updateButton.Enabled = false;
            addFeedButton.Enabled = false;
            addSleepButton.Enabled = false;
        }

        private void EnableButtonsExistingBaby()
        {
            deleteButton.Enabled = true;
            updateButton.Enabled = true;
            addFeedButton.Enabled = true;
            addSleepButton.Enabled = true;
        }

        private void EnableButtonsNewBaby()
        {
            createButton.Enabled = true;
            addFeedButton.Enabled = true;
            addSleepButton.Enabled = true;
        }

        private void DisableUpdateButtonForHalfASecond()
        {
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            timer.Start();
            updateButton.Enabled = false;
        }

        /// <summary>
        /// Sets the maximum date for the date and date of birth pickers to today.
        /// </summary>
        private void SetMaxDateOfDatePickers()
        {
            dateOfBirthPicker.MaxDate = DateTime.Today;
            datePicker.MaxDate = DateTime.Today;
        }

        /// <summary>
        /// Calls assemble feed type method and sets the feed type combo values based on the result.
        /// </summary>
        private void SetFeedTypeDropdownValues()
        {
            feedTypeCombo.DataSource = FeedSleepRepeatLogic.AssembleFeedTypes();
        }

        /// <summary>
        /// Adds a key down event handler for the activities list box.
        /// </summary>
        private void AddActivitiesKeyDownEventHandler()
        {
            activitiesListBox.KeyDown += activitiesListBox_KeyDown;
        }

        /// <summary>
        /// Calls load babies method to load all baby details from the database.
        /// Inserts default baby that can be used to reset all values.
        /// </summary>
        public void LoadBabyList()
        {
            babies = SqliteDataAccess.LoadBabies();
            babies = FeedSleepRepeatLogic.InsertDefaultBaby(babies);
        }

        /// <summary>
        /// Connects the baby name combo to the babies list data source.
        /// </summary>
        private void ConnectBabyNameCombo()
        {
            babyNameCombo.DataSource = null;
            babyNameCombo.DisplayMember = "FullName";
            babyNameCombo.DataSource = babies;
        }

        private void HandleDatePickerChange()
        {
            // Do nothing if the default baby is selected
            if (string.IsNullOrEmpty(babyNameCombo.Text))
            {
                return;
            }

            // Warn user if changes have been made and new date selected without updating
            if (changed == true)
            {
                if (MessageBox.Show(
                Constants.ChangeDateYesNo,
                Constants.ChangeDateCaption,
                MessageBoxButtons.YesNo)
                == DialogResult.No)
                {
                    // Undo datePicker change without firing warning twice
                    datePicker.ValueChanged -= datePicker_ValueChanged;
                    datePicker.Value = lastDateValue;
                    datePicker.ValueChanged += datePicker_ValueChanged;
                    return;
                }
            }

            ResetBabyDayValues();

            currentBabyDay = currentBaby.BabyDays.FirstOrDefault(bd => bd.Date == datePicker.Value.Date);

            if (currentBabyDay != null)
            {
                RefreshBabyDayValues();
                currentBabyDay.Activities = SqliteDataAccess.LoadActivities(currentBabyDay);
                currentBabyDay.Activities = FeedSleepRepeatLogic.SortActivities(currentBabyDay.Activities);
                RefreshActivitiesListbox();
            }
            else
            {
                currentBabyDay = new();
            }

            changed = false;
        }

        /// <summary>
        /// Resets all feed activity values.
        /// </summary>
        private void ResetFeedValues()
        {
            feedStartPicker.Value = datePicker.Value;
            feedEndPicker.Value = datePicker.Value;
            feedAmountBox.Text = String.Empty;
            feedTypeCombo.SelectedItem = String.Empty;
        }

        /// <summary>
        /// Resets all sleep activity values.
        /// </summary>
        private void ResetSleepValues()
        {
            sleepStartPicker.Value = datePicker.Value;
            sleepEndPicker.Value = datePicker.Value;
            sleepPlaceBox.Text = String.Empty;
        }

        /// <summary>
        /// Resets the current baby day and all baby day values, including the activities list box.
        /// Also calls methods to reset all feed and sleep values.
        /// </summary>
        private void ResetBabyDayValues()
        {
            currentBabyDay = new();
            // Store datePicker value so date can be reverted when date changed without updating
            lastDateValue = datePicker.Value;
            weightBox.Text = string.Empty;
            wetNappiesNumericUpDown.Value = 0;
            dirtyNappiesNumericUpDown.Value = 0;
            nappiesTotal.Text = "0";
            activitiesListBox.DataSource = null;
            ResetFeedValues();
            ResetSleepValues();
        }

        /// <summary>
        /// Resets the current baby and all baby values. Also calls methods to reset the max date
        /// of the date pickers, and reset all baby day and activity values.
        /// </summary>
        private void ResetAllValues()
        {
            currentBaby = new();
            DisableButtons();
            SetMaxDateOfDatePickers();
            dateOfBirthPicker.Value = DateTime.Today.Date;
            ageBox.Text = Constants.DefaultAge;
            datePicker.Value = DateTime.Today.Date;
            ResetBabyDayValues();
        }

        /// <summary>
        /// Refreshes baby day values when a different baby or day is selected.
        /// </summary>
        private void RefreshBabyDayValues()
        {
            weightBox.Text = currentBabyDay.Weight;
            wetNappiesNumericUpDown.Value = currentBabyDay.WetNappies;
            dirtyNappiesNumericUpDown.Value = currentBabyDay.DirtyNappies;
            nappiesTotal.Text = FeedSleepRepeatLogic.RefreshTotalNappies(
                wetNappiesNumericUpDown.Value, dirtyNappiesNumericUpDown.Value);
        }

        /// <summary>
        /// Sets first and last name based on cleaned up baby name combo value when a baby is created.
        /// Also sets date of birth.
        /// </summary>
        /// <param name="name">Array of two strings containing a baby's first and last names.</param>
        private void SetCurrentBabyValues(string[] name)
        {
            currentBaby.FirstName = name[0];
            currentBaby.LastName = name[1];
            currentBaby.DateOfBirth = dateOfBirthPicker.Value.Date;
        }

        /// <summary>
        /// Sets current baby day values when a baby is created or updated.
        /// </summary>
        private void SetCurrentBabyDayValues()
        {
            currentBabyDay.Date = datePicker.Value;
            currentBabyDay.Weight = weightBox.Text;
            currentBabyDay.WetNappies = wetNappiesNumericUpDown.Value;
            currentBabyDay.DirtyNappies = dirtyNappiesNumericUpDown.Value;
        }

        /// <summary>
        /// Adds an activity to the current baby day. Then calls the sort activities method
        /// to sort the current day's activities in time of day order using the start property.
        /// </summary>
        /// <param name="activity">The activiy instance to be added to the current baby day.</param>
        private void AddActivity(Activity activity)
        {
            currentBabyDay.Activities.Add(activity);
            currentBabyDay.Activities = FeedSleepRepeatLogic.SortActivities(currentBabyDay.Activities);
        }

        /// <summary>
        /// Refreshes the activities list box when activities are added/deleted, 
        /// or when a new baby or date is selected.
        /// </summary>
        private void RefreshActivitiesListbox()
        {
            activitiesListBox.DataSource = null;
            activitiesListBox.DataSource = currentBabyDay.Activities;
            activitiesListBox.DisplayMember = "ActivitySummary";
        }
    }
}
