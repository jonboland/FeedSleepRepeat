
namespace FeedSleepRepeatUI
{
    partial class NappyChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.nappiesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nappyChart7Button = new System.Windows.Forms.Button();
            this.nappyChart14Button = new System.Windows.Forms.Button();
            this.nappyChart30Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nappiesChart)).BeginInit();
            this.SuspendLayout();
            // 
            // nappiesChart
            // 
            chartArea1.Name = "ChartArea1";
            this.nappiesChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.nappiesChart.Legends.Add(legend1);
            this.nappiesChart.Location = new System.Drawing.Point(12, 12);
            this.nappiesChart.Name = "nappiesChart";
            this.nappiesChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Wet Nappies";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Dirty Nappies";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Total";
            this.nappiesChart.Series.Add(series1);
            this.nappiesChart.Series.Add(series2);
            this.nappiesChart.Series.Add(series3);
            this.nappiesChart.Size = new System.Drawing.Size(776, 426);
            this.nappiesChart.TabIndex = 0;
            this.nappiesChart.Text = "nappyChart";
            // 
            // nappyChart7Button
            // 
            this.nappyChart7Button.Location = new System.Drawing.Point(654, 159);
            this.nappyChart7Button.Name = "nappyChart7Button";
            this.nappyChart7Button.Size = new System.Drawing.Size(108, 38);
            this.nappyChart7Button.TabIndex = 1;
            this.nappyChart7Button.Text = "7 Days";
            this.nappyChart7Button.UseVisualStyleBackColor = true;
            this.nappyChart7Button.Click += new System.EventHandler(this.nappyChart7Button_Click);
            // 
            // nappyChart14Button
            // 
            this.nappyChart14Button.Location = new System.Drawing.Point(654, 233);
            this.nappyChart14Button.Name = "nappyChart14Button";
            this.nappyChart14Button.Size = new System.Drawing.Size(108, 38);
            this.nappyChart14Button.TabIndex = 2;
            this.nappyChart14Button.Text = "14 Days";
            this.nappyChart14Button.UseVisualStyleBackColor = true;
            this.nappyChart14Button.Click += new System.EventHandler(this.nappyChart14Button_Click);
            // 
            // nappyChart30Button
            // 
            this.nappyChart30Button.Location = new System.Drawing.Point(654, 304);
            this.nappyChart30Button.Name = "nappyChart30Button";
            this.nappyChart30Button.Size = new System.Drawing.Size(108, 38);
            this.nappyChart30Button.TabIndex = 3;
            this.nappyChart30Button.Text = "30 Days";
            this.nappyChart30Button.UseVisualStyleBackColor = true;
            this.nappyChart30Button.Click += new System.EventHandler(this.nappyChart30Button_Click);
            // 
            // NappyChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nappyChart30Button);
            this.Controls.Add(this.nappyChart14Button);
            this.Controls.Add(this.nappyChart7Button);
            this.Controls.Add(this.nappiesChart);
            this.Name = "NappyChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nappy Chart";
            ((System.ComponentModel.ISupportInitialize)(this.nappiesChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart nappiesChart;
        private System.Windows.Forms.Button nappyChart7Button;
        private System.Windows.Forms.Button nappyChart14Button;
        private System.Windows.Forms.Button nappyChart30Button;
    }
}