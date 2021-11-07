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
    public partial class WeightChart : Form
    {
        private readonly Baby currentBaby;

        public WeightChart(Baby baby)
        {
            InitializeComponent();
            SetEmptyPointStyle();
            currentBaby = baby;
        }

        private void WeightChart_Load(object sender, EventArgs e)
        {
            FillChart();
        }

        private void FillChart()
        {
            DateTime earliestDay = currentBaby.BabyDays.Min(d => d.Date);
            DateTime latestDay = currentBaby.BabyDays.Max(d => d.Date);
            double totalDays = (latestDay - earliestDay).TotalDays;


            for (int i = 0; i <= totalDays; i++)
            {
                double weight = double.NaN;
                DateTime date = earliestDay.AddDays(i);
                BabyDay day = currentBaby.BabyDays.FirstOrDefault(d => d.Date == date);

                if (day != null && day.Weight != String.Empty)
                {
                    weight = Convert.ToDouble(day.Weight);
                }

                weightsChart.Series["Weights"].Points.AddXY(date, weight);
            }
        }

        private void SetEmptyPointStyle()
        {
            weightsChart.Series["Weights"].EmptyPointStyle.BorderDashStyle = ChartDashStyle.Dash;
            weightsChart.Series["Weights"].CustomProperties = "EmptyPointValue = Average";
        }
    }
}
