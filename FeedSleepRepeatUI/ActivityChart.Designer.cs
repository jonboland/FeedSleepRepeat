
namespace FeedSleepRepeatUI
{
    partial class ActivityChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.activitiesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateSelectionBoxLabel = new System.Windows.Forms.Label();
            this.activityChartDatePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.activitiesChart)).BeginInit();
            this.SuspendLayout();
            // 
            // activitiesChart
            // 
            chartArea2.Name = "ChartArea1";
            this.activitiesChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.activitiesChart.Legends.Add(legend2);
            this.activitiesChart.Location = new System.Drawing.Point(12, 12);
            this.activitiesChart.Name = "activitiesChart";
            this.activitiesChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            series3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.Legend = "Legend1";
            series3.Name = "Feeds";
            series3.YValuesPerPoint = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
            series4.Legend = "Legend1";
            series4.Name = "Sleeps";
            series4.YValuesPerPoint = 2;
            this.activitiesChart.Series.Add(series3);
            this.activitiesChart.Series.Add(series4);
            this.activitiesChart.Size = new System.Drawing.Size(776, 426);
            this.activitiesChart.TabIndex = 0;
            this.activitiesChart.Text = "activityChart";
            // 
            // dateSelectionBoxLabel
            // 
            this.dateSelectionBoxLabel.AutoSize = true;
            this.dateSelectionBoxLabel.BackColor = System.Drawing.Color.White;
            this.dateSelectionBoxLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateSelectionBoxLabel.Location = new System.Drawing.Point(699, 116);
            this.dateSelectionBoxLabel.Name = "dateSelectionBoxLabel";
            this.dateSelectionBoxLabel.Size = new System.Drawing.Size(77, 15);
            this.dateSelectionBoxLabel.TabIndex = 1;
            this.dateSelectionBoxLabel.Text = "Change date:";
            // 
            // activityChartDatePicker
            // 
            this.activityChartDatePicker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activityChartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.activityChartDatePicker.Location = new System.Drawing.Point(700, 140);
            this.activityChartDatePicker.Name = "activityChartDatePicker";
            this.activityChartDatePicker.Size = new System.Drawing.Size(76, 23);
            this.activityChartDatePicker.TabIndex = 2;
            this.activityChartDatePicker.CloseUp += new System.EventHandler(this.activityChartDatePicker_CloseUp);
            // 
            // ActivityChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.activityChartDatePicker);
            this.Controls.Add(this.dateSelectionBoxLabel);
            this.Controls.Add(this.activitiesChart);
            this.Name = "ActivityChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activity Chart";
            ((System.ComponentModel.ISupportInitialize)(this.activitiesChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart activitiesChart;
        private System.Windows.Forms.Label dateSelectionBoxLabel;
        private System.Windows.Forms.DateTimePicker activityChartDatePicker;
    }
}