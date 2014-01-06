using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using MsdnMag;

namespace TimeTable {
	public class frmMain : System.Windows.Forms.Form {
		#region Controls
		private ContextMenuStrip cmnuAvalTblsItem;
		private NotifyIcon ntfTray;
		private Timer tmrUpdateParaState;
		private System.Windows.Forms.ComboBox cmbWeekDay;
		private System.Windows.Forms.Label lblWeekType;
		private System.Windows.Forms.RadioButton radEven;
		private System.Windows.Forms.RadioButton radOdd;
		private System.Windows.Forms.ListView lstTable;
		private System.Windows.Forms.ColumnHeader columnSubject;
		private System.Windows.Forms.ColumnHeader columnType;
		private System.Windows.Forms.ColumnHeader columnLocation;
		private System.Windows.Forms.MainMenu mnuMainMenu;
		private System.Windows.Forms.MenuItem mnuTimeTable;
		private System.Windows.Forms.MenuItem mnuNewTimeTable;
		private System.Windows.Forms.MenuItem mnuEditTimeTable;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.Label lbl1;
		private System.Windows.Forms.Label lbl2;
		private System.Windows.Forms.Label lbl3;
		private System.Windows.Forms.Label lbl5;
		private System.Windows.Forms.Label lbl6;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.ColumnHeader columnParaNo;
		private System.Windows.Forms.ColumnHeader columnStart;
		private System.Windows.Forms.ColumnHeader columnEnd;
		private System.Windows.Forms.ListView lstRingTbl;
		private System.Windows.Forms.Label lblRings;
		private System.Windows.Forms.ToolTip MainToolTip;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuAbout;
		private System.Windows.Forms.MenuItem mnuInfo;
		private System.Windows.Forms.Label lblTbls;
		private System.Windows.Forms.ListView lstAvalTbls;
		private System.Windows.Forms.ColumnHeader columnTerm;
		private System.Windows.Forms.ColumnHeader columnGroup;
		private System.Windows.Forms.ColumnHeader columnCourse;
		private System.Windows.Forms.MenuItem mnuSetting;
		private System.Windows.Forms.ColumnHeader columnFile;
		private System.Windows.Forms.Label lblAddInfo;
		private System.Windows.Forms.MonthCalendar Calendar;
		private System.Windows.Forms.Label lbl4;
		private MenuItem mnuSaveAsTxt;
		private MenuItem mnuImportTbl;
        //private RaftingContainer leftRaftingContainer;
        //private RaftingContainer leftRaftingContainer1;
        //private RaftingContainer topRaftingContainer;
        //private RaftingContainer bottomRaftingContainer;
		private MenuItem mnuTblFolder;
		private ImageList imglstMenuImages;
		private GraphicMenu GfxMnu;
		private MenuItem menuStatist;
		#endregion		

		Label[] Lbls;

		public static string[] FillNumChr = {	"\u0075",
								"\u0076",
								"\u0077",
								"\u0078",
								"\u0079",
								"\u007A" };
		public static string[] NonFillNumChr = {	"\u006A",
								"\u006B",
								"\u006C",
								"\u006D",
								"\u006E",
								"\u006F" };

		//Unicode символы для шрифта Arial
		public static string DownArrow	= "\u2193";
		public static string UpArrow	= "\u2191";
		public static string LeftArrow	= "\u0060";
		public static string RightArrow = "\u0061";
		public static string Romb		= "\u2666";
		public static string RTriang	= "\u25ba";
		public static string LTriang	= "\u25c4";

		private bool AskForNewTbl		= true;

		private string DefaultLoadTbl;

		/// <summary>Путь к расписанию, которое загружено в данный момонет</summary>
		public static string CurrWorkTblPath;

		/// <summary>
		/// За сколько дней до праздника (с выходным) выводит сообщ. в txtInfo,
		/// что до праздника осталось n дней?
		/// </summary>
		const byte HolidayNotifyDays = 4;
		private ToolStripMenuItem mnuDelAvalTbl;
		private Label lblRingTable;
		private Label lblAvalTbls;
		private Label lblCalendar;

		/// <summary>
		/// Сколько линий текста помещается в txtInfo, прежде 
		/// чем включается вертикальный Scroll bar
		/// </summary>
		const byte txtInfoLinesToScroll = 3;

		// ==================================================================================
		[STAThread]
		public static void Main() {
			Settings.LoadSettings(Settings.CfgFilePath);
			frmMain f = new frmMain();
			f.ShowDialog();
		}

		// ----------------------------------------------------------------------------------
		public frmMain() {
			InitializeComponent();
			Lbls = new Label[] {lbl1, lbl2, lbl3, lbl4, lbl5, lbl6};
		}

