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
    public partial class ActivityChart : Form
    {
        private readonly Baby currentBaby;
        private readonly DateTime day = DateTime.Today;

        public ActivityChart(Baby baby)
        {
            currentBaby = baby;
            InitializeComponent();
            SetChartProperties();
            SetSeriesProperties();
            FillChart();
        }

        private void FillChart(int period = 7)
        {
            activitiesChart.Titles.Add(currentBaby.FullName);
            List<Activity> currentActivities;

            for (int i = 0; i < period; i++)
            {
                
                currentActivities = new();
                BabyDay currentBabyDay = currentBaby.BabyDays.FirstOrDefault(d => d.Date == day.AddDays(-i));

                if (currentBabyDay != null)
                {
                    currentActivities = SqliteDataAccess.LoadActivities(currentBabyDay);
                }

                foreach (var activity in currentActivities)
                {
                    DateTime start = activity.Start.AddDays(i);
                    DateTime end = activity.End.AddDays(i);

                    if (activity.ActivityType == ActivityType.Feed)
                    {
                        activitiesChart.Series["Feeds"].Points.AddXY(i, start, end);
                    }
                    else if (activity.ActivityType == ActivityType.Sleep)
                    {
                        activitiesChart.Series["Sleeps"].Points.AddXY(i, start, end);
                    }
                }               

                CustomiseYAxisLabels(i);
            }
        }

        private void CustomiseYAxisLabels(int i)
        {
            activitiesChart
                .ChartAreas["ChartArea1"].AxisX.CustomLabels
                .Add(i - 0.5, i + 0.5, day.AddDays(-i).ToShortDateString());
        }

        private void SetChartProperties()
        {
            // Set Y axis interval and style
            activitiesChart.ChartAreas["ChartArea1"].AxisY.Interval = 120; // Show 2 hour intervals.
            activitiesChart.ChartAreas["ChartArea1"].AxisY.IntervalType = DateTimeIntervalType.Minutes;
            activitiesChart.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "HH:mm"; // Set the format to show hours and minutes.

            // Set viewable area of Y axis
            activitiesChart.ChartAreas["ChartArea1"].AxisY.Minimum = day.ToOADate();
            activitiesChart.ChartAreas[0].AxisY.IsMarginVisible = false;
        }

        private void SetSeriesProperties()
        {
            // Set Range Bar chart type
            activitiesChart.Series["Feeds"].ChartType = SeriesChartType.RangeBar;
            activitiesChart.Series["Sleeps"].ChartType = SeriesChartType.RangeBar;

            // Set point width
            activitiesChart.Series["Feeds"]["PointWidth"] = "0.6";
            activitiesChart.Series["Sleeps"]["PointWidth"] = "0.6";

            // Set draw style to overlapped
            activitiesChart.Series["Sleeps"]["DrawSideBySide"] = "false";
        }
    }
}
