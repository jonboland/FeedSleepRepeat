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
        private readonly Baby currentBaby;

        public NappyChart(Baby baby)
        {
            currentBaby = baby;
            InitializeComponent();
            SetChartIconAndTitle();
            SetSeriesColours();
            FillChart();
        }
 
        private void FillChart(int period = -6)
        {
            DateTime firstDay = DateTime.Today;

            for (int i = period; i < 1; i++)
            {
                decimal wetNappies = 0;
                decimal dirtyNappies = 0;
                DateTime date = firstDay.AddDays(i);
                BabyDay day = currentBaby.BabyDays.FirstOrDefault(b => b.Date == date);

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

        private void SetSeriesColours()
        {
            nappiesChart.Series["Dirty Nappies"].Color = Color.FromArgb(170, Color.Blue);
            nappiesChart.Series["Total"].Color = Color.FromArgb(170, Color.LightGray);
        }

        private void SetChartIconAndTitle()
        {
            this.Icon = Properties.Resources.babybottle;
            Title title = new();
            title.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            title.Text = currentBaby.FullName;
            nappiesChart.Titles.Add(title);
        }

        private void ClearPoints()
        {
            foreach (var series in nappiesChart.Series)
            {
                series.Points.Clear();
            }
        }

        private void nappyChart7Button_Click(object sender, EventArgs e)
        {
            ClearPoints();
            FillChart(-6);
        }

        private void nappyChart14Button_Click(object sender, EventArgs e)
        {
            ClearPoints();
            FillChart(-13);
        }

        private void nappyChart30Button_Click(object sender, EventArgs e)
        {
            ClearPoints();
            FillChart(-29);
        }
    }
}
