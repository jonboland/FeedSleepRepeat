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
        private void ResetFeedValues()
        {
            feedStartPicker.Value = DateTime.Now;
            feedEndPicker.Value = DateTime.Now;
            feedAmountBox.Text = String.Empty;
            feedTypeCombo.SelectedItem = String.Empty;
        }
        
        private void ResetBabyDayValues()
        {
            weightBox.Text = string.Empty;
            wetNappiesNumericUpDown.Value = 0;
            dirtyNappiesNumericUpDown.Value = 0;
            nappiesTotal.Text = "0";
            ResetFeedValues();
        }
        
        private void ResetAllValues()
        {
            CurrentBaby = null;
            dateOfBirthPicker.Value = DateTime.Today.Date;
            ageBox.Text = "0y 0m 0d";
            datePicker.Value = DateTime.Today.Date;
            ResetBabyDayValues();

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

        public void LoadBabyList()
        {
            Babies = SqliteDataAccess.LoadBabies();
            // Insert default baby that can be used to reset all values
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
            babyNameCombo.DataSource = Babies;
            babyNameCombo.DisplayMember = "FullName";
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
    }
}
