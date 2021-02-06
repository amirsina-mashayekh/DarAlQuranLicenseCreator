namespace DarAlQuranLicense
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.provinceLabel = new System.Windows.Forms.Label();
			this.province = new System.Windows.Forms.TextBox();
			this.region = new System.Windows.Forms.TextBox();
			this.cityLabel = new System.Windows.Forms.Label();
			this.dQName = new System.Windows.Forms.TextBox();
			this.darAlQuranLabel = new System.Windows.Forms.Label();
			this.numberLabel = new System.Windows.Forms.Label();
			this.dateLabel = new System.Windows.Forms.Label();
			this.number = new System.Windows.Forms.TextBox();
			this.dQManager = new System.Windows.Forms.TextBox();
			this.darAlQuranManagerLabel = new System.Windows.Forms.Label();
			this.doEManager = new System.Windows.Forms.TextBox();
			this.DepartmentOfEducationManagerLabel = new System.Windows.Forms.Label();
			this.cityRadioButton = new System.Windows.Forms.RadioButton();
			this.districtRadioButton = new System.Windows.Forms.RadioButton();
			this.studentInfoBox = new System.Windows.Forms.GroupBox();
			this.background = new System.Windows.Forms.ComboBox();
			this.generate = new System.Windows.Forms.Button();
			this.dateCheckBox = new System.Windows.Forms.CheckBox();
			this.studentPicture = new System.Windows.Forms.PictureBox();
			this.studentCode = new System.Windows.Forms.TextBox();
			this.fatherName = new System.Windows.Forms.TextBox();
			this.customLicenseText = new System.Windows.Forms.TextBox();
			this.customLicenseRadioButton = new System.Windows.Forms.RadioButton();
			this.levelRadioButton = new System.Windows.Forms.RadioButton();
			this.score = new System.Windows.Forms.ComboBox();
			this.level = new System.Windows.Forms.ComboBox();
			this.studentName = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.message = new System.Windows.Forms.TextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.helpButton = new System.Windows.Forms.Button();
			this.date = new System.Windows.Forms.MaskedTextBox();
			this.studentInfoBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.studentPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// provinceLabel
			// 
			this.provinceLabel.AutoSize = true;
			this.provinceLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.provinceLabel.Location = new System.Drawing.Point(12, 9);
			this.provinceLabel.Name = "provinceLabel";
			this.provinceLabel.Size = new System.Drawing.Size(47, 22);
			this.provinceLabel.TabIndex = 0;
			this.provinceLabel.Text = "استان:";
			// 
			// province
			// 
			this.province.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.province.Location = new System.Drawing.Point(109, 6);
			this.province.Name = "province";
			this.province.Size = new System.Drawing.Size(131, 28);
			this.province.TabIndex = 0;
			this.province.Leave += new System.EventHandler(this.UpdateDQInfo);
			// 
			// region
			// 
			this.region.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.region.Location = new System.Drawing.Point(418, 6);
			this.region.Name = "region";
			this.region.Size = new System.Drawing.Size(129, 28);
			this.region.TabIndex = 1;
			this.region.Leave += new System.EventHandler(this.UpdateDQInfo);
			// 
			// cityLabel
			// 
			this.cityLabel.AutoSize = true;
			this.cityLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.cityLabel.Location = new System.Drawing.Point(398, 9);
			this.cityLabel.Name = "cityLabel";
			this.cityLabel.Size = new System.Drawing.Size(14, 22);
			this.cityLabel.TabIndex = 2;
			this.cityLabel.Text = ":";
			// 
			// dQName
			// 
			this.dQName.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.dQName.Location = new System.Drawing.Point(109, 40);
			this.dQName.Name = "dQName";
			this.dQName.Size = new System.Drawing.Size(438, 28);
			this.dQName.TabIndex = 2;
			this.dQName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dQName.Leave += new System.EventHandler(this.UpdateDQInfo);
			// 
			// darAlQuranLabel
			// 
			this.darAlQuranLabel.AutoSize = true;
			this.darAlQuranLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.darAlQuranLabel.Location = new System.Drawing.Point(13, 43);
			this.darAlQuranLabel.Name = "darAlQuranLabel";
			this.darAlQuranLabel.Size = new System.Drawing.Size(61, 22);
			this.darAlQuranLabel.TabIndex = 4;
			this.darAlQuranLabel.Text = "دارالقرآن:";
			// 
			// numberLabel
			// 
			this.numberLabel.AutoSize = true;
			this.numberLabel.Cursor = System.Windows.Forms.Cursors.Help;
			this.numberLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.numberLabel.Location = new System.Drawing.Point(246, 114);
			this.numberLabel.Name = "numberLabel";
			this.numberLabel.Size = new System.Drawing.Size(46, 22);
			this.numberLabel.TabIndex = 6;
			this.numberLabel.Text = "شماره:";
			this.toolTip.SetToolTip(this.numberLabel, "شماره گواهینامه را وارد کنید.");
			// 
			// dateLabel
			// 
			this.dateLabel.AutoSize = true;
			this.dateLabel.Cursor = System.Windows.Forms.Cursors.Default;
			this.dateLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.dateLabel.Location = new System.Drawing.Point(12, 111);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(42, 22);
			this.dateLabel.TabIndex = 8;
			this.dateLabel.Text = "تاریخ:";
			// 
			// number
			// 
			this.number.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.number.Location = new System.Drawing.Point(418, 111);
			this.number.Name = "number";
			this.number.Size = new System.Drawing.Size(129, 28);
			this.number.TabIndex = 3;
			// 
			// dQManager
			// 
			this.dQManager.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.dQManager.Location = new System.Drawing.Point(109, 74);
			this.dQManager.Name = "dQManager";
			this.dQManager.Size = new System.Drawing.Size(131, 28);
			this.dQManager.TabIndex = 5;
			this.dQManager.Leave += new System.EventHandler(this.UpdateDQInfo);
			// 
			// darAlQuranManagerLabel
			// 
			this.darAlQuranManagerLabel.AutoSize = true;
			this.darAlQuranManagerLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.darAlQuranManagerLabel.Location = new System.Drawing.Point(12, 77);
			this.darAlQuranManagerLabel.Name = "darAlQuranManagerLabel";
			this.darAlQuranManagerLabel.Size = new System.Drawing.Size(91, 22);
			this.darAlQuranManagerLabel.TabIndex = 9;
			this.darAlQuranManagerLabel.Text = "مدیر دارالقرآن:";
			// 
			// doEManager
			// 
			this.doEManager.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.doEManager.Location = new System.Drawing.Point(418, 74);
			this.doEManager.Name = "doEManager";
			this.doEManager.Size = new System.Drawing.Size(129, 28);
			this.doEManager.TabIndex = 6;
			this.doEManager.Leave += new System.EventHandler(this.UpdateDQInfo);
			// 
			// DepartmentOfEducationManagerLabel
			// 
			this.DepartmentOfEducationManagerLabel.AutoSize = true;
			this.DepartmentOfEducationManagerLabel.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.DepartmentOfEducationManagerLabel.Location = new System.Drawing.Point(246, 77);
			this.DepartmentOfEducationManagerLabel.Name = "DepartmentOfEducationManagerLabel";
			this.DepartmentOfEducationManagerLabel.Size = new System.Drawing.Size(132, 22);
			this.DepartmentOfEducationManagerLabel.TabIndex = 11;
			this.DepartmentOfEducationManagerLabel.Text = "مدیر آموزش و پرورش:";
			// 
			// cityRadioButton
			// 
			this.cityRadioButton.AutoSize = true;
			this.cityRadioButton.Checked = true;
			this.cityRadioButton.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.cityRadioButton.Location = new System.Drawing.Point(246, 6);
			this.cityRadioButton.Name = "cityRadioButton";
			this.cityRadioButton.Size = new System.Drawing.Size(90, 26);
			this.cityRadioButton.TabIndex = 7;
			this.cityRadioButton.TabStop = true;
			this.cityRadioButton.Text = "شهرستان /";
			this.cityRadioButton.UseVisualStyleBackColor = true;
			this.cityRadioButton.CheckedChanged += new System.EventHandler(this.UpdateDQInfo);
			// 
			// districtRadioButton
			// 
			this.districtRadioButton.AutoSize = true;
			this.districtRadioButton.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.districtRadioButton.Location = new System.Drawing.Point(339, 6);
			this.districtRadioButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.districtRadioButton.Name = "districtRadioButton";
			this.districtRadioButton.Size = new System.Drawing.Size(64, 26);
			this.districtRadioButton.TabIndex = 8;
			this.districtRadioButton.Text = "منطقه";
			this.districtRadioButton.UseVisualStyleBackColor = true;
			this.districtRadioButton.CheckedChanged += new System.EventHandler(this.UpdateDQInfo);
			// 
			// studentInfoBox
			// 
			this.studentInfoBox.Controls.Add(this.background);
			this.studentInfoBox.Controls.Add(this.generate);
			this.studentInfoBox.Controls.Add(this.dateCheckBox);
			this.studentInfoBox.Controls.Add(this.studentPicture);
			this.studentInfoBox.Controls.Add(this.studentCode);
			this.studentInfoBox.Controls.Add(this.fatherName);
			this.studentInfoBox.Controls.Add(this.customLicenseText);
			this.studentInfoBox.Controls.Add(this.customLicenseRadioButton);
			this.studentInfoBox.Controls.Add(this.levelRadioButton);
			this.studentInfoBox.Controls.Add(this.score);
			this.studentInfoBox.Controls.Add(this.level);
			this.studentInfoBox.Controls.Add(this.studentName);
			this.studentInfoBox.Controls.Add(this.label4);
			this.studentInfoBox.Controls.Add(this.label6);
			this.studentInfoBox.Controls.Add(this.label5);
			this.studentInfoBox.Controls.Add(this.label3);
			this.studentInfoBox.Controls.Add(this.label2);
			this.studentInfoBox.Controls.Add(this.label1);
			this.studentInfoBox.Location = new System.Drawing.Point(12, 250);
			this.studentInfoBox.Name = "studentInfoBox";
			this.studentInfoBox.Size = new System.Drawing.Size(460, 236);
			this.studentInfoBox.TabIndex = 12;
			this.studentInfoBox.TabStop = false;
			this.studentInfoBox.Text = "مشخصات قرآن آموز";
			// 
			// background
			// 
			this.background.Cursor = System.Windows.Forms.Cursors.Default;
			this.background.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.background.Enabled = false;
			this.background.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.background.FormattingEnabled = true;
			this.background.Items.AddRange(new object[] {
            "طرح‌زمینه 1",
            "طرح‌زمینه 2",
            "طرح‌زمینه 3",
            "طرح‌زمینه 4",
            "طرح‌زمینه 5",
            "طرح‌زمینه 6",
            "طرح‌زمینه 7",
            "طرح‌زمینه 8",
            "طرح‌زمینه 9",
            "طرح‌زمینه 10"});
			this.background.Location = new System.Drawing.Point(6, 100);
			this.background.Name = "background";
			this.background.Size = new System.Drawing.Size(89, 29);
			this.background.TabIndex = 36;
			this.toolTip.SetToolTip(this.background, "طرح زمینه مورد نظر را انتخاب کنید.");
			// 
			// generate
			// 
			this.generate.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.generate.Location = new System.Drawing.Point(344, 201);
			this.generate.Name = "generate";
			this.generate.Size = new System.Drawing.Size(110, 30);
			this.generate.TabIndex = 35;
			this.generate.Text = "ایجاد گواهینامه";
			this.generate.UseVisualStyleBackColor = true;
			this.generate.Click += new System.EventHandler(this.Generate_Click);
			// 
			// dateCheckBox
			// 
			this.dateCheckBox.AutoSize = true;
			this.dateCheckBox.Checked = true;
			this.dateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.dateCheckBox.Cursor = System.Windows.Forms.Cursors.Help;
			this.dateCheckBox.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.dateCheckBox.Location = new System.Drawing.Point(243, 169);
			this.dateCheckBox.Name = "dateCheckBox";
			this.dateCheckBox.Size = new System.Drawing.Size(211, 26);
			this.dateCheckBox.TabIndex = 8;
			this.dateCheckBox.Text = "سال به صورت دو رقمی درج شود.";
			this.toolTip.SetToolTip(this.dateCheckBox, "برای مثال ۰۱ به جای ۱۴۰۱");
			this.dateCheckBox.UseVisualStyleBackColor = true;
			// 
			// studentPicture
			// 
			this.studentPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.studentPicture.Location = new System.Drawing.Point(6, 135);
			this.studentPicture.Name = "studentPicture";
			this.studentPicture.Size = new System.Drawing.Size(72, 96);
			this.studentPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.studentPicture.TabIndex = 34;
			this.studentPicture.TabStop = false;
			// 
			// studentCode
			// 
			this.studentCode.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.studentCode.Location = new System.Drawing.Point(293, 81);
			this.studentCode.Name = "studentCode";
			this.studentCode.Size = new System.Drawing.Size(45, 28);
			this.studentCode.TabIndex = 2;
			this.studentCode.TextChanged += new System.EventHandler(this.ShowStudentInfo);
			// 
			// fatherName
			// 
			this.fatherName.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.fatherName.Location = new System.Drawing.Point(6, 31);
			this.fatherName.Name = "fatherName";
			this.fatherName.Size = new System.Drawing.Size(131, 28);
			this.fatherName.TabIndex = 1;
			// 
			// customLicenseText
			// 
			this.customLicenseText.Enabled = false;
			this.customLicenseText.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.customLicenseText.Location = new System.Drawing.Point(101, 100);
			this.customLicenseText.Name = "customLicenseText";
			this.customLicenseText.Size = new System.Drawing.Size(106, 28);
			this.customLicenseText.TabIndex = 6;
			// 
			// customLicenseRadioButton
			// 
			this.customLicenseRadioButton.AutoSize = true;
			this.customLicenseRadioButton.Cursor = System.Windows.Forms.Cursors.Help;
			this.customLicenseRadioButton.Location = new System.Drawing.Point(213, 106);
			this.customLicenseRadioButton.Name = "customLicenseRadioButton";
			this.customLicenseRadioButton.Size = new System.Drawing.Size(14, 13);
			this.customLicenseRadioButton.TabIndex = 5;
			this.toolTip.SetToolTip(this.customLicenseRadioButton, "جهت درج عبارت: «موفق به: ...» در گواهینامه از این گزینه استفاده کنید.");
			this.customLicenseRadioButton.UseVisualStyleBackColor = true;
			// 
			// levelRadioButton
			// 
			this.levelRadioButton.AutoSize = true;
			this.levelRadioButton.Checked = true;
			this.levelRadioButton.Cursor = System.Windows.Forms.Cursors.Help;
			this.levelRadioButton.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.levelRadioButton.Location = new System.Drawing.Point(122, 65);
			this.levelRadioButton.Name = "levelRadioButton";
			this.levelRadioButton.Size = new System.Drawing.Size(105, 26);
			this.levelRadioButton.TabIndex = 3;
			this.levelRadioButton.TabStop = true;
			this.levelRadioButton.Text = "گذراندن سطح";
			this.toolTip.SetToolTip(this.levelRadioButton, "جهت درج عبارت: «موفق به گذراندن سطح: ...» در گواهینامه از این گزینه استفاده کنید." +
        "");
			this.levelRadioButton.UseVisualStyleBackColor = true;
			this.levelRadioButton.CheckedChanged += new System.EventHandler(this.LevelRadioButton_CheckedChanged);
			// 
			// score
			// 
			this.score.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.score.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.score.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.score.FormattingEnabled = true;
			this.score.Items.AddRange(new object[] {
            "خیلی خوب",
            "خوب",
            "متوسط"});
			this.score.Location = new System.Drawing.Point(299, 134);
			this.score.Name = "score";
			this.score.Size = new System.Drawing.Size(100, 29);
			this.score.TabIndex = 7;
			// 
			// level
			// 
			this.level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.level.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.level.FormattingEnabled = true;
			this.level.Items.AddRange(new object[] {
            "یک",
            "دو",
            "سه",
            "چهار",
            "پنج",
            "شش",
            "هفت",
            "هشت",
            "نه",
            "ده"});
			this.level.Location = new System.Drawing.Point(6, 65);
			this.level.Name = "level";
			this.level.Size = new System.Drawing.Size(110, 29);
			this.level.TabIndex = 4;
			// 
			// studentName
			// 
			this.studentName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.studentName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.studentName.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.studentName.FormattingEnabled = true;
			this.studentName.IntegralHeight = false;
			this.studentName.Location = new System.Drawing.Point(187, 31);
			this.studentName.Name = "studentName";
			this.studentName.Size = new System.Drawing.Size(200, 29);
			this.studentName.TabIndex = 0;
			this.studentName.SelectedIndexChanged += new System.EventHandler(this.ShowStudentInfo);
			this.studentName.Leave += new System.EventHandler(this.ShowStudentInfo);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.label4.Location = new System.Drawing.Point(233, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 22);
			this.label4.TabIndex = 21;
			this.label4.Text = "موفق به";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.label6.Location = new System.Drawing.Point(222, 137);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(71, 22);
			this.label6.TabIndex = 22;
			this.label6.Text = "شده است.";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.label5.Location = new System.Drawing.Point(405, 137);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 22);
			this.label5.TabIndex = 23;
			this.label5.Text = "با درجه";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.label3.Location = new System.Drawing.Point(344, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 22);
			this.label3.TabIndex = 24;
			this.label3.Text = "دارای کد قرآن آموز";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.label2.Location = new System.Drawing.Point(143, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 22);
			this.label2.TabIndex = 26;
			this.label2.Text = "فرزندِ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.label1.Location = new System.Drawing.Point(393, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 22);
			this.label1.TabIndex = 27;
			this.label1.Text = "قرآن آموز";
			// 
			// message
			// 
			this.message.Cursor = System.Windows.Forms.Cursors.Default;
			this.message.Font = new System.Drawing.Font("Samim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.message.Location = new System.Drawing.Point(11, 488);
			this.message.Multiline = true;
			this.message.Name = "message";
			this.message.ReadOnly = true;
			this.message.Size = new System.Drawing.Size(352, 45);
			this.message.TabIndex = 35;
			this.toolTip.SetToolTip(this.message, "پیغام نرم افزار");
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 32767;
			this.toolTip.InitialDelay = 500;
			this.toolTip.ReshowDelay = 100;
			// 
			// helpButton
			// 
			this.helpButton.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.helpButton.Location = new System.Drawing.Point(397, 496);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 30);
			this.helpButton.TabIndex = 13;
			this.helpButton.Text = "راهنما";
			this.helpButton.UseVisualStyleBackColor = true;
			this.helpButton.Click += new System.EventHandler(this.HelpButton_Click);
			// 
			// date
			// 
			this.date.Font = new System.Drawing.Font("Samim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.date.Location = new System.Drawing.Point(109, 108);
			this.date.Mask = "0000/00/00";
			this.date.Name = "date";
			this.date.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.date.Size = new System.Drawing.Size(131, 28);
			this.date.TabIndex = 36;
			this.date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// MainForm
			// 
			this.AcceptButton = this.generate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(559, 536);
			this.Controls.Add(this.cityLabel);
			this.Controls.Add(this.districtRadioButton);
			this.Controls.Add(this.cityRadioButton);
			this.Controls.Add(this.date);
			this.Controls.Add(this.message);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.studentInfoBox);
			this.Controls.Add(this.doEManager);
			this.Controls.Add(this.DepartmentOfEducationManagerLabel);
			this.Controls.Add(this.dQManager);
			this.Controls.Add(this.darAlQuranManagerLabel);
			this.Controls.Add(this.dateLabel);
			this.Controls.Add(this.number);
			this.Controls.Add(this.numberLabel);
			this.Controls.Add(this.dQName);
			this.Controls.Add(this.darAlQuranLabel);
			this.Controls.Add(this.region);
			this.Controls.Add(this.province);
			this.Controls.Add(this.provinceLabel);
			this.Font = new System.Drawing.Font("Samim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.ShowIcon = false;
			this.Text = "گواهینامه دارالقرآن";
			this.studentInfoBox.ResumeLayout(false);
			this.studentInfoBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.studentPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label provinceLabel;
		private System.Windows.Forms.TextBox province;
		private System.Windows.Forms.TextBox region;
		private System.Windows.Forms.Label cityLabel;
		private System.Windows.Forms.TextBox dQName;
		private System.Windows.Forms.Label darAlQuranLabel;
		private System.Windows.Forms.Label numberLabel;
		private System.Windows.Forms.Label dateLabel;
		private System.Windows.Forms.TextBox number;
		private System.Windows.Forms.TextBox dQManager;
		private System.Windows.Forms.Label darAlQuranManagerLabel;
		private System.Windows.Forms.TextBox doEManager;
		private System.Windows.Forms.Label DepartmentOfEducationManagerLabel;
		private System.Windows.Forms.RadioButton cityRadioButton;
		private System.Windows.Forms.RadioButton districtRadioButton;
		private System.Windows.Forms.GroupBox studentInfoBox;
		private System.Windows.Forms.TextBox customLicenseText;
		private System.Windows.Forms.RadioButton customLicenseRadioButton;
		private System.Windows.Forms.RadioButton levelRadioButton;
		private System.Windows.Forms.ComboBox score;
		private System.Windows.Forms.ComboBox studentName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.PictureBox studentPicture;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.ComboBox level;
		private System.Windows.Forms.TextBox message;
		private System.Windows.Forms.TextBox fatherName;
		private System.Windows.Forms.CheckBox dateCheckBox;
		private System.Windows.Forms.TextBox studentCode;
		private System.Windows.Forms.Button generate;
		private System.Windows.Forms.ComboBox background;
		private System.Windows.Forms.MaskedTextBox date;
	}
}

