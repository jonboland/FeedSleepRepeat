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
            ApplyPropertySettings();
            LoadBabyList();
            ConnectBabyList();
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
                CurrentBaby = babySearch.First();
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
    }
}
