using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FeedSleepRepeatLibrary;

namespace FeedSleepRepeatUI
{
    public partial class NappyChart : Form
    {
        public Baby CurrentBaby { get; set; }

        public NappyChart(Baby currentBaby)
        {
            CurrentBaby = currentBaby;
            InitializeComponent();
        }
       
        private void NappyChart_Load(object sender, EventArgs e)
        {
            FillChart();
        }
 
        private void FillChart(int period = -6)
        {
            ResetChart();

            DateTime firstDay = DateTime.Today;

            for (int i = period; i < 1; i++)
            {
                decimal wetNappies = 0;
                decimal dirtyNappies = 0;
                DateTime date = firstDay.AddDays(i);
                BabyDay day = CurrentBaby.BabyDays.FirstOrDefault(b => b.Date == date);

                if (day != null)
                {
                    wetNappies = day.WetNappies;
                    dirtyNappies = day.DirtyNappies;
                }

                nappiesChart.Series["Wet Nappies"].Points.AddXY(date, wetNappies);
                nappiesChart.Series["Dirty Nappies"].Points.AddXY(date, dirtyNappies);
                nappiesChart.Series["Total"].Points.AddXY(date, wetNappies + dirtyNappies);
            }
        }

        private void ResetChart()
        {
            nappiesChart.Titles.Clear();
            nappiesChart.Titles.Add(CurrentBaby.FullName);

            foreach (var series in nappiesChart.Series)
            {
                series.Points.Clear();
            }
        }

        private void nappyChart7Button_Click(object sender, EventArgs e)
        {
            FillChart(-6);
        }

        private void nappyChart14Button_Click(object sender, EventArgs e)
        {
            FillChart(-13);
        }

        private void nappyChart30Button_Click(object sender, EventArgs e)
        {
            FillChart(-29);
        }
    }
}
