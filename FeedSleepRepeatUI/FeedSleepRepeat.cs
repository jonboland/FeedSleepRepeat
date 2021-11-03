﻿using System;
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
        private List<Baby> babies = new();
        private Baby currentBaby = new();
        private BabyDay currentBabyDay = new();
        private string lastBabyName = string.Empty;
        private DateTime lastDateValue = new();
        private readonly Timer timer = new();
        private bool datePickerDroppedDown = false;
        private bool changed = false;

        public FeedForm()
        {
            InitializeComponent();
            DisableButtons();
            DisableGraphButtons();
            SetMaxDateOfDatePickers();
            SetFeedTypeDropdownValues();
            AddActivitiesKeyDownEventHandler();
            LoadBabyList();
            ConnectBabyNameCombo();
        }

        /// <summary>
        /// Populates fields with the selected baby's details, unless the default (blank) baby is selected.
        /// This includes the baby day fields and activity list if data for today's date is present.
        /// 
        /// NB: Method is triggered at runtime.
        /// </summary>
        /// <param name="sender">Reference to the object that raised the event</param>
        /// <param name="e">Object specific to the event that is being handled</param>
        private void babyNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool cancelSelection = ConfirmBabySelectionIfChangesWillBeLost();

            if (cancelSelection)
            {
                return;
            }

            if (currentBaby.FullName != babyNameCombo.Text)
            {
                ResetAllValues();

                currentBaby = babies.First(b => b.FullName == babyNameCombo.Text);

                if (!string.IsNullOrEmpty(currentBaby.FullName))
                {
                    EnableButtonsExistingBaby();
                    dateOfBirthPicker.Value = currentBaby.DateOfBirth;
                    ageBox.Text = FeedSleepRepeatLogic.CalculateAge(currentBaby.DateOfBirth);
                    currentBaby.BabyDays = SqliteDataAccess.LoadBabyDays(currentBaby);
                    BabyDay today = currentBaby.BabyDays.FirstOrDefault(bd => bd.Date == DateTime.Today);

                    if (today != null)
                    {
                        currentBabyDay = today;
                        datePicker.ValueChanged -= datePicker_ValueChanged;
                        RefreshBabyDayValues();
                        datePicker.ValueChanged += datePicker_ValueChanged;
                        currentBabyDay.Activities = SqliteDataAccess.LoadActivities(currentBabyDay);
                        currentBabyDay.Activities = FeedSleepRepeatLogic.SortActivities(currentBabyDay.Activities);
                        RefreshActivitiesListbox();
                    }
                }

                changed = false;
                lastBabyName = string.Empty;
            }          
        }

        /// <summary>
        /// Disables and enables buttons based on the babyNameCombo.Text value.
        /// </summary>
        private void babyNameCombo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(babyNameCombo.Text))
            {
                DisableButtons();
            }

            else if (babies.Any(b => b.FullName == babyNameCombo.Text))
            {
                DisableButtons();
                EnableButtonsExistingBaby();
            }

            else
            {
                DisableButtons();
                EnableButtonsNewBaby();
                lastBabyName = babyNameCombo.Text;
            }
        }

        /// <summary>
        /// Sets the changed state to true if the dateOfBirthPicker value is changed.
        /// 
        /// The changed state is used to determine whether warnings about 
        /// potential loss of changes are displayed.
        /// </summary>
        private void dateOfBirthPicker_ValueChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        /// <summary>
        /// Sets the changed state to true if the weightBox.Text value is changed.
        /// 
        /// The changed state is used to determine whether warnings about 
        /// potential loss of changes are displayed. 
        /// </summary>
        private void weightBox_TextChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        /// <summary>
        /// Updates the total nappies textbox when the number of wet nappies is changed.
        /// </summary>
        private void wetNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = FeedSleepRepeatLogic.RefreshTotalNappies(
                wetNappiesNumericUpDown.Value, dirtyNappiesNumericUpDown.Value);

            changed = true;
        }

        /// <summary>
        /// Updates the total nappies textbox when the number of dirty nappies is changed.
        /// </summary>
        private void dirtyNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = FeedSleepRepeatLogic.RefreshTotalNappies(
                wetNappiesNumericUpDown.Value, dirtyNappiesNumericUpDown.Value);

            changed = true;
        }

        /// <summary>
        /// Sets the datePickerDroppedDown state to true when the calendar is open.
        /// </summary>
        private void datePicker_DropDown(object sender, EventArgs e)
        {
            datePickerDroppedDown = true;
        }

        /// <summary>
        /// If the calendar is open and the selected baby day exists,
        /// populates baby day fields and activity list with its values.
        /// </summary>
        private void datePicker_CloseUp(object sender, EventArgs e)
        {
            if (datePicker.Value == lastDateValue)
            {
                datePickerDroppedDown = false;
                return;
            }

            HandleDatePickerChange();
            datePickerDroppedDown = false;
        }

        /// <summary>
        /// If the calendar is closed and the selected baby day exists,
        /// populates baby day fields and activity list with its values.
        /// 
        /// NB: Method is triggered at runtime.
        /// </summary>
        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            // If the calendar is being used, let datePicker_CloseUp handle any changes
            if (datePickerDroppedDown == true)
            {
                return;
            }

            HandleDatePickerChange();
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
                currentBabyDay.Id, ActivityType.Feed, feedStartPicker.Value, feedEndPicker.Value, 
                feedAmount: feedAmountBox.Text, feedType: feedTypeCombo.Text);

            AddActivity(feed);
            RefreshActivitiesListbox();
            ResetFeedValues();
            changed = true;
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
                currentBabyDay.Id, ActivityType.Sleep, sleepStartPicker.Value, sleepEndPicker.Value,
                sleepPlace: sleepPlaceBox.Text);

            AddActivity(sleep);
            RefreshActivitiesListbox();
            ResetSleepValues();
            changed = true;
        }

        /// <summary>
        /// Deletes currently selected activity from activity list box when delete or back key pressed.
        /// </summary>
        private void activitiesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Delete or Keys.Back)
            {
                currentBabyDay.Activities.Remove((Activity)activitiesListBox.SelectedItem);
                RefreshActivitiesListbox();
                changed = true;
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

            if (babies.Any(b => b.FullName == babyNameCombo.Text.Trim()))
            {
                MessageBox.Show(Constants.CreationFailedBabyAlreadyExists);
                return;
            }

            SetCurrentBabyValues(name);
            SetCurrentBabyDayValues();
            SqliteDataAccess.CreateBaby(currentBaby, currentBabyDay);
            ResetAllValues();
            LoadBabyList();
            ConnectBabyNameCombo();
            changed = false;
        }

        /// <summary>
        /// Updates selected baby/baby day record and associated activity records in database.
        /// Or, if baby day doesn't exist, creates records in database.
        /// </summary>
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!babies.Any(baby => baby.FullName == babyNameCombo.Text))
            {
                MessageBox.Show(Constants.UpdateFailedBabyNotCreated);
                return;
            }

            if (currentBaby.FullName == String.Empty)
            {
                MessageBox.Show(Constants.UpdateFailedBabyNotSelected);
                return;
            }

            if (currentBaby.DateOfBirth != dateOfBirthPicker.Value.Date)
            {
                currentBaby.DateOfBirth = dateOfBirthPicker.Value.Date;
                SqliteDataAccess.UpdateDateOfBirth(currentBaby);
            }

            SetCurrentBabyDayValues();

            // ROWIDs assigned via Sqlite AUTOINCREMENT begin at 1
            if (currentBabyDay.BabyId != 0)
            {               
                SqliteDataAccess.UpdateBabyDay(currentBabyDay);
            }
            else
            {
                currentBabyDay.BabyId = currentBaby.Id;
                SqliteDataAccess.CreateBabyDay(currentBabyDay);
            }

            currentBaby.BabyDays = SqliteDataAccess.LoadBabyDays(currentBaby);
            currentBabyDay = currentBaby.BabyDays.First(bd => bd.Date == datePicker.Value.Date);
            currentBabyDay.Activities = SqliteDataAccess.LoadActivities(currentBabyDay);
            changed = false;

            DisableUpdateButtonForHalfASecond();
        }

        /// <summary>
        /// Reenables the Update button and stops the timer.
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            // Handles possibility of user typing new name while update button disabled
            if (createButton.Enabled == false)
            {
                updateButton.Enabled = true;
            }
            
            timer.Stop();
        }

        /// <summary>
        /// Confirms decision then deletes baby and all associated records from database.
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!babies.Any(baby => baby.FullName == babyNameCombo.Text))
            {
                MessageBox.Show(Constants.DeleteFailedBabyNotCreated);
                return;
            }

            if (currentBaby.FullName == String.Empty)
            {
                MessageBox.Show(Constants.DeleteFailedBabyNotSelected);
                return;
            }

            if (MessageBox.Show(
                Constants.DeleteBabyYesNo,
                Constants.DeleteBabyCaption, 
                MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                SqliteDataAccess.DeleteBaby(currentBaby);
                ResetAllValues();
                LoadBabyList();
                ConnectBabyNameCombo();
                changed = false;
            }
        }

        /// <summary>
        /// Confirms decision if application closed without updating/creating.
        /// </summary>
        private void FeedForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && changed == true)
            {
                if (updateButton.Enabled)
                {
                    if (MessageBox.Show(
                        Constants.FormCloseYesNoUpdate,
                        Constants.FormCloseCaption,
                        MessageBoxButtons.YesNo)
                        == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }

                else if (createButton.Enabled)
                {
                    if (MessageBox.Show(
                        Constants.FormCloseYesNoCreate,
                        Constants.FormCloseCaption,
                        MessageBoxButtons.YesNo)
                        == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }


    }
}
