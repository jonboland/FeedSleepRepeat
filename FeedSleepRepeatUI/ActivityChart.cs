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
        public ActivityChart()
        {
            InitializeComponent();

            DateTime currentData = DateTime.Today;
            activitiesChart.Series["Feeds"].Points.AddXY(1, currentData.AddHours(4.5), currentData.AddHours(5));
            activitiesChart.Series["Sleeps"].Points.AddXY(1, currentData.AddHours(5), currentData.AddHours(7));
            activitiesChart.Series["Feeds"].Points.AddXY(1, currentData.AddHours(9), currentData.AddHours(9.25));
            activitiesChart.Series["Sleeps"].Points.AddXY(1, currentData.AddHours(10), currentData.AddHours(13));

            // Set axis labels
            activitiesChart.Series["Feeds"].Points[0].AxisLabel = " ";
            activitiesChart.Series["Feeds"].Points[1].AxisLabel = " ";

            // Set Range Bar chart type
            activitiesChart.Series["Feeds"].ChartType = SeriesChartType.RangeBar;
            activitiesChart.Series["Sleeps"].ChartType = SeriesChartType.RangeBar;

            // Set point width
            activitiesChart.Series["Feeds"]["PointWidth"] = "0.3";
            activitiesChart.Series["Sleeps"]["PointWidth"] = "0.3";

            // Draw series side-by-side
            activitiesChart.Series["Sleeps"]["DrawSideBySide"] = "false";

            // Set Y axis Minimum and Maximum
            //activityChart.ChartAreas["Default"].AxisY.LabelStyle.Format = "HH:mm";
            //activityChart.ChartAreas["Default"].AxisY.Interval = 0.0834;

            activitiesChart.ChartAreas["ChartArea1"].AxisY.Interval = 60; // Show 1 hour intervals.
            activitiesChart.ChartAreas["ChartArea1"].AxisY.IntervalType = DateTimeIntervalType.Minutes;
            activitiesChart.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "HH:mm"; // Set the format to show hours and minutes.

            // Set Y axis Minimum and Maximum
            //activityChart.ChartAreas["Default"].AxisY.Minimum = currentData.AddDays(-1).ToOADate();
            //activityChart.ChartAreas["Default"].AxisY.Maximum = currentData.AddDays(28).ToOADate();
        }
    }
}
