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
            currentBaby = baby;
            InitializeComponent();
            SetStyle();
            SetChartTitle();
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

                weightsChart.Series["Weights (gm)"].Points.AddXY(date, weight);
            }
        }

        private void SetStyle()
        {
            weightsChart.Series["Weights (gm)"].Color = Color.FromArgb(170, Color.Blue);
            weightsChart.Series["Weights (gm)"].EmptyPointStyle.Color = Color.FromArgb(170, Color.Blue);
            weightsChart.Series["Weights (gm)"].CustomProperties = "EmptyPointValue = Average";
        }

        private void SetChartTitle()
        {
            Title title = new();
            title.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            title.Text = currentBaby.FullName;
            weightsChart.Titles.Add(title);
        }
    }
}
