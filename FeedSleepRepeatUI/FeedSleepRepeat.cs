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
        List<Baby> Babies;
        Baby CurrentBaby;

        public FeedForm()
        {
            InitializeComponent();
            LoadBabyList();
            ConnectBabyList();
            dateOfBirthPicker.MaxDate = DateTime.Today;
            datePicker.MaxDate = DateTime.Today;
        }

        private void babyNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Baby> babySearch = Babies.Where(b => b.FullName == babyNameCombo.Text).ToList();

            if (!babySearch.Any())
            {
                ResetValues();
            }
            else
            {
                Baby CurrentBaby = babySearch.First();
                dateOfBirthPicker.Value = CurrentBaby.DateOfBirth;
                ageBox.Text = CalculateAge(CurrentBaby.DateOfBirth);
                nappiesTotal.Text = UpdateTotalNappies();
            }
        }

        private void wetNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = UpdateTotalNappies();
        }

        private void dirtyNappiesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            nappiesTotal.Text = UpdateTotalNappies();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string[] name = babyNameCombo.Text.Split();

            if (Babies.Any(b => b.FullName == babyNameCombo.Text))
            {
                // TODO: add pop up to tell user baby already exists
            }
            else if (name.Length != 2)
            {
                // TODO: add pop up to tell user baby needs exactly two names
            }
            else
            {
                Baby b = GenerateBabyInstance();
                BabyDay d = GenerateBabyDayInstance();
                SqliteDataAccess.CreateBaby(b, d);
                ResetValues();
                LoadBabyList();
                ConnectBabyList();
            }        
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Baby b = GenerateBabyInstance();
            SqliteDataAccess.UpdateBaby(b);
            ResetValues();
            LoadBabyList();
            ConnectBabyList();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Baby b = GenerateBabyInstance();
            SqliteDataAccess.DeleteBaby(b);
            ResetValues();
            LoadBabyList();
            ConnectBabyList();
        }

        private void ResetValues()
        {
            CurrentBaby = null;
            dateOfBirthPicker.Value = DateTime.Today.Date;
            ageBox.Text = "0y 0m 0d";
            wetNappiesNumericUpDown.Value = 0;
            dirtyNappiesNumericUpDown.Value = 0;
            nappiesTotal.Text = "0";
        }

        private void LoadBabyList()
        {
            Babies = SqliteDataAccess.LoadBabies();
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

        private string UpdateTotalNappies()
        {
            decimal totalNappies = wetNappiesNumericUpDown.Value + dirtyNappiesNumericUpDown.Value;
            return totalNappies.ToString();
        }
    }
}
