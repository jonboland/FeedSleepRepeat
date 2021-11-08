
namespace FeedSleepRepeatUI
{
    partial class FeedForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateOfBirthLabel = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageBox = new System.Windows.Forms.TextBox();
            this.dateOfBirthPicker = new System.Windows.Forms.DateTimePicker();
            this.dateLabel = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightBox = new System.Windows.Forms.TextBox();
            this.wetNappiesLabel = new System.Windows.Forms.Label();
            this.dirtyNappiesLabel = new System.Windows.Forms.Label();
            this.nappiesTotal = new System.Windows.Forms.TextBox();
            this.nappiesTotalLabel = new System.Windows.Forms.Label();
            this.feedStartLabel = new System.Windows.Forms.Label();
            this.feedEndLabel = new System.Windows.Forms.Label();
            this.feedAmountLabel = new System.Windows.Forms.Label();
            this.feedTypeLabel = new System.Windows.Forms.Label();
            this.feedTypeCombo = new System.Windows.Forms.ComboBox();
            this.babyNameCombo = new System.Windows.Forms.ComboBox();
            this.sleepEndLabel = new System.Windows.Forms.Label();
            this.sleepStartLabel = new System.Windows.Forms.Label();
            this.sleepPlaceBox = new System.Windows.Forms.TextBox();
            this.locationLabel = new System.Windows.Forms.Label();
            this.wetNappiesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dirtyNappiesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.addFeedButton = new System.Windows.Forms.Button();
            this.addSleepButton = new System.Windows.Forms.Button();
            this.activityChartButton = new System.Windows.Forms.Button();
            this.weightChartButton = new System.Windows.Forms.Button();
            this.nappyChartButton = new System.Windows.Forms.Button();
            this.activitiesGroupBox = new System.Windows.Forms.GroupBox();
            this.sleepEndPicker = new System.Windows.Forms.DateTimePicker();
            this.sleepStartPicker = new System.Windows.Forms.DateTimePicker();
            this.feedStartPicker = new System.Windows.Forms.DateTimePicker();
            this.feedEndPicker = new System.Windows.Forms.DateTimePicker();
            this.feedAmountBox = new System.Windows.Forms.TextBox();
            this.activitiesListBox = new System.Windows.Forms.ListBox();
            this.chartsGroupBox = new System.Windows.Forms.GroupBox();
            this.babyNameLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wetNappiesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dirtyNappiesNumericUpDown)).BeginInit();
            this.activitiesGroupBox.SuspendLayout();
            this.chartsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateOfBirthLabel
            // 
            this.dateOfBirthLabel.AutoSize = true;
            this.dateOfBirthLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateOfBirthLabel.Location = new System.Drawing.Point(217, 58);
            this.dateOfBirthLabel.Name = "dateOfBirthLabel";
            this.dateOfBirthLabel.Size = new System.Drawing.Size(90, 19);
            this.dateOfBirthLabel.TabIndex = 99;
            this.dateOfBirthLabel.Text = "Date of birth:";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ageLabel.Location = new System.Drawing.Point(424, 58);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(36, 19);
            this.ageLabel.TabIndex = 99;
            this.ageLabel.Text = "Age:";
            // 
            // ageBox
            // 
            this.ageBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ageBox.Location = new System.Drawing.Point(462, 55);
            this.ageBox.Name = "ageBox";
            this.ageBox.ReadOnly = true;
            this.ageBox.Size = new System.Drawing.Size(97, 25);
            this.ageBox.TabIndex = 99;
            this.ageBox.TabStop = false;
            // 
            // dateOfBirthPicker
            // 
            this.dateOfBirthPicker.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateOfBirthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateOfBirthPicker.Location = new System.Drawing.Point(309, 55);
            this.dateOfBirthPicker.Name = "dateOfBirthPicker";
            this.dateOfBirthPicker.Size = new System.Drawing.Size(98, 25);
            this.dateOfBirthPicker.TabIndex = 1;
            this.dateOfBirthPicker.ValueChanged += new System.EventHandler(this.dateOfBirthPicker_ValueChanged);
            this.dateOfBirthPicker.Enter += new System.EventHandler(this.dateOfBirthPicker_Enter);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateLabel.Location = new System.Drawing.Point(60, 104);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(41, 19);
            this.dateLabel.TabIndex = 99;
            this.dateLabel.Text = "Date:";
            // 
            // datePicker
            // 
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(105, 101);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(98, 25);
            this.datePicker.TabIndex = 2;
            this.datePicker.CloseUp += new System.EventHandler(this.datePicker_CloseUp);
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            this.datePicker.DropDown += new System.EventHandler(this.datePicker_DropDown);
            this.datePicker.Enter += new System.EventHandler(this.datePicker_Enter);
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.weightLabel.Location = new System.Drawing.Point(232, 104);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(75, 19);
            this.weightLabel.TabIndex = 99;
            this.weightLabel.Text = "Weight (g):";
            // 
            // weightBox
            // 
            this.weightBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.weightBox.Location = new System.Drawing.Point(309, 101);
            this.weightBox.Name = "weightBox";
            this.weightBox.Size = new System.Drawing.Size(98, 25);
            this.weightBox.TabIndex = 3;
            this.weightBox.TextChanged += new System.EventHandler(this.weightBox_TextChanged);
            // 
            // wetNappiesLabel
            // 
            this.wetNappiesLabel.AutoSize = true;
            this.wetNappiesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wetNappiesLabel.Location = new System.Drawing.Point(15, 149);
            this.wetNappiesLabel.Name = "wetNappiesLabel";
            this.wetNappiesLabel.Size = new System.Drawing.Size(87, 19);
            this.wetNappiesLabel.TabIndex = 99;
            this.wetNappiesLabel.Text = "Wet nappies:";
            // 
            // dirtyNappiesLabel
            // 
            this.dirtyNappiesLabel.AutoSize = true;
            this.dirtyNappiesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dirtyNappiesLabel.Location = new System.Drawing.Point(214, 149);
            this.dirtyNappiesLabel.Name = "dirtyNappiesLabel";
            this.dirtyNappiesLabel.Size = new System.Drawing.Size(93, 19);
            this.dirtyNappiesLabel.TabIndex = 99;
            this.dirtyNappiesLabel.Text = "Dirty nappies:";
            // 
            // nappiesTotal
            // 
            this.nappiesTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nappiesTotal.Location = new System.Drawing.Point(462, 145);
            this.nappiesTotal.Name = "nappiesTotal";
            this.nappiesTotal.ReadOnly = true;
            this.nappiesTotal.Size = new System.Drawing.Size(97, 25);
            this.nappiesTotal.TabIndex = 99;
            this.nappiesTotal.TabStop = false;
            this.nappiesTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nappiesTotalLabel
            // 
            this.nappiesTotalLabel.AutoSize = true;
            this.nappiesTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nappiesTotalLabel.Location = new System.Drawing.Point(419, 148);
            this.nappiesTotalLabel.Name = "nappiesTotalLabel";
            this.nappiesTotalLabel.Size = new System.Drawing.Size(41, 19);
            this.nappiesTotalLabel.TabIndex = 99;
            this.nappiesTotalLabel.Text = "Total:";
            // 
            // feedStartLabel
            // 
            this.feedStartLabel.AutoSize = true;
            this.feedStartLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedStartLabel.Location = new System.Drawing.Point(17, 49);
            this.feedStartLabel.Name = "feedStartLabel";
            this.feedStartLabel.Size = new System.Drawing.Size(73, 19);
            this.feedStartLabel.TabIndex = 99;
            this.feedStartLabel.Text = "Feed start:";
            // 
            // feedEndLabel
            // 
            this.feedEndLabel.AutoSize = true;
            this.feedEndLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedEndLabel.Location = new System.Drawing.Point(161, 49);
            this.feedEndLabel.Name = "feedEndLabel";
            this.feedEndLabel.Size = new System.Drawing.Size(68, 19);
            this.feedEndLabel.TabIndex = 99;
            this.feedEndLabel.Text = "Feed end:";
            // 
            // feedAmountLabel
            // 
            this.feedAmountLabel.AutoSize = true;
            this.feedAmountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedAmountLabel.Location = new System.Drawing.Point(410, 49);
            this.feedAmountLabel.Name = "feedAmountLabel";
            this.feedAmountLabel.Size = new System.Drawing.Size(78, 19);
            this.feedAmountLabel.TabIndex = 99;
            this.feedAmountLabel.Text = "Amt (ml/g):";
            // 
            // feedTypeLabel
            // 
            this.feedTypeLabel.AutoSize = true;
            this.feedTypeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedTypeLabel.Location = new System.Drawing.Point(299, 49);
            this.feedTypeLabel.Name = "feedTypeLabel";
            this.feedTypeLabel.Size = new System.Drawing.Size(40, 19);
            this.feedTypeLabel.TabIndex = 99;
            this.feedTypeLabel.Text = "Type:";
            // 
            // feedTypeCombo
            // 
            this.feedTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.feedTypeCombo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedTypeCombo.FormattingEnabled = true;
            this.feedTypeCombo.Location = new System.Drawing.Point(340, 47);
            this.feedTypeCombo.Name = "feedTypeCombo";
            this.feedTypeCombo.Size = new System.Drawing.Size(63, 25);
            this.feedTypeCombo.TabIndex = 8;
            // 
            // babyNameCombo
            // 
            this.babyNameCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.babyNameCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.babyNameCombo.FormattingEnabled = true;
            this.babyNameCombo.Location = new System.Drawing.Point(105, 55);
            this.babyNameCombo.Name = "babyNameCombo";
            this.babyNameCombo.Size = new System.Drawing.Size(98, 25);
            this.babyNameCombo.TabIndex = 0;
            this.babyNameCombo.SelectedIndexChanged += new System.EventHandler(this.babyNameCombo_SelectedIndexChanged);
            this.babyNameCombo.TextChanged += new System.EventHandler(this.babyNameCombo_TextChanged);
            // 
            // sleepEndLabel
            // 
            this.sleepEndLabel.AutoSize = true;
            this.sleepEndLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepEndLabel.Location = new System.Drawing.Point(158, 86);
            this.sleepEndLabel.Name = "sleepEndLabel";
            this.sleepEndLabel.Size = new System.Drawing.Size(71, 19);
            this.sleepEndLabel.TabIndex = 99;
            this.sleepEndLabel.Text = "Sleep end:";
            // 
            // sleepStartLabel
            // 
            this.sleepStartLabel.AutoSize = true;
            this.sleepStartLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepStartLabel.Location = new System.Drawing.Point(13, 86);
            this.sleepStartLabel.Name = "sleepStartLabel";
            this.sleepStartLabel.Size = new System.Drawing.Size(76, 19);
            this.sleepStartLabel.TabIndex = 99;
            this.sleepStartLabel.Text = "Sleep start:";
            // 
            // sleepPlaceBox
            // 
            this.sleepPlaceBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepPlaceBox.Location = new System.Drawing.Point(340, 83);
            this.sleepPlaceBox.Name = "sleepPlaceBox";
            this.sleepPlaceBox.Size = new System.Drawing.Size(207, 25);
            this.sleepPlaceBox.TabIndex = 13;
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.locationLabel.Location = new System.Drawing.Point(296, 86);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(43, 19);
            this.locationLabel.TabIndex = 99;
            this.locationLabel.Text = "Place:";
            // 
            // wetNappiesNumericUpDown
            // 
            this.wetNappiesNumericUpDown.Location = new System.Drawing.Point(104, 146);
            this.wetNappiesNumericUpDown.Name = "wetNappiesNumericUpDown";
            this.wetNappiesNumericUpDown.Size = new System.Drawing.Size(99, 25);
            this.wetNappiesNumericUpDown.TabIndex = 4;
            this.wetNappiesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.wetNappiesNumericUpDown.ValueChanged += new System.EventHandler(this.wetNappiesNumericUpDown_ValueChanged);
            // 
            // dirtyNappiesNumericUpDown
            // 
            this.dirtyNappiesNumericUpDown.Location = new System.Drawing.Point(309, 146);
            this.dirtyNappiesNumericUpDown.Name = "dirtyNappiesNumericUpDown";
            this.dirtyNappiesNumericUpDown.Size = new System.Drawing.Size(98, 25);
            this.dirtyNappiesNumericUpDown.TabIndex = 5;
            this.dirtyNappiesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dirtyNappiesNumericUpDown.ValueChanged += new System.EventHandler(this.dirtyNappiesNumericUpDown_ValueChanged);
            // 
            // addFeedButton
            // 
            this.addFeedButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addFeedButton.Location = new System.Drawing.Point(580, 43);
            this.addFeedButton.Name = "addFeedButton";
            this.addFeedButton.Size = new System.Drawing.Size(112, 29);
            this.addFeedButton.TabIndex = 10;
            this.addFeedButton.Text = "Add Feed";
            this.addFeedButton.UseVisualStyleBackColor = true;
            this.addFeedButton.Click += new System.EventHandler(this.addFeedButton_Click);
            // 
            // addSleepButton
            // 
            this.addSleepButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addSleepButton.Location = new System.Drawing.Point(580, 81);
            this.addSleepButton.Name = "addSleepButton";
            this.addSleepButton.Size = new System.Drawing.Size(112, 30);
            this.addSleepButton.TabIndex = 14;
            this.addSleepButton.Text = "Add Sleep";
            this.addSleepButton.UseVisualStyleBackColor = true;
            this.addSleepButton.Click += new System.EventHandler(this.addSleepButton_Click);
            // 
            // activityChartButton
            // 
            this.activityChartButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.activityChartButton.Location = new System.Drawing.Point(17, 24);
            this.activityChartButton.Name = "activityChartButton";
            this.activityChartButton.Size = new System.Drawing.Size(112, 30);
            this.activityChartButton.TabIndex = 19;
            this.activityChartButton.Text = "Activity";
            this.activityChartButton.UseVisualStyleBackColor = true;
            this.activityChartButton.Click += new System.EventHandler(this.activityChartButton_Click);
            // 
            // weightChartButton
            // 
            this.weightChartButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.weightChartButton.Location = new System.Drawing.Point(17, 70);
            this.weightChartButton.Name = "weightChartButton";
            this.weightChartButton.Size = new System.Drawing.Size(112, 30);
            this.weightChartButton.TabIndex = 20;
            this.weightChartButton.Text = "Weight";
            this.weightChartButton.UseVisualStyleBackColor = true;
            this.weightChartButton.Click += new System.EventHandler(this.weightChartButton_Click);
            // 
            // nappyChartButton
            // 
            this.nappyChartButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nappyChartButton.Location = new System.Drawing.Point(17, 115);
            this.nappyChartButton.Name = "nappyChartButton";
            this.nappyChartButton.Size = new System.Drawing.Size(112, 30);
            this.nappyChartButton.TabIndex = 21;
            this.nappyChartButton.Text = "Nappy";
            this.nappyChartButton.UseVisualStyleBackColor = true;
            this.nappyChartButton.Click += new System.EventHandler(this.nappyChartButton_Click);
            // 
            // activitiesGroupBox
            // 
            this.activitiesGroupBox.Controls.Add(this.sleepEndPicker);
            this.activitiesGroupBox.Controls.Add(this.sleepStartPicker);
            this.activitiesGroupBox.Controls.Add(this.feedStartPicker);
            this.activitiesGroupBox.Controls.Add(this.feedEndPicker);
            this.activitiesGroupBox.Controls.Add(this.feedAmountBox);
            this.activitiesGroupBox.Controls.Add(this.activitiesListBox);
            this.activitiesGroupBox.Controls.Add(this.addSleepButton);
            this.activitiesGroupBox.Controls.Add(this.addFeedButton);
            this.activitiesGroupBox.Controls.Add(this.sleepPlaceBox);
            this.activitiesGroupBox.Controls.Add(this.locationLabel);
            this.activitiesGroupBox.Controls.Add(this.sleepEndLabel);
            this.activitiesGroupBox.Controls.Add(this.sleepStartLabel);
            this.activitiesGroupBox.Controls.Add(this.feedTypeCombo);
            this.activitiesGroupBox.Controls.Add(this.feedTypeLabel);
            this.activitiesGroupBox.Controls.Add(this.feedAmountLabel);
            this.activitiesGroupBox.Controls.Add(this.feedEndLabel);
            this.activitiesGroupBox.Controls.Add(this.feedStartLabel);
            this.activitiesGroupBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.activitiesGroupBox.Location = new System.Drawing.Point(12, 210);
            this.activitiesGroupBox.Name = "activitiesGroupBox";
            this.activitiesGroupBox.Size = new System.Drawing.Size(707, 385);
            this.activitiesGroupBox.TabIndex = 6;
            this.activitiesGroupBox.TabStop = false;
            this.activitiesGroupBox.Text = "Activities";
            // 
            // sleepEndPicker
            // 
            this.sleepEndPicker.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepEndPicker.CustomFormat = "HH:mm";
            this.sleepEndPicker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepEndPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sleepEndPicker.Location = new System.Drawing.Point(231, 83);
            this.sleepEndPicker.Name = "sleepEndPicker";
            this.sleepEndPicker.ShowUpDown = true;
            this.sleepEndPicker.Size = new System.Drawing.Size(57, 25);
            this.sleepEndPicker.TabIndex = 12;
            // 
            // sleepStartPicker
            // 
            this.sleepStartPicker.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepStartPicker.CustomFormat = "HH:mm";
            this.sleepStartPicker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleepStartPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sleepStartPicker.Location = new System.Drawing.Point(92, 83);
            this.sleepStartPicker.Name = "sleepStartPicker";
            this.sleepStartPicker.ShowUpDown = true;
            this.sleepStartPicker.Size = new System.Drawing.Size(57, 25);
            this.sleepStartPicker.TabIndex = 11;
            // 
            // feedStartPicker
            // 
            this.feedStartPicker.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedStartPicker.CustomFormat = "HH:mm";
            this.feedStartPicker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedStartPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.feedStartPicker.Location = new System.Drawing.Point(93, 47);
            this.feedStartPicker.Name = "feedStartPicker";
            this.feedStartPicker.ShowUpDown = true;
            this.feedStartPicker.Size = new System.Drawing.Size(57, 25);
            this.feedStartPicker.TabIndex = 6;
            // 
            // feedEndPicker
            // 
            this.feedEndPicker.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedEndPicker.CustomFormat = "HH:mm";
            this.feedEndPicker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedEndPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.feedEndPicker.Location = new System.Drawing.Point(231, 46);
            this.feedEndPicker.Name = "feedEndPicker";
            this.feedEndPicker.ShowUpDown = true;
            this.feedEndPicker.Size = new System.Drawing.Size(57, 25);
            this.feedEndPicker.TabIndex = 7;
            // 
            // feedAmountBox
            // 
            this.feedAmountBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.feedAmountBox.Location = new System.Drawing.Point(490, 46);
            this.feedAmountBox.Name = "feedAmountBox";
            this.feedAmountBox.Size = new System.Drawing.Size(57, 25);
            this.feedAmountBox.TabIndex = 9;
            // 
            // activitiesListBox
            // 
            this.activitiesListBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.activitiesListBox.FormattingEnabled = true;
            this.activitiesListBox.ItemHeight = 17;
            this.activitiesListBox.Location = new System.Drawing.Point(14, 138);
            this.activitiesListBox.Name = "activitiesListBox";
            this.activitiesListBox.Size = new System.Drawing.Size(678, 191);
            this.activitiesListBox.TabIndex = 15;
            // 
            // chartsGroupBox
            // 
            this.chartsGroupBox.Controls.Add(this.activityChartButton);
            this.chartsGroupBox.Controls.Add(this.nappyChartButton);
            this.chartsGroupBox.Controls.Add(this.weightChartButton);
            this.chartsGroupBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chartsGroupBox.Location = new System.Drawing.Point(576, 28);
            this.chartsGroupBox.Name = "chartsGroupBox";
            this.chartsGroupBox.Size = new System.Drawing.Size(143, 166);
            this.chartsGroupBox.TabIndex = 19;
            this.chartsGroupBox.TabStop = false;
            this.chartsGroupBox.Text = "Charts";
            // 
            // babyNameLabel
            // 
            this.babyNameLabel.AutoSize = true;
            this.babyNameLabel.Location = new System.Drawing.Point(21, 58);
            this.babyNameLabel.Name = "babyNameLabel";
            this.babyNameLabel.Size = new System.Drawing.Size(80, 19);
            this.babyNameLabel.TabIndex = 99;
            this.babyNameLabel.Text = "Baby name:";
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.createButton.Location = new System.Drawing.Point(447, 616);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(112, 30);
            this.createButton.TabIndex = 17;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deleteButton.Location = new System.Drawing.Point(26, 616);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(112, 30);
            this.deleteButton.TabIndex = 16;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updateButton.Location = new System.Drawing.Point(593, 616);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(112, 30);
            this.updateButton.TabIndex = 18;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // FeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(733, 664);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.babyNameLabel);
            this.Controls.Add(this.chartsGroupBox);
            this.Controls.Add(this.activitiesGroupBox);
            this.Controls.Add(this.dirtyNappiesNumericUpDown);
            this.Controls.Add(this.wetNappiesNumericUpDown);
            this.Controls.Add(this.babyNameCombo);
            this.Controls.Add(this.nappiesTotal);
            this.Controls.Add(this.nappiesTotalLabel);
            this.Controls.Add(this.dirtyNappiesLabel);
            this.Controls.Add(this.wetNappiesLabel);
            this.Controls.Add(this.weightBox);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.dateOfBirthPicker);
            this.Controls.Add(this.ageBox);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.dateOfBirthLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FeedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Feed Sleep Repeat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FeedForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.wetNappiesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dirtyNappiesNumericUpDown)).EndInit();
            this.activitiesGroupBox.ResumeLayout(false);
            this.activitiesGroupBox.PerformLayout();
            this.chartsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label dateOfBirthLabel;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.TextBox ageBox;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.TextBox weightBox;
        private System.Windows.Forms.Label wetNappiesLabel;
        private System.Windows.Forms.Label dirtyNappiesLabel;
        private System.Windows.Forms.TextBox nappiesTotal;
        private System.Windows.Forms.Label nappiesTotalLabel;
        private System.Windows.Forms.Label feedStartLabel;
        private System.Windows.Forms.Label feedEndLabel;
        private System.Windows.Forms.Label feedAmountLabel;
        private System.Windows.Forms.Label feedTypeLabel;
        private System.Windows.Forms.ComboBox feedTypeCombo;
        private System.Windows.Forms.ComboBox babyNameCombo;
        private System.Windows.Forms.Label sleepEndLabel;
        private System.Windows.Forms.Label sleepStartLabel;
        private System.Windows.Forms.TextBox sleepPlaceBox;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.NumericUpDown wetNappiesNumericUpDown;
        private System.Windows.Forms.NumericUpDown dirtyNappiesNumericUpDown;
        private System.Windows.Forms.Button addFeedButton;
        private System.Windows.Forms.Button addSleepButton;
        private System.Windows.Forms.Button activityChartButton;
        private System.Windows.Forms.Button weightChartButton;
        private System.Windows.Forms.Button nappyChartButton;
        private System.Windows.Forms.GroupBox activitiesGroupBox;
        private System.Windows.Forms.GroupBox chartsGroupBox;
        private System.Windows.Forms.Label babyNameLabel;
        private System.Windows.Forms.TextBox feedAmountBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button updateButton;
        internal System.Windows.Forms.DateTimePicker dateOfBirthPicker;
        private System.Windows.Forms.DateTimePicker feedEndPicker;
        private System.Windows.Forms.DateTimePicker feedStartPicker;
        private System.Windows.Forms.ListBox activitiesListBox;
        private System.Windows.Forms.DateTimePicker sleepEndPicker;
        private System.Windows.Forms.DateTimePicker sleepStartPicker;
    }
}