		#region Dispose Method
		//--------------------------------------------------------------------------------
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmbWeekDay = new System.Windows.Forms.ComboBox();
            this.lblWeekType = new System.Windows.Forms.Label();
            this.radEven = new System.Windows.Forms.RadioButton();
            this.radOdd = new System.Windows.Forms.RadioButton();
            this.lstTable = new System.Windows.Forms.ListView();
            this.columnSubject = new System.Windows.Forms.ColumnHeader();
            this.columnType = new System.Windows.Forms.ColumnHeader();
            this.columnLocation = new System.Windows.Forms.ColumnHeader();
            this.mnuMainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mnuTimeTable = new System.Windows.Forms.MenuItem();
            this.mnuNewTimeTable = new System.Windows.Forms.MenuItem();
            this.mnuEditTimeTable = new System.Windows.Forms.MenuItem();
            this.mnuImportTbl = new System.Windows.Forms.MenuItem();
            this.mnuTblFolder = new System.Windows.Forms.MenuItem();
            this.mnuSaveAsTxt = new System.Windows.Forms.MenuItem();
            this.mnuInfo = new System.Windows.Forms.MenuItem();
            this.menuStatist = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuSetting = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuAbout = new System.Windows.Forms.MenuItem();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.lstRingTbl = new System.Windows.Forms.ListView();
            this.columnParaNo = new System.Windows.Forms.ColumnHeader();
            this.columnStart = new System.Windows.Forms.ColumnHeader();
            this.columnEnd = new System.Windows.Forms.ColumnHeader();
            this.lblRings = new System.Windows.Forms.Label();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblRingTable = new System.Windows.Forms.Label();
            this.lblAvalTbls = new System.Windows.Forms.Label();
            this.lblCalendar = new System.Windows.Forms.Label();
            this.lblTbls = new System.Windows.Forms.Label();
            this.lstAvalTbls = new System.Windows.Forms.ListView();
            this.columnGroup = new System.Windows.Forms.ColumnHeader();
            this.columnCourse = new System.Windows.Forms.ColumnHeader();
            this.columnTerm = new System.Windows.Forms.ColumnHeader();
            this.columnFile = new System.Windows.Forms.ColumnHeader();
            this.cmnuAvalTblsItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imglstMenuImages = new System.Windows.Forms.ImageList(this.components);
            this.mnuDelAvalTbl = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAddInfo = new System.Windows.Forms.Label();
            this.Calendar = new System.Windows.Forms.MonthCalendar();
            this.lbl4 = new System.Windows.Forms.Label();
            this.ntfTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrUpdateParaState = new System.Windows.Forms.Timer(this.components);
            this.cmnuAvalTblsItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWeekDay
            // 
            this.cmbWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeekDay.FormattingEnabled = true;
            this.cmbWeekDay.Location = new System.Drawing.Point(8, 16);
            this.cmbWeekDay.Name = "cmbWeekDay";
            this.cmbWeekDay.Size = new System.Drawing.Size(208, 21);
            this.cmbWeekDay.TabIndex = 1;
            this.cmbWeekDay.SelectedIndexChanged += new System.EventHandler(this.cmbWeekDay_SelectedIndexChanged);
            // 
            // lblWeekType
            // 
            this.lblWeekType.BackColor = System.Drawing.Color.Transparent;
            this.lblWeekType.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWeekType.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblWeekType.Location = new System.Drawing.Point(8, 40);
            this.lblWeekType.Name = "lblWeekType";
            this.lblWeekType.Size = new System.Drawing.Size(208, 16);
            this.lblWeekType.TabIndex = 0;
            // 
            // radEven
            // 
            this.radEven.BackColor = System.Drawing.Color.Transparent;
            this.radEven.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radEven.Location = new System.Drawing.Point(8, 56);
            this.radEven.Name = "radEven";
            this.radEven.Size = new System.Drawing.Size(104, 16);
            this.radEven.TabIndex = 2;
            this.radEven.Text = "Четная(2)";
            this.radEven.UseVisualStyleBackColor = false;
            this.radEven.CheckedChanged += new System.EventHandler(this.radEven_CheckedChanged);
            // 
            // radOdd
            // 
            this.radOdd.BackColor = System.Drawing.Color.Transparent;
            this.radOdd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radOdd.Location = new System.Drawing.Point(116, 56);
            this.radOdd.Name = "radOdd";
            this.radOdd.Size = new System.Drawing.Size(100, 16);
            this.radOdd.TabIndex = 3;
            this.radOdd.Text = "Нечетная(1)";
            this.radOdd.UseVisualStyleBackColor = false;
            this.radOdd.CheckedChanged += new System.EventHandler(this.radOdd_CheckedChanged);
            // 
            // lstTable
            // 
            this.lstTable.BackColor = System.Drawing.SystemColors.Window;
            this.lstTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSubject,
            this.columnType,
            this.columnLocation});
            this.lstTable.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstTable.FullRowSelect = true;
            this.lstTable.GridLines = true;
            this.lstTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstTable.LabelWrap = false;
            this.lstTable.Location = new System.Drawing.Point(16, 72);
            this.lstTable.MultiSelect = false;
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(200, 117);
            this.lstTable.TabIndex = 0;
            this.lstTable.TabStop = false;
            this.lstTable.UseCompatibleStateImageBehavior = false;
            this.lstTable.View = System.Windows.Forms.View.Details;
            // 
            // columnSubject
            // 
            this.columnSubject.Text = "Предмет";
            this.columnSubject.Width = 103;
            // 
            // columnType
            // 
            this.columnType.Text = "Тип";
            this.columnType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnType.Width = 52;
            // 
            // columnLocation
            // 
            this.columnLocation.Text = "Где";
            this.columnLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnLocation.Width = 45;
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuTimeTable,
            this.mnuSetting,
            this.mnuHelp});
            // 
            // mnuTimeTable
            // 
            this.mnuTimeTable.Index = 0;
            this.mnuTimeTable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNewTimeTable,
            this.mnuEditTimeTable,
            this.mnuImportTbl,
            this.mnuTblFolder,
            this.mnuSaveAsTxt,
            this.mnuInfo,
            this.menuStatist,
            this.mnuExit});
            this.mnuTimeTable.Text = "Расписание";
            // 
            // mnuNewTimeTable
            // 
            this.mnuNewTimeTable.Index = 0;
            this.mnuNewTimeTable.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.mnuNewTimeTable.Text = "Новое расписание ...";
            this.mnuNewTimeTable.Click += new System.EventHandler(this.mnuNewTimeTable_Click);
            // 
            // mnuEditTimeTable
            // 
            this.mnuEditTimeTable.Index = 1;
            this.mnuEditTimeTable.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.mnuEditTimeTable.Text = "Изменить расписание ...";
            this.mnuEditTimeTable.Click += new System.EventHandler(this.mnuEditTimeTable_Click);
            // 
            // mnuImportTbl
            // 
            this.mnuImportTbl.Index = 2;
            this.mnuImportTbl.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.mnuImportTbl.Text = "Импортировать ...";
            this.mnuImportTbl.Click += new System.EventHandler(this.mnuImportTbl_Click);
            // 
            // mnuTblFolder
            // 
            this.mnuTblFolder.Index = 3;
            this.mnuTblFolder.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.mnuTblFolder.Text = "Открыть папку с расписаниями";
            this.mnuTblFolder.Click += new System.EventHandler(this.mnuTblFolder_Click);
            // 
            // mnuSaveAsTxt
            // 
            this.mnuSaveAsTxt.Index = 4;
            this.mnuSaveAsTxt.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.mnuSaveAsTxt.Text = "Сохранить как текст ...";
            this.mnuSaveAsTxt.Click += new System.EventHandler(this.mnuSaveAsTxt_Click);
            // 
            // mnuInfo
            // 
            this.mnuInfo.Index = 5;
            this.mnuInfo.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.mnuInfo.Text = "Доп. информация ...";
            this.mnuInfo.Click += new System.EventHandler(this.mnuInfo_Click);
            // 
            // menuStatist
            // 
            this.menuStatist.Index = 6;
            this.menuStatist.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.menuStatist.Text = "Статистика ...";
            this.menuStatist.Click += new System.EventHandler(this.menuStatist_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 7;
            this.mnuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.mnuExit.Text = "Выход";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuSetting
            // 
            this.mnuSetting.Index = 1;
            this.mnuSetting.Text = "Настройки ...";
            this.mnuSetting.Click += new System.EventHandler(this.mnuSetting_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 2;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbout});
            this.mnuHelp.Text = "Помощь";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Index = 0;
            this.mnuAbout.Text = "О программе ...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Font = new System.Drawing.Font("Wingdings 2", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl1.Location = new System.Drawing.Point(0, 92);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(16, 16);
            this.lbl1.TabIndex = 14;
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.Transparent;
            this.lbl2.Font = new System.Drawing.Font("Wingdings 2", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl2.Location = new System.Drawing.Point(0, 108);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(16, 16);
            this.lbl2.TabIndex = 13;
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.Color.Transparent;
            this.lbl3.Font = new System.Drawing.Font("Wingdings 2", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl3.Location = new System.Drawing.Point(0, 124);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(16, 16);
            this.lbl3.TabIndex = 12;
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl5
            // 
            this.lbl5.BackColor = System.Drawing.Color.Transparent;
            this.lbl5.Font = new System.Drawing.Font("Wingdings 2", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl5.Location = new System.Drawing.Point(0, 156);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(16, 16);
            this.lbl5.TabIndex = 10;
            this.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl6
            // 
            this.lbl6.BackColor = System.Drawing.Color.Transparent;
            this.lbl6.Font = new System.Drawing.Font("Wingdings 2", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl6.Location = new System.Drawing.Point(0, 172);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(16, 16);
            this.lbl6.TabIndex = 9;
            this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.SystemColors.Window;
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtInfo.Location = new System.Drawing.Point(4, 352);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(212, 48);
            this.txtInfo.TabIndex = 8;
            this.txtInfo.TabStop = false;
            this.txtInfo.TextChanged += new System.EventHandler(this.txtInfo_TextChanged);
            // 
            // lstRingTbl
            // 
            this.lstRingTbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRingTbl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnParaNo,
            this.columnStart,
            this.columnEnd});
            this.lstRingTbl.FullRowSelect = true;
            this.lstRingTbl.GridLines = true;
            this.lstRingTbl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstRingTbl.Location = new System.Drawing.Point(48, 224);
            this.lstRingTbl.Name = "lstRingTbl";
            this.lstRingTbl.Size = new System.Drawing.Size(164, 123);
            this.lstRingTbl.TabIndex = 5;
            this.lstRingTbl.UseCompatibleStateImageBehavior = false;
            this.lstRingTbl.View = System.Windows.Forms.View.Details;
            // 
            // columnParaNo
            // 
            this.columnParaNo.Text = "№";
            this.columnParaNo.Width = 22;
            // 
            // columnStart
            // 
            this.columnStart.Text = "Начало";
            this.columnStart.Width = 82;
            // 
            // columnEnd
            // 
            this.columnEnd.Text = "Конец";
            // 
            // lblRings
            // 
            this.lblRings.BackColor = System.Drawing.Color.Transparent;
            this.lblRings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRings.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRings.Location = new System.Drawing.Point(48, 192);
            this.lblRings.Name = "lblRings";
            this.lblRings.Size = new System.Drawing.Size(164, 32);
            this.lblRings.TabIndex = 4;
            this.lblRings.Text = "Звонки";
            this.lblRings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRingTable
            // 
            this.lblRingTable.BackColor = System.Drawing.Color.Transparent;
            this.lblRingTable.Image = global::TimeTable.Properties.Resources.Звонок;
            this.lblRingTable.Location = new System.Drawing.Point(4, 192);
            this.lblRingTable.Name = "lblRingTable";
            this.lblRingTable.Size = new System.Drawing.Size(40, 40);
            this.lblRingTable.TabIndex = 5;
            this.MainToolTip.SetToolTip(this.lblRingTable, "Звонки");
            this.lblRingTable.MouseLeave += new System.EventHandler(this.lbls_MouseLeave);
            this.lblRingTable.Click += new System.EventHandler(this.lblRingTable_Click);
            this.lblRingTable.MouseEnter += new System.EventHandler(this.lbls_MouseEnter);
            // 
            // lblAvalTbls
            // 
            this.lblAvalTbls.BackColor = System.Drawing.Color.Transparent;
            this.lblAvalTbls.Image = global::TimeTable.Properties.Resources.Расписание;
            this.lblAvalTbls.Location = new System.Drawing.Point(4, 252);
            this.lblAvalTbls.Name = "lblAvalTbls";
            this.lblAvalTbls.Size = new System.Drawing.Size(40, 40);
            this.lblAvalTbls.TabIndex = 6;
            this.MainToolTip.SetToolTip(this.lblAvalTbls, "Доступные расписания");
            this.lblAvalTbls.MouseLeave += new System.EventHandler(this.lbls_MouseLeave);
            this.lblAvalTbls.Click += new System.EventHandler(this.lblAvalTbls_Click);
            this.lblAvalTbls.MouseEnter += new System.EventHandler(this.lbls_MouseEnter);
            // 
            // lblCalendar
            // 
            this.lblCalendar.BackColor = System.Drawing.Color.Transparent;
            this.lblCalendar.Image = global::TimeTable.Properties.Resources.Календарь;
            this.lblCalendar.Location = new System.Drawing.Point(4, 308);
            this.lblCalendar.Name = "lblCalendar";
            this.lblCalendar.Size = new System.Drawing.Size(40, 40);
            this.lblCalendar.TabIndex = 7;
            this.MainToolTip.SetToolTip(this.lblCalendar, "Календарь");
            this.lblCalendar.MouseLeave += new System.EventHandler(this.lbls_MouseLeave);
            this.lblCalendar.Click += new System.EventHandler(this.lblCalendar_Click);
            this.lblCalendar.MouseEnter += new System.EventHandler(this.lbls_MouseEnter);
            // 
            // lblTbls
            // 
            this.lblTbls.BackColor = System.Drawing.Color.Transparent;
            this.lblTbls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTbls.Font = new System.Drawing.Font("Verdana", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTbls.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTbls.Location = new System.Drawing.Point(48, 192);
            this.lblTbls.Name = "lblTbls";
            this.lblTbls.Size = new System.Drawing.Size(164, 32);
            this.lblTbls.TabIndex = 3;
            this.lblTbls.Text = "Расписания";
            this.lblTbls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstAvalTbls
            // 
            this.lstAvalTbls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstAvalTbls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnGroup,
            this.columnCourse,
            this.columnTerm,
            this.columnFile});
            this.lstAvalTbls.FullRowSelect = true;
            this.lstAvalTbls.GridLines = true;
            this.lstAvalTbls.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstAvalTbls.Location = new System.Drawing.Point(48, 224);
            this.lstAvalTbls.MultiSelect = false;
            this.lstAvalTbls.Name = "lstAvalTbls";
            this.lstAvalTbls.Size = new System.Drawing.Size(164, 123);
            this.lstAvalTbls.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstAvalTbls.TabIndex = 2;
            this.lstAvalTbls.UseCompatibleStateImageBehavior = false;
            this.lstAvalTbls.View = System.Windows.Forms.View.Details;
            this.lstAvalTbls.ItemActivate += new System.EventHandler(this.lstAvalTbls_ItemActivate);
            this.lstAvalTbls.SelectedIndexChanged += new System.EventHandler(this.lstAvalTbls_SelectedIndexChanged);
            // 
            // columnGroup
            // 
            this.columnGroup.Text = "Гр.";
            this.columnGroup.Width = 55;
            // 
            // columnCourse
            // 
            this.columnCourse.Text = "К.";
            this.columnCourse.Width = 22;
            // 
            // columnTerm
            // 
            this.columnTerm.Text = "С.";
            this.columnTerm.Width = 22;
            // 
            // columnFile
            // 
            this.columnFile.Text = "Файл";
            this.columnFile.Width = 65;
            // 
            // cmnuAvalTblsItem
            // 
            this.cmnuAvalTblsItem.ImageList = this.imglstMenuImages;
            this.cmnuAvalTblsItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelAvalTbl});
            this.cmnuAvalTblsItem.Name = "cmnuAvalTblsItem";
            this.cmnuAvalTblsItem.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmnuAvalTblsItem.ShowCheckMargin = true;
            this.cmnuAvalTblsItem.ShowImageMargin = false;
            this.cmnuAvalTblsItem.Size = new System.Drawing.Size(130, 26);
            // 
            // imglstMenuImages
            // 
            this.imglstMenuImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstMenuImages.ImageStream")));
            this.imglstMenuImages.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstMenuImages.Images.SetKeyName(0, "NewFile.gif");
            this.imglstMenuImages.Images.SetKeyName(1, "OpenFile.gif");
            this.imglstMenuImages.Images.SetKeyName(2, "Info16x15.gif");
            this.imglstMenuImages.Images.SetKeyName(3, "SaveWindbeyStyle16x15.gif");
            this.imglstMenuImages.Images.SetKeyName(4, "Question16x15.gif");
            this.imglstMenuImages.Images.SetKeyName(5, "Change16x15.gif");
            this.imglstMenuImages.Images.SetKeyName(6, "Лист с шестеренкой16x15.gif");
            this.imglstMenuImages.Images.SetKeyName(7, "Отмена.gif");
            // 
            // mnuDelAvalTbl
            // 
            this.mnuDelAvalTbl.Name = "mnuDelAvalTbl";
            this.mnuDelAvalTbl.Size = new System.Drawing.Size(129, 22);
            this.mnuDelAvalTbl.Text = "Удалить";
            this.mnuDelAvalTbl.Click += new System.EventHandler(this.mnuDelAvalTbl_Click);
            // 
            // lblAddInfo
            // 
            this.lblAddInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblAddInfo.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAddInfo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblAddInfo.Location = new System.Drawing.Point(8, 0);
            this.lblAddInfo.Name = "lblAddInfo";
            this.lblAddInfo.Size = new System.Drawing.Size(208, 16);
            this.lblAddInfo.TabIndex = 0;
            this.lblAddInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Calendar
            // 
            this.Calendar.BackColor = System.Drawing.SystemColors.Window;
            this.Calendar.Location = new System.Drawing.Point(48, 192);
            this.Calendar.MaxSelectionCount = 1;
            this.Calendar.Name = "Calendar";
            this.Calendar.TabIndex = 21;
            this.Calendar.TabStop = false;
            this.Calendar.TitleForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Calendar.TrailingForeColor = System.Drawing.SystemColors.Highlight;
            this.Calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar_DateSelected);
            // 
            // lbl4
            // 
            this.lbl4.BackColor = System.Drawing.Color.Transparent;
            this.lbl4.Font = new System.Drawing.Font("Wingdings 2", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lbl4.Location = new System.Drawing.Point(0, 140);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(16, 16);
            this.lbl4.TabIndex = 22;
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ntfTray
            // 
            this.ntfTray.Text = "Расписание пар";
            // 
            // tmrUpdateParaState
            // 
            this.tmrUpdateParaState.Interval = 60000;
            this.tmrUpdateParaState.Tick += new System.EventHandler(this.tmrUpdateParaState_Tick);
            // 
            // frmMain
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(218, 405);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lblAddInfo);
            this.Controls.Add(this.lblRingTable);
            this.Controls.Add(this.lblAvalTbls);
            this.Controls.Add(this.lblCalendar);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.lbl6);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lstTable);
            this.Controls.Add(this.radOdd);
            this.Controls.Add(this.radEven);
            this.Controls.Add(this.lblWeekType);
            this.Controls.Add(this.cmbWeekDay);
            this.Controls.Add(this.lblTbls);
            this.Controls.Add(this.lstAvalTbls);
            this.Controls.Add(this.Calendar);
            this.Controls.Add(this.lblRings);
            this.Controls.Add(this.lstRingTbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mnuMainMenu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Расписание";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cmnuAvalTblsItem.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		//--------------------------------------------------------------------------------
		private void frmMain_Load(object sender, System.EventArgs e) {
			int CurrWeekDay, i;
			DateTime Now, UpdateDate;
			TimeSpan diff, HolidayNotify = TimeSpan.FromDays(HolidayNotifyDays);
			string TmpStr;
			string[] CommandLineArgs;

			Now = DateTime.Now;
			CurrWeekDay = Tbl.WeekDayToInt(Now.DayOfWeek);
			UpdateDate = Settings.LastUpdateDate;
			UpdateDate = UpdateDate.AddDays(-Tbl.WeekDayToInt(UpdateDate.DayOfWeek));
			diff = Now - UpdateDate;
			int w = diff.Days / 7;
			if(w % 2 != 0) { // если прошло нечетное количество недель
				Settings.CurrWeekType = (Settings.CurrWeekType == Tbl.WeekType.CHET) ?
					Tbl.WeekType.NECHET : Tbl.WeekType.CHET;
				Settings.LastUpdateDate = Now;
				Settings.SaveSettings(Settings.CfgFilePath);
			}

			//			 инициализация элементов управления
			Calendar.BringToFront();
			lstTable.Columns[0].Text = DownArrow + "Предмет" + DownArrow;
			lstTable.Columns[1].Text = DownArrow + "Тип" + DownArrow;
			lstTable.Columns[2].Text = DownArrow + "Где" + DownArrow;
			// инициализируем "красивое" меню
			GfxMnu = new GraphicMenu();
			GfxMnu.Init(this.Menu);
			GfxMnu.AddIcon(mnuNewTimeTable, (Bitmap) imglstMenuImages.Images[0]);
			GfxMnu.AddIcon(mnuImportTbl, (Bitmap) imglstMenuImages.Images[1]);
			GfxMnu.AddIcon(mnuTblFolder, (Bitmap) imglstMenuImages.Images[1]);
			GfxMnu.AddIcon(mnuInfo, (Bitmap) imglstMenuImages.Images[2]);
			GfxMnu.AddIcon(mnuSaveAsTxt, (Bitmap) imglstMenuImages.Images[3]);
			GfxMnu.AddIcon(mnuAbout, (Bitmap) imglstMenuImages.Images[4]);
			GfxMnu.AddIcon(mnuEditTimeTable, (Bitmap) imglstMenuImages.Images[5]);
			GfxMnu.AddIcon(menuStatist, (Bitmap) imglstMenuImages.Images[6]);
			GfxMnu.AddIcon(mnuExit, (Bitmap) imglstMenuImages.Images[7]);

			// загружаем в cmbWeekDay дни недели
			cmbWeekDay.Items.Clear();
			for(i = 0; i < Tbl.DAY_IN_WEEK; i++)
				cmbWeekDay.Items.Add(Tbl.WeekDayNames[i]);

			// Заполняем расписание звонками
			lstRingTbl.Items.Clear();
			for(i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				lstRingTbl.Items.Add((i+1).ToString());
				lstRingTbl.Items[i].SubItems.Add(Tbl.ParaStart(i).ToString("t", null));
				lstRingTbl.Items[i].SubItems.Add(Tbl.ParaEnd(i).ToString("t", null));
			}

			// выбираем в cmbWeekDay текущий день недели
			cmbWeekDay.SelectedIndex = CurrWeekDay;

			// загружаем расписание
			if(DefaultLoadTbl == null || DefaultLoadTbl == "") {
				// загружаем из файла расписание по умолчанию
				TmpStr = Settings.DefaultTable;
				TmpStr = string.Format(@"{0}\{1}", Settings.TblsFolder, TmpStr);
				if(File.Exists(TmpStr)) {
					if(!Tbl.LoadTable(TmpStr))
						Close();
					CurrWorkTblPath = TmpStr;
					HightlightTbl(ref Settings.HighlightColors);
				}
				else {
					string str = string.Format("Файл расписания, открываемый по умолчанию ({0}), не существует.\nВыберите в настройках другой и перезапустите программу.", TmpStr);
					MessageBox.Show(str, "Ошибка загрузки", MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
					return;
				}
			}
			else { //if(CurrTblPath != "")
				if(File.Exists(DefaultLoadTbl)) {
					if(!Tbl.LoadTable(DefaultLoadTbl)) Close();
					HightlightTbl(ref Settings.HighlightColors);
					CurrWorkTblPath = DefaultLoadTbl;
					}
				else {
					string str = string.Format("Файл расписания {0}, не существует.", DefaultLoadTbl);
					MessageBox.Show(str, "Ошибка отладки");
					return;
				}
			}

			LoadTblsToListView(ref lstAvalTbls);
			ListViewItem lvi;
			HighlightCurrTblInLstView(ref lstAvalTbls, out lvi);
			lblAddInfo.Text = string.Format("Гр. {0}; Курс {1}; Семестр: {2};",
				lvi.Text, lvi.SubItems[1].Text, lvi.SubItems[2].Text);

			SetParaState(Now);

			// автозапуск
			CommandLineArgs = Environment.GetCommandLineArgs();

			if(CommandLineArgs.Length > 1) {
				if(CommandLineArgs[1] == Settings.AUTOSTART_ARG) {
					ntfTray.Icon = Icon;
					ntfTray.Text = "Расписание пар\n" + txtInfo.Text;
					ntfTray.Visible = true;
					WindowState = FormWindowState.Minimized;
					ShowInTaskbar = false;
// !!!!!!!!!!!!!!!!!!!!!!! доделать сворачивание в трей
				}
			}

			cmbWeekDay.Focus();
			tmrUpdateParaState.Enabled = true;
		}

		// ----------------------------------------------------------------------------------
		/// <summary>
		/// Проверяет, будет ли идти пара, перемена или вообще ничего
		/// (пара еще не началась или уже кончилась) 
		/// на заданную дату и время. Так же проверяет наличие празндиков
		/// и всю эту имформацию выводит в txtInfo
		/// </summary>
		public void SetParaState(DateTime time) {
			DateTime Now = DateTime.Now;
			int CurrWeekDay = Tbl.WeekDayToInt(Now.DayOfWeek);

			// устанавливаем переключатели четной и нечетной нелеи и lblWeekType
			if(Settings.CurrWeekType == Tbl.WeekType.CHET) {
				lblWeekType.Text = "Сейчас четная(2-ая) неделя";
				radEven.Checked = true;
			}
			else {
				lblWeekType.Text = "Сейчас нечетная(1-ая) неделя";
				radOdd.Checked = true;
			}
			lblWeekType.Text += ", " +
				Tbl.WeekDayNames[CurrWeekDay];

			// расписание пусто
			if(Tbl.IsEmpty()) {
				txtInfo.Text = "Расписание не заполнено.\r\nВыберите в меню \"Расписание" +
					"->Изменить расписание...\"\r\n";
				if(AskForNewTbl) {
					DialogResult res;

					res = MessageBox.Show("Ваше расписание пусто.\n" + 
						"Желаете ли вы заполнить расписание?", "", 
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if(res == DialogResult.Yes)
						mnuEditTimeTable_Click(null, null);
					AskForNewTbl = false;
				}
			}
			// сегодня выходной
			else if(Tbl.ParaCount(CurrWeekDay, Settings.CurrWeekType) == 0) {
				txtInfo.Text = "Сегодня выходной! Закрывай эту несчастную программу и отдыхай!\r\n";
				txtInfo.ForeColor = Color.Green;
				if(cmbWeekDay.SelectedIndex == Tbl.DAY_IN_WEEK - 1)
					cmbWeekDay.SelectedIndex = 0;
				else 
					cmbWeekDay.SelectedIndex++;
			}
			// пары еще не начались
			else if(Now < Tbl.ParaStart(0)) {
				txtInfo.Text = "Сегодня пары еще не начались.\r\n";
			}
			// пары уже кончились
			else if(Now > Tbl.ParaEnd(Tbl.LastPara(CurrWeekDay, Settings.CurrWeekType))) {
				txtInfo.Text = "Сегодня пары уже кончились.\r\n";
				if(cmbWeekDay.SelectedIndex == Tbl.DAY_IN_WEEK - 1)
					cmbWeekDay.SelectedIndex = 0;
				else cmbWeekDay.SelectedIndex++;
			}
			// пары идут в данный момент
			else {
				int i;

				for(i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
					if(Tbl.ParaStart(i) <= Now && Now <= Tbl.ParaEnd(i)) {
						string subj;

						subj = Tbl.Table[(int) Settings.CurrWeekType, CurrWeekDay, i].Name;
						if(subj == null)
							txtInfo.Text = string.Format("Сейчас идет {0} пара\r\n", i + 1);
						else {
							string stype;

							stype = Tbl.ParaTypeNames[
								(int) Tbl.Table[(int) Settings.CurrWeekType, CurrWeekDay, i].Type];
							txtInfo.Text = string.Format("Сейчас идет {0} пара - {1} по {2}\r\n",
								i + 1, stype, subj);
							txtInfo.ForeColor = Color.Red;
						}
					}
				}
				for(i = 0; i < Tbl.MAX_PARA_COUNT - 1; i++) {
					if(Tbl.ParaEnd(i) < Now && Now < Tbl.ParaStart(i + 1)) {
						i++;
						txtInfo.Text = string.Format(
							"Сейчас перемена между {0} и {1} парой.\r\n", i, i + 1);
						txtInfo.ForeColor = Color.Salmon;
						break;
					}
				}
			}
		}

		// ----------------------------------------------------------------------------------
		/// <summary>Загружает список доступных расписаний в ListView</summary>
		public static void LoadTblsToListView(ref ListView lv) {
			DirectoryInfo DirInf;
			FileInfo [] Files;
			Tbl.TblHeader TblHead;
			int x = 0;
			string TmpStr;

			DirInf = new DirectoryInfo(
				string.Format(@"{0}\{1}", Environment.CurrentDirectory, Settings.TblsFolder));
			Files = DirInf.GetFiles();
			lv.Items.Clear();
			lv.Sorting = SortOrder.None;
			foreach(FileInfo f in Files) {
				TmpStr = string.Format(@"{0}\{1}", Settings.TblsFolder, f.Name);
				if(!ReadTblHeader(TmpStr, out TblHead)) {
					MessageBox.Show(string.Format("Поврежден файл расписания:\n {0}", f.Name));
					continue;
				}
				lv.Items.Add(TblHead.Group);
				x = lv.Items.Count - 1;
				lv.Items[x].SubItems.Add(TblHead.Course.ToString());
				lv.Items[x].SubItems.Add(TblHead.Term.ToString());
				lv.Items[x].SubItems.Add(f.Name);
			}
			lv.Sorting = SortOrder.Ascending;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Показывает расписание на день, загружая в ListView lstTable расписание на
		/// день номер Day (дни нумеруются с 0)
		/// </summary>
		private void ShowDayTable(int Day, Tbl.WeekType wt) {
			int _wt = (int) wt;

			lstTable.Items.Clear();
			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				lstTable.Items.Add(Tbl.Table[_wt, Day, i].Name);
				if(Tbl.Table[_wt, Day, i].Name == null) {
					Lbls[i].Text = NonFillNumChr[i];
					continue;
				}
				Lbls[i].Text = FillNumChr[i];
				lstTable.Items[i].SubItems.Add( 
					Tbl.ParaTypeNames[(int) Tbl.Table[_wt, Day, i].Type]);
				lstTable.Items[i].SubItems.Add(Tbl.Table[_wt, Day, i].Location);
				if(Settings.HighlightMode)
					lstTable.Items[i].ForeColor = 
						Settings.HighlightColors[(int) Tbl.Table[_wt, Day, i].Type];
				else
					lstTable.Items[i].ForeColor = SystemColors.WindowText;
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Вызывается при создании/изменении расписания для отображения нового расписания
		/// </summary>
		private void ReloadTable() {
			DefaultLoadTbl = "";
			frmMain_Load(null, null);
		}

		//--------------------------------------------------------------------------------
		private static bool ReadTblHeader(string TblPath, out Tbl.TblHeader Info) {
			XPathDocument Tbl = new XPathDocument(TblPath);
			XPathNavigator nav = Tbl.CreateNavigator();
			XPathNodeIterator nodes = nav.Select("/TimeTable/Ver|/TimeTable/Group|/TimeTable/Course|/TimeTable/Term");
			int Major, Minor;
			string TmpStr;
			
			nodes.MoveNext();
			Info = new Tbl.TblHeader();
			if(nodes.Current.Value == "") return false;
			TmpStr = nodes.Current.Value;
			Major = Convert.ToInt32(TmpStr.Split('.')[0]);
			Minor = Convert.ToInt32(TmpStr.Split('.')[1]);
			Info.Ver = new Version(Major, Minor);
			nodes.MoveNext();
			Info.Group = nodes.Current.Value;
			nodes.MoveNext();
			Info.Course = Convert.ToInt32(nodes.Current.Value);
			nodes.MoveNext();
			Info.Term = Convert.ToInt32(nodes.Current.Value);
			return true;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Изменяет цвет шрифта на SystemColor.Hightlight в ListView'е с доступными
		/// расписаниями.
		/// </summary>
		/// <param name="lv">Возвращает выделенные элемент</param>
		public static void HighlightCurrTblInLstView(ref ListView lst, out ListViewItem lv) {
			if(Tbl.LoadedTbl == null || Tbl.LoadedTbl == "") {
				lv = null;
				return;
			}
			string a = Tbl.LoadedTbl.Split('\\')[1];

			lv = null;
			foreach(ListViewItem j in lst.Items) {
				if(j.SubItems[3].Text == a) {
					j.ForeColor = SystemColors.Highlight;
					lv = j;
				}
				else 
					j.ForeColor = SystemColors.WindowText;
			}
		}
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Подцвечивает пары в таблице расписания заданным массивом цветов
		/// </summary>
		private void HightlightTbl(ref Color[] HLColors) {
			int CurrParaType;

			foreach(ListViewItem i in lstTable.Items) {
				if(i.SubItems[0].Text == "") continue;
				CurrParaType = Tbl.ParaTypeNameToInt(i.SubItems[1].Text);
				i.ForeColor = HLColors[CurrParaType];
			}
		}


		// ==================================================================================
		//								обработчики событий
		// ==================================================================================

		private void tmrUpdateParaState_Tick(object sender, EventArgs e) {
			SetParaState(DateTime.Now);
		}
		//--------------------------------------------------------------------------------
		private void cmbWeekDay_SelectedIndexChanged(object sender, System.EventArgs e) {
			Tbl.WeekType wt;
			int CurrDay = Tbl.WeekDayToInt(DateTime.Now.DayOfWeek);
			int x;

			if(radEven.Checked) wt = Tbl.WeekType.CHET;
			else wt = Tbl.WeekType.NECHET;
			ShowDayTable(cmbWeekDay.SelectedIndex, wt);
			x = cmbWeekDay.SelectedIndex - CurrDay;
			Calendar.SelectionStart = DateTime.Now.Date.AddDays(x);
		}
		//--------------------------------------------------------------------------------
		private void radEven_CheckedChanged(object sender, System.EventArgs e) {
			ShowDayTable(cmbWeekDay.SelectedIndex, Tbl.WeekType.CHET);
			cmbWeekDay.Focus();
		}
		//--------------------------------------------------------------------------------
		private void radOdd_CheckedChanged(object sender, System.EventArgs e) {
			ShowDayTable(cmbWeekDay.SelectedIndex, Tbl.WeekType.NECHET);
			cmbWeekDay.Focus();
		}
		//--------------------------------------------------------------------------------
		private void lbls_MouseEnter(object sender, System.EventArgs e) {
			Label lbl = sender as Label;

			lbl.BackColor = SystemColors.InactiveCaptionText;
			lbl.BorderStyle = BorderStyle.FixedSingle;
		}
		//--------------------------------------------------------------------------------
		private void lbls_MouseLeave(object sender, System.EventArgs e) {
			Label lbl = sender as Label;

			lbl.BackColor = SystemColors.Window;
			lbl.BorderStyle = BorderStyle.None;
		}
		//--------------------------------------------------------------------------------
		private void lblCalendar_Click(object sender, System.EventArgs e) {
			Calendar.Visible	= true;
			lstRingTbl.Visible	= false;
			lblRings.Visible	= false;
			lblTbls.Visible		= false;
			lstAvalTbls.Visible = false;
			cmbWeekDay.Focus();
		}
		//--------------------------------------------------------------------------------
		private void lblRingTable_Click(object sender, System.EventArgs e) {
			Calendar.Visible	= false;
			lblRings.Visible	= true;
			lstRingTbl.Visible	= true;
			lblTbls.Visible		= false;
			lstAvalTbls.Visible = false;
		}
		//--------------------------------------------------------------------------------
		private void lblAvalTbls_Click(object sender, System.EventArgs e) {
			Calendar.Visible	= false;
			lstRingTbl.Visible	= false;
			lblRings.Visible	= false;
			lblTbls.Visible		= true;
			lstAvalTbls.Visible = true;
			lstAvalTbls.Focus();
		}
		//--------------------------------------------------------------------------------
		private void Calendar_DateSelected(object sender, 
			System.Windows.Forms.DateRangeEventArgs e) 
		{
			cmbWeekDay.SelectedIndex = 
				Tbl.WeekDayToInt(Calendar.SelectionStart.DayOfWeek);
			cmbWeekDay.Focus();
		}
		//--------------------------------------------------------------------------------
		private void lstAvalTbls_ItemActivate(object sender, System.EventArgs e) {
			DefaultLoadTbl = 
				Settings.TblsFolder + "\\" + lstAvalTbls.SelectedItems[0].SubItems[3].Text;
			frmMain_Load(null, null);
			cmbWeekDay.Focus();
		}
		//--------------------------------------------------------------------------------
		private void lstAvalTbls_SelectedIndexChanged(object sender, System.EventArgs e) {
            if(lstAvalTbls.SelectedIndices.Count == 0) {
                lstAvalTbls.ContextMenuStrip = null;
                return;
            } 
            else {
                    lstAvalTbls.ContextMenuStrip = cmnuAvalTblsItem;
            }

			if(lstAvalTbls.SelectedIndices[0] == -1)
				CurrWorkTblPath = "";
			else
				CurrWorkTblPath = Settings.TblsFolder + @"\" + 
					lstAvalTbls.Items[lstAvalTbls.SelectedIndices[0]].SubItems[3];
		}
		//--------------------------------------------------------------------------------
		private void txtInfo_TextChanged(object sender, EventArgs e) {
			if(txtInfo.Lines.Length > txtInfoLinesToScroll)
				txtInfo.ScrollBars = ScrollBars.Vertical;
		}

		//	========================================================================
		//									меню
		//	========================================================================

		private void mnuNewTimeTable_Click(object sender, System.EventArgs e) {
			frmEditTbl dlg = new frmEditTbl(DefaultLoadTbl);

			dlg.Mode = frmEditTbl.EditMode.NEW_TABLE;
			DialogResult r = dlg.ShowDialog();
			if(r == DialogResult.OK)
				ReloadTable();
		}
		//--------------------------------------------------------------------------------
		private void mnuEditTimeTable_Click(object sender, System.EventArgs e) {
			frmEditTbl dlg = new frmEditTbl(DefaultLoadTbl);
			
			dlg.Mode = frmEditTbl.EditMode.EDIT_TABLE;
			if(dlg.ShowDialog() == DialogResult.OK) {
				DefaultLoadTbl = CurrWorkTblPath;
				ReloadTable();
			}
		}
		//--------------------------------------------------------------------------------
		private void mnuExit_Click(object sender, System.EventArgs e) {
			this.Close();
		}
		//--------------------------------------------------------------------------------
		private void mnuAbout_Click(object sender, System.EventArgs e) {
			frmAbout dlg = new frmAbout();
			dlg.ShowDialog();
		}
		//--------------------------------------------------------------------------------
		private void mnuInfo_Click(object sender, System.EventArgs e) {
			if(CurrWorkTblPath == "" || CurrWorkTblPath == null)
				return;

			frmEditInfo dlg = new frmEditInfo(false, true);
			dlg.ShowDialog();
		}
		//--------------------------------------------------------------------------------
		private void mnuSetting_Click(object sender, System.EventArgs e) {
			frmSettings dlg = new frmSettings();
			dlg.ShowDialog();
			ReloadTable();
		}
		//--------------------------------------------------------------------------------
		private void menuStatist_Click(object sender, EventArgs e) {
			if(CurrWorkTblPath == "" || CurrWorkTblPath == null)
				return;

			frmStatist dlg = new frmStatist();

				dlg.ShowDialog();
		}
		//--------------------------------------------------------------------------------
		private void mnuSaveAsTxt_Click(object sender, EventArgs e) {
			SaveFileDialog dlg = new SaveFileDialog();

			dlg.CheckPathExists = false;
			dlg.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
			dlg.RestoreDirectory = true;
			dlg.Title = "Сохранить расписание как текст";

			if(dlg.ShowDialog() == DialogResult.OK)
				Tbl.SaveAsText(dlg.FileName);
		}
		//--------------------------------------------------------------------------------
		private void mnuImportTbl_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();

			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Filter = "Файлы расписания (*.xml)|*.xml|Все файлы (*.*)|*.*";
			dlg.RestoreDirectory = true;
			dlg.Title = "Выберите расписание";
			if(dlg.ShowDialog() == DialogResult.OK) {
				Tbl.ExportTbl(dlg.FileName);
				LoadTblsToListView(ref lstAvalTbls);
				ListViewItem lvi;
				HighlightCurrTblInLstView(ref lstAvalTbls, out lvi);
			}
		}
		//--------------------------------------------------------------------------------
		private void mnuTblFolder_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start("explorer", Path.GetFullPath(Settings.TblsFolder));
		}

		//--------------------------------------------------------------------------------
		private void mnuDelAvalTbl_Click(object sender, EventArgs e) {
			if(lstAvalTbls.SelectedItems[0].ForeColor == SystemColors.Highlight) 
				return;

			string s = Settings.TblsFolder + @"\" +
					lstAvalTbls.SelectedItems[0].SubItems[3].Text;
			File.Delete(s);
			lstAvalTbls.Items.Remove(lstAvalTbls.SelectedItems[0]);
		}
	}
}