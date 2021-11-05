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
       
        private void DayGraph_Load(object sender, EventArgs e)
        {
            fillChart();
        }
 
        private void fillChart()
        {   
            nappiesChart.Titles.Add(CurrentBaby.FullName);

            DateTime firstDay = DateTime.Today;

            for (int i = -6; i < 1; i++)
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
    }
}
