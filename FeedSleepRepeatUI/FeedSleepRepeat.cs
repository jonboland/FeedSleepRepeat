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
        List<BabyDay> BabyDays;
        Baby CurrentBaby;
        //BabyDay CurrentDay;

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
                    weightBox.Text = today.Weight;
                    wetNappiesNumericUpDown.Value = today.WetNappies;
                    dirtyNappiesNumericUpDown.Value = today.DirtyNappies;
                    nappiesTotal.Text = UpdateTotalNappies();
                }
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
                MessageBox.Show("Creation was unsuccessful because a baby with this name already exists.");
            }
            else if (name.Length != 2)
            {
                MessageBox.Show("Creation was unsuccessful because babies must have exactly one first name and one last name.");
            }
            else
            {
                Baby b = GenerateBabyInstance();
                BabyDay d = GenerateBabyDayInstance();
                SqliteDataAccess.CreateBaby(b, d);
                LoadBabyList();
                ConnectBabyList();
                ResetValues();
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
