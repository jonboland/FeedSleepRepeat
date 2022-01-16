using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FeedSleepRepeatLibrary;

namespace FeedSleepRepeatUI
{
    public partial class ActivityChart : Form
    {
        private readonly ISqliteDataAccess sqliteDataAccess;
        private readonly Baby currentBaby;
        private readonly DateTime today = DateTime.Today;

        public ActivityChart(ISqliteDataAccess dataAccess, Baby baby)
        {
            sqliteDataAccess = dataAccess;
            currentBaby = baby;
            InitializeComponent();
            SetSeriesProperties();
            SetDatePickerMaxDate();
            SetChartIconAndTitle();
            SetChartProperties(today);
            FillChart(today);
        }

        private void FillChart(DateTime day, int period = 7)
        {
            for (int i = 0; i < period; i++)
            {
                List<Activity> currentActivities = new();
                List<Activity> previousActivities = new();

                DateTime currentDay = day.AddDays(-i);
                DateTime previousDay = day.AddDays(-(i + 1));

                BabyDay currentBabyDay = currentBaby.BabyDays.FirstOrDefault(d => d.Date == currentDay);
                BabyDay previousBabyDay = currentBaby.BabyDays.FirstOrDefault(d => d.Date == previousDay);

                if (currentBabyDay != null)
                {
                    currentActivities = sqliteDataAccess.LoadActivities(currentBabyDay);

                    if (previousBabyDay != null)
                    {
                        AddActivitiesThatSpanTwoDays(currentDay, previousBabyDay, currentActivities, previousActivities);
                    }
                }
                else
                {
                    AddDummyActivityInstance(currentActivities, currentDay);
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

                CustomiseXAxisLabels(day, i);
            }
        }

        /// <summary>
        /// Adds activities from the previous day to the current day if they finish in the current day.
        /// </summary>
        private void AddActivitiesThatSpanTwoDays(
            DateTime currentDay,
            BabyDay previousBabyDay,
            List<Activity> currentActivities,
            List<Activity> previousActivities)
        {
            previousActivities = sqliteDataAccess.LoadActivities(previousBabyDay);
            currentActivities.AddRange(previousActivities.Where(p => p.End > currentDay));
        }

        /// <summary>
        /// Adds a dummy activity instance to days with no recorded activity instances,
        /// so a blank row is shown for the day in the chart.
        /// </summary>
        private void AddDummyActivityInstance(List<Activity> currentActivities, DateTime currentDay)
        {
            currentActivities.Add(new Activity { ActivityType = ActivityType.Feed, Start = currentDay, End = currentDay });
        }

        private void CustomiseXAxisLabels(DateTime day, int i)
        {
            activitiesChart
                .ChartAreas["ChartArea1"].AxisX.CustomLabels
                .Add(i - 0.5, i + 0.5, day.AddDays(-i).ToShortDateString());
        }

        private void SetChartProperties(DateTime day)
        {
            // Set Y axis interval and style
            activitiesChart.ChartAreas["ChartArea1"].AxisY.Interval = 120;
            activitiesChart.ChartAreas["ChartArea1"].AxisY.IntervalType = DateTimeIntervalType.Minutes;
            activitiesChart.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "HH:mm";

            // Set viewable area of Y axis
            activitiesChart.ChartAreas["ChartArea1"].AxisY.Minimum = day.ToOADate();
            activitiesChart.ChartAreas["ChartArea1"].AxisY.Maximum = day.AddDays(1).ToOADate();
            activitiesChart.ChartAreas[0].AxisY.IsMarginVisible = false;
        }

        private void SetSeriesProperties()
        {
            // Set Range Bar chart type
            activitiesChart.Series["Feeds"].ChartType = SeriesChartType.RangeBar;
            activitiesChart.Series["Sleeps"].ChartType = SeriesChartType.RangeBar;

            // Set point width
            activitiesChart.Series["Feeds"]["PointWidth"] = "0.9";
            activitiesChart.Series["Sleeps"]["PointWidth"] = "0.6";

            // Set draw style to overlapped
            activitiesChart.Series["Sleeps"]["DrawSideBySide"] = "false";

            // Set sleeps series colour
            activitiesChart.Series["Sleeps"].Color = Color.FromArgb(170, Color.LightGray);
        }

        private void SetDatePickerMaxDate()
        {
            activityChartDatePicker.ValueChanged -= activityChartDatePicker_ValueChanged;
            activityChartDatePicker.MaxDate = today.AddDays(-6);
            activityChartDatePicker.ValueChanged += activityChartDatePicker_ValueChanged;
        }

        private void SetChartIconAndTitle()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Title title = new();
            title.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            title.Text = currentBaby.FullName;
            activitiesChart.Titles.Add(title);
        }

        private void activityChartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            var selectedDate = activityChartDatePicker.Value.Date.AddDays(6);
            ClearChart();
            SetChartProperties(selectedDate);
            FillChart(selectedDate);
        }

        private void ClearChart()
        {
            foreach (var series in activitiesChart.Series)
            {
                series.Points.Clear();
            }

            activitiesChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Clear();
        }
    }
}
