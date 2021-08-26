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
        private void ResetValues()
        {
            CurrentBaby = null;
            dateOfBirthPicker.Value = DateTime.Today.Date;
            ageBox.Text = "0y 0m 0d";
            datePicker.Value = DateTime.Today.Date;
            weightBox.Text = string.Empty;
            wetNappiesNumericUpDown.Value = 0;
            dirtyNappiesNumericUpDown.Value = 0;
            nappiesTotal.Text = "0";
        }

        public void LoadBabyList()
        {
            Babies = SqliteDataAccess.LoadBabies();
            // Insert default baby that can be used to reset all values
            Babies.Insert(0, new Baby()
            {
                FirstName = String.Empty,
                LastName = String.Empty,
                DateOfBirth = DateTime.Today.Date
            });
        }

        private void ConnectBabyList()
        {
            babyNameCombo.DataSource = null;
            babyNameCombo.DataSource = Babies;
            babyNameCombo.DisplayMember = "FullName";
        }

        private Baby GenerateBabyInstance()
        {
            Baby b = new();

            string[] name = babyNameCombo.Text.Split();

            b.FirstName = name[0];
            b.LastName = name[1];
            b.DateOfBirth = dateOfBirthPicker.Value.Date;

            return b;
        }

        private string CalculateAge(DateTime dateOfBirth)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateOfBirth);
            DateTime age = DateTime.MinValue + timeSpan;
            return $"{age.Year - 1}y {age.Month - 1}m {age.Day - 1}d";
        }

        private BabyDay GenerateBabyDayInstance()
        {
            BabyDay d = new();

            d.Date = datePicker.Value;
            d.Weight = weightBox.Text;
            d.WetNappies = wetNappiesNumericUpDown.Value;
            d.DirtyNappies = dirtyNappiesNumericUpDown.Value;

            return d;
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
    }
}
