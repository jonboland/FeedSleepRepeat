using System;
using System.Drawing;
using System.Linq;
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
            SetChartIconAndTitle();
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

                weightsChart.Series["Weights (g)"].Points.AddXY(date, weight);
            }
        }

        private void SetStyle()
        {
            weightsChart.Series["Weights (g)"].Color = Color.FromArgb(170, Color.Blue);
            weightsChart.Series["Weights (g)"].EmptyPointStyle.Color = Color.FromArgb(170, Color.Blue);
            weightsChart.Series["Weights (g)"].CustomProperties = "EmptyPointValue = Average";
        }

        private void SetChartIconAndTitle()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Title title = new();
            title.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            title.Text = currentBaby.FullName;
            weightsChart.Titles.Add(title);
        }
    }
}
