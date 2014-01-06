namespace TimeTable {
	partial class frmStatist {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatist));
			this.tabHolidays = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lstOddStat = new System.Windows.Forms.ListView();
			this.columnKey = new System.Windows.Forms.ColumnHeader();
			this.columnVal = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lstEvenStat = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.lstHolidays = new System.Windows.Forms.ListView();
			this.columnName = new System.Windows.Forms.ColumnHeader();
			this.columnDate = new System.Windows.Forms.ColumnHeader();
			this.columnNoStudy = new System.Windows.Forms.ColumnHeader();
			this.tabHolidays.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabHolidays
			// 
			this.tabHolidays.Controls.Add(this.tabPage1);
			this.tabHolidays.Controls.Add(this.tabPage2);
			this.tabHolidays.Controls.Add(this.tabPage4);
			this.tabHolidays.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabHolidays.Location = new System.Drawing.Point(0, 0);
			this.tabHolidays.Name = "tabHolidays";
			this.tabHolidays.SelectedIndex = 0;
			this.tabHolidays.Size = new System.Drawing.Size(456, 301);
			this.tabHolidays.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lstOddStat);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(448, 275);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Нечетная (1-ая) неделя";
			// 
			// lstOddStat
			// 
			this.lstOddStat.AutoArrange = false;
			this.lstOddStat.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstOddStat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnKey,
            this.columnVal});
			this.lstOddStat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstOddStat.GridLines = true;
			this.lstOddStat.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstOddStat.Location = new System.Drawing.Point(3, 3);
			this.lstOddStat.Margin = new System.Windows.Forms.Padding(0);
			this.lstOddStat.Name = "lstOddStat";
			this.lstOddStat.Size = new System.Drawing.Size(442, 269);
			this.lstOddStat.TabIndex = 0;
			this.lstOddStat.View = System.Windows.Forms.View.Details;
			this.lstOddStat.SizeChanged += new System.EventHandler(this.lstOddStat_SizeChanged);
			// 
			// columnKey
			// 
			this.columnKey.Width = 230;
			// 
			// columnVal
			// 
			this.columnVal.Width = 208;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.lstEvenStat);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(448, 275);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Четная (2-ая) неделя";
			// 
			// lstEvenStat
			// 
			this.lstEvenStat.AutoArrange = false;
			this.lstEvenStat.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstEvenStat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lstEvenStat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstEvenStat.GridLines = true;
			this.lstEvenStat.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstEvenStat.Location = new System.Drawing.Point(3, 3);
			this.lstEvenStat.Name = "lstEvenStat";
			this.lstEvenStat.Size = new System.Drawing.Size(442, 269);
			this.lstEvenStat.TabIndex = 1;
			this.lstEvenStat.View = System.Windows.Forms.View.Details;
			this.lstEvenStat.SizeChanged += new System.EventHandler(this.lstEvenStat_SizeChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 230;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 208;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.lstHolidays);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(448, 275);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Праздники";
			// 
			// lstHolidays
			// 
			this.lstHolidays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstHolidays.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDate,
            this.columnNoStudy});
			this.lstHolidays.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstHolidays.GridLines = true;
			this.lstHolidays.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstHolidays.Location = new System.Drawing.Point(3, 3);
			this.lstHolidays.MultiSelect = false;
			this.lstHolidays.Name = "lstHolidays";
			this.lstHolidays.ShowGroups = false;
			this.lstHolidays.Size = new System.Drawing.Size(442, 269);
			this.lstHolidays.TabIndex = 0;
			this.lstHolidays.View = System.Windows.Forms.View.Details;
			this.lstHolidays.SizeChanged += new System.EventHandler(this.lstHolidays_SizeChanged);
			// 
			// columnName
			// 
			this.columnName.Text = "Название праздника";
			this.columnName.Width = 290;
			// 
			// columnDate
			// 
			this.columnDate.Text = "Дата";
			// 
			// columnNoStudy
			// 
			this.columnNoStudy.Text = "Выходной?";
			this.columnNoStudy.Width = 70;
			// 
			// frmStatist
			// 
			this.ClientSize = new System.Drawing.Size(456, 301);
			this.Controls.Add(this.tabHolidays);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "frmStatist";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Статистика гр ХХ сем ХХ курс ХХ";
			this.Load += new System.EventHandler(this.frmStatist_Load);
			this.SizeChanged += new System.EventHandler(this.frmStatist_SizeChanged);
			this.tabHolidays.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabHolidays;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView lstOddStat;
		private System.Windows.Forms.ColumnHeader columnKey;
		private System.Windows.Forms.ColumnHeader columnVal;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.ListView lstHolidays;
		private System.Windows.Forms.ColumnHeader columnName;
		private System.Windows.Forms.ColumnHeader columnDate;
		private System.Windows.Forms.ColumnHeader columnNoStudy;
		private System.Windows.Forms.ListView lstEvenStat;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}