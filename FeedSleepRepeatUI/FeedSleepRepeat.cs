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
            CurrentBabyDay = new();

            CurrentBaby = Babies.First(b => b.FullName == babyNameCombo.Text);

            if (!string.IsNullOrEmpty(CurrentBaby.FullName))
            {
                dateOfBirthPicker.Value = CurrentBaby.DateOfBirth;
                ageBox.Text = CalculateAge();
                CurrentBabyDays = SqliteDataAccess.LoadBabyDays(CurrentBaby);
                BabyDay today = CurrentBabyDays.FirstOrDefault(bd => bd.Date == DateTime.Today);

                if (today != null)
                {
                    CurrentBabyDay = today;
                    RefreshBabyDayValues();
                    CurrentBabyDay.Activities = SqliteDataAccess.LoadActivities(CurrentBabyDay);
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
            ResetBabyDayValues();

            CurrentBabyDay = CurrentBabyDays.FirstOrDefault(bd => bd.Date == datePicker.Value.Date);

            if (CurrentBabyDay != null)
            {
                RefreshBabyDayValues();
                CurrentBabyDay.Activities = SqliteDataAccess.LoadActivities(CurrentBabyDay);
                RefreshActivitiesListbox();
            }
            else
            {
                CurrentBabyDay = new();
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
                CurrentBabyDay.Activities.Remove((Activity)activitiesListBox.SelectedItem);
                activitiesListBox.DataSource = null;
                activitiesListBox.DataSource = CurrentBabyDay.Activities;
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

            SetBabyValues();
            SetBabyDayValues();
            SqliteDataAccess.CreateBaby(CurrentBaby, CurrentBabyDay);
            CurrentBabyDays.Clear();
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

            if (CurrentBaby.DateOfBirth != dateOfBirthPicker.Value.Date)
            {
                CurrentBaby.DateOfBirth = dateOfBirthPicker.Value.Date;
                SqliteDataAccess.UpdateDateOfBirth(CurrentBaby);
            }

            SetBabyDayValues();

            if (CurrentBabyDay.BabyId != 0)
            {               
                SqliteDataAccess.UpdateBabyDay(CurrentBabyDay);
            }
            else
            {
                CurrentBabyDay.BabyId = CurrentBaby.Id;
                SqliteDataAccess.CreateBabyDay(CurrentBabyDay);
            }

            CurrentBabyDays.Clear();
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
                SqliteDataAccess.DeleteBaby(CurrentBaby);
                CurrentBabyDays.Clear();
                ResetAllValues();
                LoadBabyList();
                ConnectBabyNameCombo();
            }
        }
    }
}
