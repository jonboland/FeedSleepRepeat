using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FeedSleepRepeatLibrary;

namespace FeedSleepRepeatUI
{
    public partial class FeedForm : Form
    {
        List<Baby> Babies = new();
        Baby CurrentBaby = new();
        BabyDay CurrentBabyDay = new();

        public FeedForm()
        {
            InitializeComponent();
            SetMaxDateOfDatePickers();
            SetFeedTypeDropdownValues();
            AddActivitiesKeyDownEventHandler();
            LoadBabyList();
            ConnectBabyNameCombo();
        }

        /// <summary>
        /// Populates fields with the selected baby's details, unless the default (blank) baby is selected.
        /// This includes the baby day fields and activity list if data for today's date is present.
        /// NB: Method is triggered at runtime.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event</param>
        /// <param name="e">Object specific to the event that is being handled</param>
        private void babyNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAllValues();

            CurrentBaby = Babies.First(b => b.FullName == babyNameCombo.Text);

            if (!string.IsNullOrEmpty(CurrentBaby.FullName))
            {
                dateOfBirthPicker.Value = CurrentBaby.DateOfBirth;
                ageBox.Text = FeedSleepRepeatLogic.CalculateAge(CurrentBaby.DateOfBirth);
                CurrentBaby.BabyDays = SqliteDataAccess.LoadBabyDays(CurrentBaby);
                BabyDay today = CurrentBaby.BabyDays.FirstOrDefault(bd => bd.Date == DateTime.Today);

                if (today != null)
                {
                    CurrentBabyDay = today;
                    RefreshBabyDayValues();
                    CurrentBabyDay.Activities = SqliteDataAccess.LoadActivities(CurrentBabyDay);
                    CurrentBabyDay.Activities = FeedSleepRepeatLogic.SortActivities(CurrentBabyDay.Activities);
                    RefreshActivitiesListbox();
                }
            }
        }

