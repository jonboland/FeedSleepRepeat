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
        List<BabyDay> CurrentBabyDays = new();
        BabyDay CurrentBabyDay = new();
        List<Activity> CurrentBabyDayActivities = new();

        public FeedForm()
        {
            InitializeComponent();
            SetDatePickerMaxValues();
            AddFeedTypeDropdownValues();
            AddActivitiesKeyDownEventHandler();
            LoadBabyList();
            ConnectBabyNameCombo();
        }

        // This method is triggered at runtime
        private void babyNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAllValues();
            CurrentBabyDays.Clear();
            CurrentBabyDay = null;
            CurrentBabyDayActivities.Clear();

            CurrentBaby = Babies.First(b => b.FullName == babyNameCombo.Text);

            if (!string.IsNullOrEmpty(CurrentBaby.FullName))
            {
                dateOfBirthPicker.Value = CurrentBaby.DateOfBirth;
                ageBox.Text = CalculateAge(CurrentBaby.DateOfBirth);
                CurrentBabyDays = SqliteDataAccess.LoadBabyDays(CurrentBaby);
                BabyDay today = CurrentBabyDays.FirstOrDefault(bd => bd.Date == DateTime.Today);

                if (today != null)
                {
                    CurrentBabyDay = today;
                    RefreshBabyDayValues(today);
                    CurrentBabyDayActivities = SqliteDataAccess.LoadActivities(today);
                    RefreshActivitiesListbox();
                }
            }
        }

        private void wetNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = RefreshTotalNappies();
        }

        private void dirtyNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = RefreshTotalNappies();
        }

        // This method is triggered at runtime
        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            CurrentBabyDayActivities.Clear();

            CurrentBabyDay = CurrentBabyDays.FirstOrDefault(bd => bd.Date == datePicker.Value.Date);

            if (CurrentBabyDay != null)
            {
                RefreshBabyDayValues(CurrentBabyDay);
                CurrentBabyDayActivities = SqliteDataAccess.LoadActivities(CurrentBabyDay);
                RefreshActivitiesListbox();
            }
            else
            {
                ResetBabyDayValues();
            }
        }

        private void addFeedButton_Click(object sender, EventArgs e)
        {
            if (babyNameCombo.Text.All(char.IsWhiteSpace))
            {
                MessageBox.Show("Feed could not be added because a baby hasn't been selected.");
                return;
            }

            Activity feed = GenerateFeedActivityInstance();
            AddActivity(feed);
            RefreshActivitiesListbox();
            ResetFeedValues();
        }

        private void addSleepButton_Click(object sender, EventArgs e)
        {
            //if (CurrentBaby == null || CurrentBaby.FirstName == String.Empty)
            if (babyNameCombo.Text.All(char.IsWhiteSpace))
                {
                MessageBox.Show("Sleep could not be added because a baby hasn't been selected.");
                return;
            }

            Activity sleep = GenerateSleepActivityInstance();
            AddActivity(sleep);
            RefreshActivitiesListbox();
            ResetSleepValues();
        }

        private void activitiesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Delete or Keys.Back)
            {
                CurrentBabyDayActivities.Remove((Activity)activitiesListBox.SelectedItem);
                activitiesListBox.DataSource = null;
                activitiesListBox.DataSource = CurrentBabyDayActivities;
                activitiesListBox.DisplayMember = "ActivitySummary";
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {   
            if (Babies.Any(b => b.FullName == babyNameCombo.Text))
            {
                MessageBox.Show("Creation was unsuccessful because a baby with this name already exists.");
                return;
            }

            //TODO: strip whitespace from either end of babyNameCombo.Text before splitting
            string[] name = babyNameCombo.Text.Split();

            if (name.Length != 2)
            {
                MessageBox.Show("Creation was unsuccessful because babies must have exactly one first name and one last name.");
                return;
            }

            Baby baby = GenerateBabyInstance();
            BabyDay day = GenerateBabyDayInstance();
            SqliteDataAccess.CreateBaby(baby, day, CurrentBabyDayActivities);
            //SqliteDataAccess.CreateBaby(baby, day);
            CurrentBabyDays.Clear();
            CurrentBabyDayActivities.Clear();
            ResetAllValues();
            LoadBabyList();
            ConnectBabyNameCombo();   
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!Babies.Any(baby => baby.FullName == babyNameCombo.Text))
            {
                MessageBox.Show("This baby couldn't be updated because it hasn't been created yet.");
                return;
            }

            if (CurrentBaby.FullName == String.Empty)
            {
                MessageBox.Show("Updating was unsuccessful because a baby hasn't been selected.");
                return;
            }
            // TODO: Consider updating the existing baby instance instead of generating a new one
            Baby baby = GenerateBabyInstance();
            SqliteDataAccess.UpdateBaby(baby);

            if (CurrentBabyDay != null)
            {
                UpdateSelectedBabyDay(CurrentBabyDay);
                SqliteDataAccess.UpdateBabyDay(CurrentBabyDay, CurrentBabyDayActivities);
            }
            else
            {
                BabyDay babyDay = GenerateBabyDayInstance();
                babyDay.BabyId = CurrentBaby.Id;
                SqliteDataAccess.CreateBabyDay(babyDay, CurrentBabyDayActivities);
            }

            CurrentBabyDays.Clear();
            CurrentBabyDayActivities.Clear();
            ResetAllValues();
            LoadBabyList();
            ConnectBabyNameCombo();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show(
                "Are you sure you want to permanently delete this baby's record?", 
                "Delete Confirmation",
                MessageBoxButtons.YesNo);

            if (choice == DialogResult.Yes)
            {
                Baby b = GenerateBabyInstance();
                SqliteDataAccess.DeleteBaby(b);
                CurrentBabyDays.Clear();
                CurrentBabyDayActivities.Clear();
                ResetAllValues();
                LoadBabyList();
                ConnectBabyNameCombo();
            }
        }
    }
}
