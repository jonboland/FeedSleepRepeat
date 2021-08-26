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
        List<BabyDay> BabyDays = new();       
        //BabyDay CurrentDay = new();

        public FeedForm()
        {
            InitializeComponent();
            ApplyPropertySettings();
            LoadBabyList();
            ConnectBabyList();
        }

        private void babyNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetValues();
            CurrentBaby = Babies.FirstOrDefault(b => b.FullName == babyNameCombo.Text);

            if (CurrentBaby != null && CurrentBaby.FirstName != String.Empty)
            {
                dateOfBirthPicker.Value = CurrentBaby.DateOfBirth;
                ageBox.Text = CalculateAge(CurrentBaby.DateOfBirth);
                BabyDays = SqliteDataAccess.LoadBabyDays(CurrentBaby);
                BabyDay today = BabyDays.FirstOrDefault(bd => bd.Date == DateTime.Today);

                if (today != null)
                {
                    RefreshBabyDayValues(today);
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

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {          
            BabyDay selectedDay = BabyDays.FirstOrDefault(bd => bd.Date == datePicker.Value.Date);

            if (selectedDay != null)
            {
                RefreshBabyDayValues(selectedDay);
            }
            else
            {
                weightBox.Text = string.Empty;
                wetNappiesNumericUpDown.Value = 0;
                dirtyNappiesNumericUpDown.Value = 0;
                nappiesTotal.Text = "0";
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

            Baby b = GenerateBabyInstance();
            BabyDay d = GenerateBabyDayInstance();
            SqliteDataAccess.CreateBaby(b, d);
            ResetValues();
            LoadBabyList();
            ConnectBabyList();   
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!Babies.Any(b => b.FullName == babyNameCombo.Text))
            {
                MessageBox.Show("This baby couldn't be updated because it hasn't been created yet.");
                return;
            }

            Baby b = GenerateBabyInstance();
            SqliteDataAccess.UpdateBaby(b);

            BabyDay d = GenerateBabyDayInstance();
            d.BabyId = CurrentBaby.Id;              

            if (BabyDays.Any(d => d.Date == datePicker.Value.Date))
            {
                SqliteDataAccess.UpdateBabyDay(d);
            }
            else
            {
                SqliteDataAccess.CreateBabyDay(d);
            }

            ResetValues();
            LoadBabyList();
            ConnectBabyList();
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
                ResetValues();
                LoadBabyList();
                ConnectBabyList();
            }
        }
    }
}