        /// <summary>
        /// Updates the total nappies textbox when the number of wet nappies is changed
        /// </summary>
        private void wetNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = FeedSleepRepeatLogic.RefreshTotalNappies(
                wetNappiesNumericUpDown.Value, dirtyNappiesNumericUpDown.Value);
        }

        /// <summary>
        /// Updates the total nappies textbox when the number of dirty nappies is changed
        /// </summary>
        private void dirtyNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = FeedSleepRepeatLogic.RefreshTotalNappies(
                wetNappiesNumericUpDown.Value, dirtyNappiesNumericUpDown.Value);
        }

        /// <summary>
        /// If the selected baby day exists, populates baby day fields and activity list with its values.
        /// NB: Method is triggered at runtime.
        /// </summary>
        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            ResetBabyDayValues();

            CurrentBabyDay = CurrentBaby.BabyDays.FirstOrDefault(bd => bd.Date == datePicker.Value.Date);

            if (CurrentBabyDay != null)
            {
                RefreshBabyDayValues();
                CurrentBabyDay.Activities = SqliteDataAccess.LoadActivities(CurrentBabyDay);
                CurrentBabyDay.Activities = FeedSleepRepeatLogic.SortActivities(CurrentBabyDay.Activities);
                RefreshActivitiesListbox();
            }
            else
            {
                CurrentBabyDay = new();
            }
        }

        /// <summary>
        /// Generates feed activity instance, adds it to current baby day activities, 
        /// refreshes activity list box, and resets feed values.
        /// </summary>
        private void addFeedButton_Click(object sender, EventArgs e)
        {
            if (babyNameCombo.Text.All(char.IsWhiteSpace))
            {
                MessageBox.Show(Constants.FeedNotAddedNoBabySelected);
                return;
            }

            Activity feed = FeedSleepRepeatLogic.GenerateActivityInstance( 
                CurrentBabyDay.Id, ActivityType.Feed, feedStartPicker.Value, feedEndPicker.Value, 
                feedAmount: feedAmountBox.Text, feedType: feedTypeCombo.Text);

            AddActivity(feed);
            RefreshActivitiesListbox();
            ResetFeedValues();
        }

        /// <summary>
        /// Generates sleep activity instance, adds it to current baby day activities, 
        /// refreshes activity list box, and resets sleep values.
        /// </summary>
        private void addSleepButton_Click(object sender, EventArgs e)
        {
            if (babyNameCombo.Text.All(char.IsWhiteSpace))
            {
                MessageBox.Show(Constants.SleepNotAddedNoBabySelected);
                return;
            }

            Activity sleep = FeedSleepRepeatLogic.GenerateActivityInstance(
                CurrentBabyDay.Id, ActivityType.Sleep, sleepStartPicker.Value, sleepEndPicker.Value,
                sleepPlace: sleepPlaceBox.Text);

            AddActivity(sleep);
            RefreshActivitiesListbox();
            ResetSleepValues();
        }

        /// <summary>
        /// Deletes currently selected activity from activity list box when delete or back key pressed.
        /// </summary>
        private void activitiesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Delete or Keys.Back)
            {
                CurrentBabyDay.Activities.Remove((Activity)activitiesListBox.SelectedItem);
                RefreshActivitiesListbox();
            }
        }

        /// <summary>
        /// Creates a new baby record in the database.
        /// Also creates a baby day record for the currently selected day,
        /// and records for any activities added to it.
        /// </summary>
        private void createButton_Click(object sender, EventArgs e)
        {
            string[] name = FeedSleepRepeatLogic.FormatName(babyNameCombo.Text);

            if (name.Length != 2)
            {
                MessageBox.Show(Constants.CreationFailedInvalidNameFormat);
                return;
            }

            if (Babies.Any(b => b.FullName == babyNameCombo.Text.Trim()))
            {
                MessageBox.Show(Constants.CreationFailedBabyAlreadyExists);
                return;
            }

            SetCurrentBabyValues(name);
            SetCurrentBabyDayValues();
            SqliteDataAccess.CreateBaby(CurrentBaby, CurrentBabyDay);
            ResetAllValues();
            LoadBabyList();
            ConnectBabyNameCombo();
        }

        /// <summary>
        /// Updates selected baby/baby day record and associated activity records in database.
        /// Or, if baby day doesn't exist, creates records in database.
        /// </summary>
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!Babies.Any(baby => baby.FullName == babyNameCombo.Text))
            {
                MessageBox.Show(Constants.UpdateFailedBabyNotCreated);
                return;
            }

            if (CurrentBaby.FullName == String.Empty)
            {
                MessageBox.Show(Constants.UpdateFailedBabyNotSelected);
                return;
            }

            if (CurrentBaby.DateOfBirth != dateOfBirthPicker.Value.Date)
            {
                CurrentBaby.DateOfBirth = dateOfBirthPicker.Value.Date;
                SqliteDataAccess.UpdateDateOfBirth(CurrentBaby);
            }

            SetCurrentBabyDayValues();

            // ROWIDs assigned via Sqlite AUTOINCREMENT begin at 1
            if (CurrentBabyDay.BabyId != 0)
            {               
                SqliteDataAccess.UpdateBabyDay(CurrentBabyDay);
            }
            else
            {
                CurrentBabyDay.BabyId = CurrentBaby.Id;
                SqliteDataAccess.CreateBabyDay(CurrentBabyDay);
            }

            ResetAllValues();
            LoadBabyList();
            ConnectBabyNameCombo();
        }

        /// <summary>
        /// Confirms decision then deletes baby and all associated records from database.
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!Babies.Any(baby => baby.FullName == babyNameCombo.Text))
            {
                MessageBox.Show(Constants.DeleteFailedBabyNotCreated);
                return;
            }

            if (CurrentBaby.FullName == String.Empty)
            {
                MessageBox.Show(Constants.DeleteFailedBabyNotSelected);
                return;
            }

            DialogResult choice = MessageBox.Show(
                Constants.DeleteBabyYesNo, 
                Constants.DeleteBabyCaption,
                MessageBoxButtons.YesNo);

            if (choice == DialogResult.Yes)
            {
                SqliteDataAccess.DeleteBaby(CurrentBaby);
                ResetAllValues();
                LoadBabyList();
                ConnectBabyNameCombo();
            }
        }

        // TODO: Consider disabling/enabling Create and Update buttons instead of resetting fields
        // TODO: Consider adding YesNo dialogs when user navigates away without creating/updating
        // TODO: Handle and log exceptions
    }
}
