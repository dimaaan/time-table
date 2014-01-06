using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace TimeTable {
	public class frmSettings : System.Windows.Forms.Form {

		/// <summary>Массив кнопок-закладок</summary>
		private Label[] lblButtons;

		/// <summary>Индекс элемента массива lblButtion, который сейчас выделен</summary>
		private int SelectedLblButton = -1;

		/// <summary>Показывает какая из кнопок-закладок в фокусе (-1 если никакая)</summary>
		private int FocusedBtn = -1;

		/// <summary>Массив label'ей с цветами подцветки</summary>
		private Label[] arrlblCol;

		/// <summary>Массив label'ей с названиями предметов</summary>
		private Label[] arrlblSubj;

		private const string WindowCaption = "Настройки";
		private IContainer components;

		//==================================================================================

		#region Contorls


		private System.Windows.Forms.Label panOptionsMenu;
		private System.Windows.Forms.Label lblDefTbl;
		private System.Windows.Forms.Label lblHighlight;
		private System.Windows.Forms.Panel panLine;
		private System.Windows.Forms.Panel panDefTbl;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblDefTblHead;
		private System.Windows.Forms.Panel panHighlight;
		private System.Windows.Forms.ListView lstAvalTbls;
		private System.Windows.Forms.ColumnHeader columnGroup;
		private System.Windows.Forms.ColumnHeader columnCourse;
		private System.Windows.Forms.ColumnHeader columnTerm;
		private System.Windows.Forms.ColumnHeader columnFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkHighlight;
		private System.Windows.Forms.Label lblColLection;
		private System.Windows.Forms.Label lblLection;
		private System.Windows.Forms.Label lblColPractize;
		private System.Windows.Forms.Label lblColSeminar;
		private System.Windows.Forms.Label lblColLab;
		private System.Windows.Forms.Label lblColConsult;
		private System.Windows.Forms.Label lblColAdditLesson;
		private System.Windows.Forms.Label lblPractize;
		private System.Windows.Forms.Label lblSeminar;
		private System.Windows.Forms.Label lblLab;
		private System.Windows.Forms.Label lblConsult;
		private System.Windows.Forms.ColorDialog ColorDlg;
		private Panel panCalls;
		private Label lblCalls;
		private Label lblFirstPara;
		private TextBox txtParaStartMinutes;
		private Label lblHours;
		private TextBox txtParaStartHours;
		private Label lblMinutes;
		private Label lblBigBreakLen;
		private Label lblMinutes2;
		private TextBox txtBreakLen;
		private Label lblBreakLen;
		private Label lblMinutes3;
		private TextBox txtBigBreakLen;
		private Label lblMinutes4;
		private TextBox txtParaLen;
		private Label lblParaLen;
		private Panel panel1;
		private Label label8;
		private Panel panel2;
		private Label lblWeekType;
		private Panel panWeekType;
		private Label lblWeekTypeHead;
		private RadioButton radEven;
		private Label lblTray;
		private Panel panTray;
		private CheckBox chkAutostart;
		private GroupBox grpAutostart;
		private CheckBox chkNotifyOnParaStart;
		private CheckBox chkNotifyOnParaEnd;
		private RadioButton radOdd;
		private CheckBox chkParaStartSnd;
		private Button btnSelParaStartSound;
		private Button btnSelParaEndSound;
		private CheckBox chkParaEndSnd;
		private Label lblTrayHead;
		private ToolTip tlpMain;
		private PictureBox pictureBox1;
		private PictureBox pictureBox2;
		private PictureBox pictureBox3;
		private PictureBox pictureBox5;
		private PictureBox pictureBox4;
		private System.Windows.Forms.Label lblAdditLesson;
		#endregion

		public frmSettings() {
			InitializeComponent();

			arrlblCol = new Label[] {lblColLection, lblColPractize, lblColSeminar,
										lblColLab, lblColConsult, lblColAdditLesson};
			arrlblSubj = new Label[] {lblLection, lblPractize, lblSeminar, lblLab, 
										 lblConsult, lblAdditLesson};
			lblButtons = new Label[] {lblDefTbl, lblHighlight, lblCalls, lblWeekType, lblTray};
		}

		#region Dispose Method
		/// <summary>Clean up any resources being used.</summary>
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
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
			this.panOptionsMenu = new System.Windows.Forms.Label();
			this.lblDefTbl = new System.Windows.Forms.Label();
			this.panLine = new System.Windows.Forms.Panel();
			this.lblHighlight = new System.Windows.Forms.Label();
			this.panDefTbl = new System.Windows.Forms.Panel();
			this.lblDefTblHead = new System.Windows.Forms.Label();
			this.lstAvalTbls = new System.Windows.Forms.ListView();
			this.columnGroup = new System.Windows.Forms.ColumnHeader();
			this.columnCourse = new System.Windows.Forms.ColumnHeader();
			this.columnTerm = new System.Windows.Forms.ColumnHeader();
			this.columnFile = new System.Windows.Forms.ColumnHeader();
			this.btnOK = new System.Windows.Forms.Button();
			this.panHighlight = new System.Windows.Forms.Panel();
			this.lblAdditLesson = new System.Windows.Forms.Label();
			this.lblConsult = new System.Windows.Forms.Label();
			this.lblLab = new System.Windows.Forms.Label();
			this.lblSeminar = new System.Windows.Forms.Label();
			this.lblPractize = new System.Windows.Forms.Label();
			this.lblColAdditLesson = new System.Windows.Forms.Label();
			this.chkHighlight = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblLection = new System.Windows.Forms.Label();
			this.lblColConsult = new System.Windows.Forms.Label();
			this.lblColLection = new System.Windows.Forms.Label();
			this.lblColSeminar = new System.Windows.Forms.Label();
			this.lblColLab = new System.Windows.Forms.Label();
			this.lblColPractize = new System.Windows.Forms.Label();
			this.ColorDlg = new System.Windows.Forms.ColorDialog();
			this.panCalls = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblMinutes4 = new System.Windows.Forms.Label();
			this.txtParaLen = new System.Windows.Forms.TextBox();
			this.lblParaLen = new System.Windows.Forms.Label();
			this.lblMinutes3 = new System.Windows.Forms.Label();
			this.txtBigBreakLen = new System.Windows.Forms.TextBox();
			this.lblBigBreakLen = new System.Windows.Forms.Label();
			this.lblMinutes2 = new System.Windows.Forms.Label();
			this.txtBreakLen = new System.Windows.Forms.TextBox();
			this.lblBreakLen = new System.Windows.Forms.Label();
			this.lblMinutes = new System.Windows.Forms.Label();
			this.txtParaStartMinutes = new System.Windows.Forms.TextBox();
			this.lblHours = new System.Windows.Forms.Label();
			this.txtParaStartHours = new System.Windows.Forms.TextBox();
			this.lblFirstPara = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lblCalls = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblWeekType = new System.Windows.Forms.Label();
			this.panWeekType = new System.Windows.Forms.Panel();
			this.radEven = new System.Windows.Forms.RadioButton();
			this.lblWeekTypeHead = new System.Windows.Forms.Label();
			this.radOdd = new System.Windows.Forms.RadioButton();
			this.lblTray = new System.Windows.Forms.Label();
			this.panTray = new System.Windows.Forms.Panel();
			this.lblTrayHead = new System.Windows.Forms.Label();
			this.grpAutostart = new System.Windows.Forms.GroupBox();
			this.btnSelParaEndSound = new System.Windows.Forms.Button();
			this.chkNotifyOnParaEnd = new System.Windows.Forms.CheckBox();
			this.chkParaEndSnd = new System.Windows.Forms.CheckBox();
			this.btnSelParaStartSound = new System.Windows.Forms.Button();
			this.chkNotifyOnParaStart = new System.Windows.Forms.CheckBox();
			this.chkParaStartSnd = new System.Windows.Forms.CheckBox();
			this.chkAutostart = new System.Windows.Forms.CheckBox();
			this.tlpMain = new System.Windows.Forms.ToolTip(this.components);
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.panDefTbl.SuspendLayout();
			this.panHighlight.SuspendLayout();
			this.panCalls.SuspendLayout();
			this.panWeekType.SuspendLayout();
			this.panTray.SuspendLayout();
			this.grpAutostart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox4)).BeginInit();
			this.SuspendLayout();
			// 
			// panOptionsMenu
			// 
			this.panOptionsMenu.BackColor = System.Drawing.Color.Transparent;
			this.panOptionsMenu.Location = new System.Drawing.Point(0, 0);
			this.panOptionsMenu.Name = "panOptionsMenu";
			this.panOptionsMenu.Size = new System.Drawing.Size(136, 312);
			this.panOptionsMenu.TabIndex = 0;
			// 
			// lblDefTbl
			// 
			this.lblDefTbl.BackColor = System.Drawing.Color.Transparent;
			this.lblDefTbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDefTbl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lblDefTbl.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDefTbl.Location = new System.Drawing.Point(8, 8);
			this.lblDefTbl.Name = "lblDefTbl";
			this.lblDefTbl.Size = new System.Drawing.Size(120, 40);
			this.lblDefTbl.TabIndex = 1;
			this.lblDefTbl.Tag = "0";
			this.lblDefTbl.Text = "Расписание по умолчанию";
			this.lblDefTbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblDefTbl.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
			this.lblDefTbl.Click += new System.EventHandler(this.lblDefTbl_Click);
			this.lblDefTbl.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
			// 
			// panLine
			// 
			this.panLine.BackColor = System.Drawing.Color.Black;
			this.panLine.Location = new System.Drawing.Point(136, 0);
			this.panLine.Name = "panLine";
			this.panLine.Size = new System.Drawing.Size(3, 312);
			this.panLine.TabIndex = 2;
			// 
			// lblHighlight
			// 
			this.lblHighlight.BackColor = System.Drawing.Color.Transparent;
			this.lblHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblHighlight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lblHighlight.Location = new System.Drawing.Point(8, 56);
			this.lblHighlight.Name = "lblHighlight";
			this.lblHighlight.Size = new System.Drawing.Size(120, 40);
			this.lblHighlight.TabIndex = 3;
			this.lblHighlight.Tag = "1";
			this.lblHighlight.Text = "Подцветка";
			this.lblHighlight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblHighlight.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
			this.lblHighlight.Click += new System.EventHandler(this.lblHighlight_Click);
			this.lblHighlight.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
			// 
			// panDefTbl
			// 
			this.panDefTbl.Controls.Add(this.lblDefTblHead);
			this.panDefTbl.Controls.Add(this.lstAvalTbls);
			this.panDefTbl.Controls.Add(this.pictureBox1);
			this.panDefTbl.Location = new System.Drawing.Point(144, 8);
			this.panDefTbl.Name = "panDefTbl";
			this.panDefTbl.Size = new System.Drawing.Size(296, 256);
			this.panDefTbl.TabIndex = 4;
			// 
			// lblDefTblHead
			// 
			this.lblDefTblHead.BackColor = System.Drawing.Color.Transparent;
			this.lblDefTblHead.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblDefTblHead.Location = new System.Drawing.Point(80, 16);
			this.lblDefTblHead.Name = "lblDefTblHead";
			this.lblDefTblHead.Size = new System.Drawing.Size(200, 40);
			this.lblDefTblHead.TabIndex = 1;
			this.lblDefTblHead.Text = "Выберите расписание, котрое будет появляться при запуске программы:";
			this.lblDefTblHead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstAvalTbls
			// 
			this.lstAvalTbls.AllowColumnReorder = true;
			this.lstAvalTbls.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstAvalTbls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnGroup,
            this.columnCourse,
            this.columnTerm,
            this.columnFile});
			this.lstAvalTbls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lstAvalTbls.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lstAvalTbls.FullRowSelect = true;
			this.lstAvalTbls.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstAvalTbls.LabelWrap = false;
			this.lstAvalTbls.Location = new System.Drawing.Point(16, 72);
			this.lstAvalTbls.MultiSelect = false;
			this.lstAvalTbls.Name = "lstAvalTbls";
			this.lstAvalTbls.Size = new System.Drawing.Size(264, 176);
			this.lstAvalTbls.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstAvalTbls.TabIndex = 21;
			this.lstAvalTbls.View = System.Windows.Forms.View.Details;
			this.lstAvalTbls.ItemActivate += new System.EventHandler(this.lstAvalTbls_ItemActivate);
			// 
			// columnGroup
			// 
			this.columnGroup.Text = "Группа";
			this.columnGroup.Width = 53;
			// 
			// columnCourse
			// 
			this.columnCourse.Text = "Курс";
			this.columnCourse.Width = 43;
			// 
			// columnTerm
			// 
			this.columnTerm.Text = "Семестр";
			this.columnTerm.Width = 61;
			// 
			// columnFile
			// 
			this.columnFile.Text = "Файл";
			this.columnFile.Width = 107;
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.SystemColors.Window;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.btnOK.Location = new System.Drawing.Point(328, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 24);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "Применить";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// panHighlight
			// 
			this.panHighlight.BackColor = System.Drawing.Color.Transparent;
			this.panHighlight.Controls.Add(this.pictureBox2);
			this.panHighlight.Controls.Add(this.lblAdditLesson);
			this.panHighlight.Controls.Add(this.lblConsult);
			this.panHighlight.Controls.Add(this.lblLab);
			this.panHighlight.Controls.Add(this.lblSeminar);
			this.panHighlight.Controls.Add(this.lblPractize);
			this.panHighlight.Controls.Add(this.lblColAdditLesson);
			this.panHighlight.Controls.Add(this.chkHighlight);
			this.panHighlight.Controls.Add(this.label1);
			this.panHighlight.Controls.Add(this.lblLection);
			this.panHighlight.Controls.Add(this.lblColConsult);
			this.panHighlight.Controls.Add(this.lblColLection);
			this.panHighlight.Controls.Add(this.lblColSeminar);
			this.panHighlight.Controls.Add(this.lblColLab);
			this.panHighlight.Controls.Add(this.lblColPractize);
			this.panHighlight.Location = new System.Drawing.Point(144, 8);
			this.panHighlight.Name = "panHighlight";
			this.panHighlight.Size = new System.Drawing.Size(296, 256);
			this.panHighlight.TabIndex = 5;
			// 
			// lblAdditLesson
			// 
			this.lblAdditLesson.Enabled = false;
			this.lblAdditLesson.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblAdditLesson.Location = new System.Drawing.Point(92, 216);
			this.lblAdditLesson.Name = "lblAdditLesson";
			this.lblAdditLesson.Size = new System.Drawing.Size(152, 16);
			this.lblAdditLesson.TabIndex = 11;
			this.lblAdditLesson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblConsult
			// 
			this.lblConsult.Enabled = false;
			this.lblConsult.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblConsult.Location = new System.Drawing.Point(92, 192);
			this.lblConsult.Name = "lblConsult";
			this.lblConsult.Size = new System.Drawing.Size(152, 16);
			this.lblConsult.TabIndex = 10;
			this.lblConsult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLab
			// 
			this.lblLab.Enabled = false;
			this.lblLab.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblLab.Location = new System.Drawing.Point(92, 168);
			this.lblLab.Name = "lblLab";
			this.lblLab.Size = new System.Drawing.Size(152, 16);
			this.lblLab.TabIndex = 9;
			this.lblLab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSeminar
			// 
			this.lblSeminar.Enabled = false;
			this.lblSeminar.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblSeminar.Location = new System.Drawing.Point(92, 144);
			this.lblSeminar.Name = "lblSeminar";
			this.lblSeminar.Size = new System.Drawing.Size(152, 16);
			this.lblSeminar.TabIndex = 8;
			this.lblSeminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPractize
			// 
			this.lblPractize.Enabled = false;
			this.lblPractize.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblPractize.Location = new System.Drawing.Point(92, 120);
			this.lblPractize.Name = "lblPractize";
			this.lblPractize.Size = new System.Drawing.Size(152, 16);
			this.lblPractize.TabIndex = 7;
			this.lblPractize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblColAdditLesson
			// 
			this.lblColAdditLesson.BackColor = System.Drawing.SystemColors.GrayText;
			this.lblColAdditLesson.Enabled = false;
			this.lblColAdditLesson.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColAdditLesson.Location = new System.Drawing.Point(52, 216);
			this.lblColAdditLesson.Name = "lblColAdditLesson";
			this.lblColAdditLesson.Size = new System.Drawing.Size(16, 16);
			this.lblColAdditLesson.TabIndex = 6;
			this.lblColAdditLesson.Click += new System.EventHandler(this.lblCol_Click);
			// 
			// chkHighlight
			// 
			this.chkHighlight.Location = new System.Drawing.Point(56, 64);
			this.chkHighlight.Name = "chkHighlight";
			this.chkHighlight.Size = new System.Drawing.Size(184, 24);
			this.chkHighlight.TabIndex = 2;
			this.chkHighlight.Text = "Включить подцветку";
			this.chkHighlight.CheckedChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(96, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(192, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = "Эта возможность позволяет выделять цветом некоторые типы пар. Выберите нужные:";
			// 
			// lblLection
			// 
			this.lblLection.Enabled = false;
			this.lblLection.ForeColor = System.Drawing.SystemColors.GrayText;
			this.lblLection.Location = new System.Drawing.Point(92, 96);
			this.lblLection.Name = "lblLection";
			this.lblLection.Size = new System.Drawing.Size(152, 16);
			this.lblLection.TabIndex = 1;
			this.lblLection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblColConsult
			// 
			this.lblColConsult.BackColor = System.Drawing.SystemColors.GrayText;
			this.lblColConsult.Enabled = false;
			this.lblColConsult.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColConsult.Location = new System.Drawing.Point(52, 192);
			this.lblColConsult.Name = "lblColConsult";
			this.lblColConsult.Size = new System.Drawing.Size(16, 16);
			this.lblColConsult.TabIndex = 5;
			this.lblColConsult.Click += new System.EventHandler(this.lblCol_Click);
			// 
			// lblColLection
			// 
			this.lblColLection.BackColor = System.Drawing.SystemColors.GrayText;
			this.lblColLection.Enabled = false;
			this.lblColLection.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColLection.Location = new System.Drawing.Point(52, 96);
			this.lblColLection.Name = "lblColLection";
			this.lblColLection.Size = new System.Drawing.Size(16, 16);
			this.lblColLection.TabIndex = 0;
			this.lblColLection.Click += new System.EventHandler(this.lblCol_Click);
			// 
			// lblColSeminar
			// 
			this.lblColSeminar.BackColor = System.Drawing.SystemColors.GrayText;
			this.lblColSeminar.Enabled = false;
			this.lblColSeminar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColSeminar.Location = new System.Drawing.Point(52, 144);
			this.lblColSeminar.Name = "lblColSeminar";
			this.lblColSeminar.Size = new System.Drawing.Size(16, 16);
			this.lblColSeminar.TabIndex = 3;
			this.lblColSeminar.Click += new System.EventHandler(this.lblCol_Click);
			// 
			// lblColLab
			// 
			this.lblColLab.BackColor = System.Drawing.SystemColors.GrayText;
			this.lblColLab.Enabled = false;
			this.lblColLab.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColLab.Location = new System.Drawing.Point(52, 168);
			this.lblColLab.Name = "lblColLab";
			this.lblColLab.Size = new System.Drawing.Size(16, 16);
			this.lblColLab.TabIndex = 4;
			this.lblColLab.Click += new System.EventHandler(this.lblCol_Click);
			// 
			// lblColPractize
			// 
			this.lblColPractize.BackColor = System.Drawing.SystemColors.GrayText;
			this.lblColPractize.Enabled = false;
			this.lblColPractize.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColPractize.Location = new System.Drawing.Point(52, 120);
			this.lblColPractize.Name = "lblColPractize";
			this.lblColPractize.Size = new System.Drawing.Size(16, 16);
			this.lblColPractize.TabIndex = 2;
			this.lblColPractize.Click += new System.EventHandler(this.lblCol_Click);
			// 
			// ColorDlg
			// 
			this.ColorDlg.AnyColor = true;
			// 
			// panCalls
			// 
			this.panCalls.Controls.Add(this.panel1);
			this.panCalls.Controls.Add(this.pictureBox3);
			this.panCalls.Controls.Add(this.lblMinutes4);
			this.panCalls.Controls.Add(this.txtParaLen);
			this.panCalls.Controls.Add(this.lblParaLen);
			this.panCalls.Controls.Add(this.lblMinutes3);
			this.panCalls.Controls.Add(this.txtBigBreakLen);
			this.panCalls.Controls.Add(this.lblBigBreakLen);
			this.panCalls.Controls.Add(this.lblMinutes2);
			this.panCalls.Controls.Add(this.txtBreakLen);
			this.panCalls.Controls.Add(this.lblBreakLen);
			this.panCalls.Controls.Add(this.lblMinutes);
			this.panCalls.Controls.Add(this.txtParaStartMinutes);
			this.panCalls.Controls.Add(this.lblHours);
			this.panCalls.Controls.Add(this.txtParaStartHours);
			this.panCalls.Controls.Add(this.lblFirstPara);
			this.panCalls.Controls.Add(this.label8);
			this.panCalls.Location = new System.Drawing.Point(144, 8);
			this.panCalls.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.panCalls.Name = "panCalls";
			this.panCalls.Size = new System.Drawing.Size(296, 256);
			this.panCalls.TabIndex = 22;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(5, 83);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(289, 2);
			this.panel1.TabIndex = 15;
			// 
			// lblMinutes4
			// 
			this.lblMinutes4.Location = new System.Drawing.Point(209, 218);
			this.lblMinutes4.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.lblMinutes4.Name = "lblMinutes4";
			this.lblMinutes4.Size = new System.Drawing.Size(40, 23);
			this.lblMinutes4.TabIndex = 13;
			this.lblMinutes4.Text = "минут";
			this.lblMinutes4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtParaLen
			// 
			this.txtParaLen.Location = new System.Drawing.Point(176, 221);
			this.txtParaLen.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.txtParaLen.Name = "txtParaLen";
			this.txtParaLen.Size = new System.Drawing.Size(33, 20);
			this.txtParaLen.TabIndex = 12;
			// 
			// lblParaLen
			// 
			this.lblParaLen.Location = new System.Drawing.Point(4, 219);
			this.lblParaLen.Name = "lblParaLen";
			this.lblParaLen.Size = new System.Drawing.Size(161, 23);
			this.lblParaLen.TabIndex = 11;
			this.lblParaLen.Text = "Продолжительность пары:";
			this.lblParaLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMinutes3
			// 
			this.lblMinutes3.Location = new System.Drawing.Point(209, 174);
			this.lblMinutes3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.lblMinutes3.Name = "lblMinutes3";
			this.lblMinutes3.Size = new System.Drawing.Size(40, 23);
			this.lblMinutes3.TabIndex = 10;
			this.lblMinutes3.Text = "минут";
			this.lblMinutes3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtBigBreakLen
			// 
			this.txtBigBreakLen.Location = new System.Drawing.Point(176, 177);
			this.txtBigBreakLen.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.txtBigBreakLen.Name = "txtBigBreakLen";
			this.txtBigBreakLen.Size = new System.Drawing.Size(33, 20);
			this.txtBigBreakLen.TabIndex = 9;
			// 
			// lblBigBreakLen
			// 
			this.lblBigBreakLen.Location = new System.Drawing.Point(4, 175);
			this.lblBigBreakLen.Name = "lblBigBreakLen";
			this.lblBigBreakLen.Size = new System.Drawing.Size(161, 23);
			this.lblBigBreakLen.TabIndex = 8;
			this.lblBigBreakLen.Text = "Длинна большой перемены:";
			this.lblBigBreakLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMinutes2
			// 
			this.lblMinutes2.Location = new System.Drawing.Point(209, 132);
			this.lblMinutes2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.lblMinutes2.Name = "lblMinutes2";
			this.lblMinutes2.Size = new System.Drawing.Size(40, 23);
			this.lblMinutes2.TabIndex = 7;
			this.lblMinutes2.Text = "минут";
			this.lblMinutes2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtBreakLen
			// 
			this.txtBreakLen.Location = new System.Drawing.Point(176, 135);
			this.txtBreakLen.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.txtBreakLen.Name = "txtBreakLen";
			this.txtBreakLen.Size = new System.Drawing.Size(33, 20);
			this.txtBreakLen.TabIndex = 6;
			// 
			// lblBreakLen
			// 
			this.lblBreakLen.Location = new System.Drawing.Point(4, 133);
			this.lblBreakLen.Name = "lblBreakLen";
			this.lblBreakLen.Size = new System.Drawing.Size(161, 23);
			this.lblBreakLen.TabIndex = 5;
			this.lblBreakLen.Text = "Длинна обычной перемены:";
			this.lblBreakLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMinutes
			// 
			this.lblMinutes.Location = new System.Drawing.Point(248, 92);
			this.lblMinutes.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.lblMinutes.Name = "lblMinutes";
			this.lblMinutes.Size = new System.Drawing.Size(40, 23);
			this.lblMinutes.TabIndex = 4;
			this.lblMinutes.Text = "минут";
			this.lblMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtParaStartMinutes
			// 
			this.txtParaStartMinutes.Location = new System.Drawing.Point(214, 95);
			this.txtParaStartMinutes.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.txtParaStartMinutes.Name = "txtParaStartMinutes";
			this.txtParaStartMinutes.Size = new System.Drawing.Size(33, 20);
			this.txtParaStartMinutes.TabIndex = 3;
			// 
			// lblHours
			// 
			this.lblHours.Location = new System.Drawing.Point(169, 93);
			this.lblHours.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.lblHours.Name = "lblHours";
			this.lblHours.Size = new System.Drawing.Size(40, 23);
			this.lblHours.TabIndex = 2;
			this.lblHours.Text = "часов";
			this.lblHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtParaStartHours
			// 
			this.txtParaStartHours.Location = new System.Drawing.Point(135, 96);
			this.txtParaStartHours.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.txtParaStartHours.Name = "txtParaStartHours";
			this.txtParaStartHours.Size = new System.Drawing.Size(33, 20);
			this.txtParaStartHours.TabIndex = 1;
			// 
			// lblFirstPara
			// 
			this.lblFirstPara.Location = new System.Drawing.Point(4, 93);
			this.lblFirstPara.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
			this.lblFirstPara.Name = "lblFirstPara";
			this.lblFirstPara.Size = new System.Drawing.Size(129, 23);
			this.lblFirstPara.TabIndex = 0;
			this.lblFirstPara.Text = "Время начала занятий:";
			this.lblFirstPara.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(96, 16);
			this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(184, 41);
			this.label8.TabIndex = 16;
			this.label8.Text = "Расписание звонков генерируется по введенным ниже данным:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCalls
			// 
			this.lblCalls.BackColor = System.Drawing.Color.Transparent;
			this.lblCalls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblCalls.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lblCalls.Location = new System.Drawing.Point(8, 104);
			this.lblCalls.Name = "lblCalls";
			this.lblCalls.Size = new System.Drawing.Size(120, 40);
			this.lblCalls.TabIndex = 23;
			this.lblCalls.Tag = "2";
			this.lblCalls.Text = "Звонки";
			this.lblCalls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblCalls.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
			this.lblCalls.Click += new System.EventHandler(this.lblCalls_Click);
			this.lblCalls.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Location = new System.Drawing.Point(145, 265);
			this.panel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(295, 2);
			this.panel2.TabIndex = 24;
			// 
			// lblWeekType
			// 
			this.lblWeekType.BackColor = System.Drawing.Color.Transparent;
			this.lblWeekType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblWeekType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lblWeekType.Location = new System.Drawing.Point(8, 152);
			this.lblWeekType.Name = "lblWeekType";
			this.lblWeekType.Size = new System.Drawing.Size(120, 40);
			this.lblWeekType.TabIndex = 25;
			this.lblWeekType.Tag = "3";
			this.lblWeekType.Text = "Неделя";
			this.lblWeekType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblWeekType.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
			this.lblWeekType.Click += new System.EventHandler(this.lblWeekType_Click);
			this.lblWeekType.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
			// 
			// panWeekType
			// 
			this.panWeekType.Controls.Add(this.pictureBox5);
			this.panWeekType.Controls.Add(this.radEven);
			this.panWeekType.Controls.Add(this.lblWeekTypeHead);
			this.panWeekType.Controls.Add(this.radOdd);
			this.panWeekType.Location = new System.Drawing.Point(144, 9);
			this.panWeekType.Name = "panWeekType";
			this.panWeekType.Size = new System.Drawing.Size(296, 256);
			this.panWeekType.TabIndex = 26;
			// 
			// radEven
			// 
			this.radEven.Location = new System.Drawing.Point(67, 148);
			this.radEven.Name = "radEven";
			this.radEven.Size = new System.Drawing.Size(212, 24);
			this.radEven.TabIndex = 2;
			this.radEven.Text = "Четная (2-ая) неделя";
			// 
			// lblWeekTypeHead
			// 
			this.lblWeekTypeHead.Location = new System.Drawing.Point(16, 87);
			this.lblWeekTypeHead.Name = "lblWeekTypeHead";
			this.lblWeekTypeHead.Size = new System.Drawing.Size(203, 23);
			this.lblWeekTypeHead.TabIndex = 0;
			this.lblWeekTypeHead.Text = "Выберите текущий тип недели:";
			// 
			// radOdd
			// 
			this.radOdd.Location = new System.Drawing.Point(67, 117);
			this.radOdd.Name = "radOdd";
			this.radOdd.Size = new System.Drawing.Size(212, 24);
			this.radOdd.TabIndex = 1;
			this.radOdd.Text = "Нечетная (1-ая) неделя";
			// 
			// lblTray
			// 
			this.lblTray.BackColor = System.Drawing.Color.Transparent;
			this.lblTray.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTray.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.lblTray.Location = new System.Drawing.Point(8, 200);
			this.lblTray.Name = "lblTray";
			this.lblTray.Size = new System.Drawing.Size(120, 40);
			this.lblTray.TabIndex = 27;
			this.lblTray.Tag = "4";
			this.lblTray.Text = "Фоновый режим";
			this.lblTray.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblTray.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
			this.lblTray.Click += new System.EventHandler(this.lblTray_Click);
			this.lblTray.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
			// 
			// panTray
			// 
			this.panTray.Controls.Add(this.lblTrayHead);
			this.panTray.Controls.Add(this.pictureBox4);
			this.panTray.Controls.Add(this.grpAutostart);
			this.panTray.Controls.Add(this.chkAutostart);
			this.panTray.Location = new System.Drawing.Point(145, 8);
			this.panTray.Name = "panTray";
			this.panTray.Size = new System.Drawing.Size(296, 256);
			this.panTray.TabIndex = 27;
			// 
			// lblTrayHead
			// 
			this.lblTrayHead.Location = new System.Drawing.Point(95, 20);
			this.lblTrayHead.Name = "lblTrayHead";
			this.lblTrayHead.Size = new System.Drawing.Size(183, 43);
			this.lblTrayHead.TabIndex = 4;
			this.lblTrayHead.Text = "В фоновом режиме программа загружается вместе с Windows и создает иконку в Sys Tr" +
				"ay\'е\r\n";
			// 
			// grpAutostart
			// 
			this.grpAutostart.Controls.Add(this.btnSelParaEndSound);
			this.grpAutostart.Controls.Add(this.chkNotifyOnParaEnd);
			this.grpAutostart.Controls.Add(this.chkParaEndSnd);
			this.grpAutostart.Controls.Add(this.btnSelParaStartSound);
			this.grpAutostart.Controls.Add(this.chkNotifyOnParaStart);
			this.grpAutostart.Controls.Add(this.chkParaStartSnd);
			this.grpAutostart.Location = new System.Drawing.Point(3, 88);
			this.grpAutostart.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.grpAutostart.Name = "grpAutostart";
			this.grpAutostart.Size = new System.Drawing.Size(276, 164);
			this.grpAutostart.TabIndex = 2;
			this.grpAutostart.TabStop = false;
			this.grpAutostart.Text = "При работе в фоновом режиме:";
			// 
			// btnSelParaEndSound
			// 
			this.btnSelParaEndSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelParaEndSound.Location = new System.Drawing.Point(11, 136);
			this.btnSelParaEndSound.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.btnSelParaEndSound.Name = "btnSelParaEndSound";
			this.btnSelParaEndSound.Size = new System.Drawing.Size(243, 19);
			this.btnSelParaEndSound.TabIndex = 6;
			this.btnSelParaEndSound.Text = "...";
			this.btnSelParaEndSound.Click += new System.EventHandler(this.btnSelParaSound_Click);
			this.btnSelParaEndSound.TextChanged += new System.EventHandler(this.btnSelParaSound_TextChanged);
			// 
			// chkNotifyOnParaEnd
			// 
			this.chkNotifyOnParaEnd.Location = new System.Drawing.Point(11, 39);
			this.chkNotifyOnParaEnd.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.chkNotifyOnParaEnd.Name = "chkNotifyOnParaEnd";
			this.chkNotifyOnParaEnd.Size = new System.Drawing.Size(170, 24);
			this.chkNotifyOnParaEnd.TabIndex = 1;
			this.chkNotifyOnParaEnd.Text = "Извещать о конце пары";
			// 
			// chkParaEndSnd
			// 
			this.chkParaEndSnd.AutoSize = true;
			this.chkParaEndSnd.Location = new System.Drawing.Point(11, 114);
			this.chkParaEndSnd.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.chkParaEndSnd.Name = "chkParaEndSnd";
			this.chkParaEndSnd.Size = new System.Drawing.Size(210, 17);
			this.chkParaEndSnd.TabIndex = 5;
			this.chkParaEndSnd.Text = "Проигрвать звук по окончанию пары";
			// 
			// btnSelParaStartSound
			// 
			this.btnSelParaStartSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelParaStartSound.Location = new System.Drawing.Point(11, 85);
			this.btnSelParaStartSound.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnSelParaStartSound.Name = "btnSelParaStartSound";
			this.btnSelParaStartSound.Size = new System.Drawing.Size(243, 19);
			this.btnSelParaStartSound.TabIndex = 4;
			this.btnSelParaStartSound.Text = "...";
			this.btnSelParaStartSound.Click += new System.EventHandler(this.btnSelParaSound_Click);
			this.btnSelParaStartSound.TextChanged += new System.EventHandler(this.btnSelParaSound_TextChanged);
			// 
			// chkNotifyOnParaStart
			// 
			this.chkNotifyOnParaStart.Location = new System.Drawing.Point(11, 18);
			this.chkNotifyOnParaStart.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.chkNotifyOnParaStart.Name = "chkNotifyOnParaStart";
			this.chkNotifyOnParaStart.Size = new System.Drawing.Size(224, 24);
			this.chkNotifyOnParaStart.TabIndex = 0;
			this.chkNotifyOnParaStart.Text = "Извещать о начале пары";
			// 
			// chkParaStartSnd
			// 
			this.chkParaStartSnd.AutoSize = true;
			this.chkParaStartSnd.Location = new System.Drawing.Point(11, 63);
			this.chkParaStartSnd.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.chkParaStartSnd.Name = "chkParaStartSnd";
			this.chkParaStartSnd.Size = new System.Drawing.Size(204, 17);
			this.chkParaStartSnd.TabIndex = 3;
			this.chkParaStartSnd.Text = "Проигрывать звук при начале пары";
			// 
			// chkAutostart
			// 
			this.chkAutostart.Location = new System.Drawing.Point(14, 66);
			this.chkAutostart.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
			this.chkAutostart.Name = "chkAutostart";
			this.chkAutostart.Size = new System.Drawing.Size(185, 24);
			this.chkAutostart.TabIndex = 0;
			this.chkAutostart.Text = "Работать в фоновом режиме";
			this.chkAutostart.CheckedChanged += new System.EventHandler(this.chkAutostart_CheckedChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = TimeTable.Properties.Resources.DefTbl;
			this.pictureBox1.Location = new System.Drawing.Point(16, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(56, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.Image = TimeTable.Properties.Resources.Color;
			this.pictureBox2.Location = new System.Drawing.Point(16, 16);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(72, 48);
			this.pictureBox2.TabIndex = 12;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = TimeTable.Properties.Resources.Звонок;
			this.pictureBox3.Location = new System.Drawing.Point(16, 16);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(57, 48);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 14;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox5
			// 
			this.pictureBox5.Image = TimeTable.Properties.Resources.WeekType;
			this.pictureBox5.Location = new System.Drawing.Point(16, 16);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(57, 48);
			this.pictureBox5.TabIndex = 3;
			this.pictureBox5.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.Image = TimeTable.Properties.Resources.TrayIcon_Icon;
			this.pictureBox4.Location = new System.Drawing.Point(16, 16);
			this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(57, 48);
			this.pictureBox4.TabIndex = 3;
			this.pictureBox4.TabStop = false;
			// 
			// frmSettings
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(450, 304);
			this.Controls.Add(this.panCalls);
			this.Controls.Add(this.panDefTbl);
			this.Controls.Add(this.panHighlight);
			this.Controls.Add(this.panLine);
			this.Controls.Add(this.panTray);
			this.Controls.Add(this.panWeekType);
			this.Controls.Add(this.lblTray);
			this.Controls.Add(this.lblWeekType);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.lblCalls);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblHighlight);
			this.Controls.Add(this.lblDefTbl);
			this.Controls.Add(this.panOptionsMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(144, 8);
			this.MaximizeBox = false;
			this.Name = "frmSettings";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmSettings_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSettings_Paint);
			this.panDefTbl.ResumeLayout(false);
			this.panHighlight.ResumeLayout(false);
			this.panCalls.ResumeLayout(false);
			this.panCalls.PerformLayout();
			this.panWeekType.ResumeLayout(false);
			this.panTray.ResumeLayout(false);
			this.grpAutostart.ResumeLayout(false);
			this.grpAutostart.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox4)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		//-------------------------------------------------------------------
		private void frmSettings_Load(object sender, System.EventArgs e) {
			btnOK.Text = string.Format("{0} {1} {2}", frmMain.RTriang, 
				btnOK.Text, frmMain.LTriang);

			for(int i = 0; i < arrlblSubj.Length; i++)
				arrlblSubj[i].Text = Tbl.ParaTypeNames[i];

			frmMain.LoadTblsToListView(ref lstAvalTbls);
			foreach(ListViewItem i in lstAvalTbls.Items) {
				if(i.SubItems[3].Text == Settings.DefaultTable)
					i.BackColor = Color.Pink;
			}

			// подцветка
			LoadHLColors();
			if(Settings.HighlightMode) {
				chkHighlight.Checked = true;
				foreach(Label a in arrlblCol)
					a.Enabled = true;
				foreach(Label b in arrlblSubj) {
					b.Enabled = true;
					b.ForeColor = SystemColors.WindowText;
				}
			}

			// расписание звонков
			txtParaStartHours.Text = Settings.FirstPara.Hours.ToString();
			txtParaStartMinutes.Text = Settings.FirstPara.Minutes.ToString();
			txtBreakLen.Text = Settings.BreakLen.Minutes.ToString();
			txtBigBreakLen.Text = Settings.BigBreakLen.Minutes.ToString();
			txtParaLen.Text = Settings.ParaLen.TotalMinutes.ToString();

			// тип недели
			if(Settings.CurrWeekType == Tbl.WeekType.CHET)
				radEven.Checked = true;
			else
				radOdd.Checked = true;

			// фоновый режим
			chkAutostart.Checked = Settings.Autostart;
			chkNotifyOnParaStart.Checked = Settings.NotifyOnParaStart;
			chkNotifyOnParaEnd.Checked = Settings.NotifyOnParaEnd;
			chkParaStartSnd.Checked = Settings.PlaySndOnParaStart;
			chkParaEndSnd.Checked = Settings.PlaySndOnParaEnd;
			if(File.Exists(Settings.ParaStartSoundPath))
				btnSelParaStartSound.Text = Settings.ParaStartSoundPath;
			if(File.Exists(Settings.ParaStartSoundPath))
				btnSelParaEndSound.Text = Settings.ParaEndSoundPath;


			lblDefTbl_Click(null, null);
		}
		//-------------------------------------------------------------------
		private void frmSettings_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			Rectangle r = new Rectangle(0,0, panOptionsMenu.Width, panOptionsMenu.Height);
			LinearGradientBrush GradBr = new LinearGradientBrush(r, Color.FromArgb(123,162,231), 
				Color.FromArgb(99,117,214), LinearGradientMode.BackwardDiagonal);
			LinearGradientBrush FocusBrush;

			e.Graphics.FillRectangle(GradBr, r);
			if(FocusedBtn != -1) {
				r.X = lblButtons[FocusedBtn].Location.X;
				r.Y = lblButtons[FocusedBtn].Location.Y;
				r.Width = lblButtons[FocusedBtn].Width;
				r.Height = lblButtons[FocusedBtn].Height;
				FocusBrush = new LinearGradientBrush(r, Color.WhiteSmoke, Color.Orange,
					LinearGradientMode.Vertical);
				e.Graphics.FillRectangle(FocusBrush, r);
			}
		}
		//-------------------------------------------------------------------
		/// <summary>Загружает цвета подцветки пар в label'и</summary>
		private void LoadHLColors() {
			for(int i = 0; i < Settings.HighlightColors.Length; i++) {
				arrlblCol[i].BackColor = Settings.HighlightColors[i];
			}
		}


		//===================================================================
		//							Обработчики событий
		//===================================================================
		//-------------------------------------------------------------------
		private void btn_MouseEnter(object sender, System.EventArgs e) {
			Label lbl = (Label) sender;

			FocusedBtn = Convert.ToInt32(lbl.Tag);
			lbl.ForeColor = Color.Brown;
		}
		//-------------------------------------------------------------------
		private void btn_MouseLeave(object sender, System.EventArgs e) {
			Label lbl = (Label) sender;

			lbl.ForeColor = SystemColors.ControlText;
			FocusedBtn = -1;
		}
		//-------------------------------------------------------------------
		private void lblDefTbl_Click(object sender, System.EventArgs e) {
			panDefTbl.BringToFront();
			SelectedLblButton = Convert.ToInt32(lblDefTbl.Tag);
			this.Text = string.Format("{0} --> {1}", WindowCaption, lblDefTbl.Text);
		}
		//-------------------------------------------------------------------
		private void lblHighlight_Click(object sender, System.EventArgs e) {
			panHighlight.BringToFront();
			SelectedLblButton = Convert.ToInt32(lblHighlight.Tag);
			this.Text = string.Format("{0} --> {1}", WindowCaption, lblHighlight.Text);
		}
		//-------------------------------------------------------------------
		private void lblCalls_Click(object sender, EventArgs e) {
			panCalls.BringToFront();
			SelectedLblButton = Convert.ToInt32(lblCalls.Tag);
			this.Text = string.Format("{0} --> {1}", WindowCaption, lblCalls.Text);
		}
		//-------------------------------------------------------------------
		private void lblWeekType_Click(object sender, EventArgs e) {
			panWeekType.BringToFront();
			SelectedLblButton = Convert.ToInt32(lblWeekType.Tag);
			this.Text = string.Format("{0} --> {1}", WindowCaption, lblWeekType.Text);
		}
		//-------------------------------------------------------------------
		private void lblTray_Click(object sender, EventArgs e) {
			panTray.BringToFront();
			SelectedLblButton = Convert.ToInt32(lblTray.Tag);
			this.Text = string.Format("{0} --> {1}", WindowCaption, lblTray.Text);
		}
		//-------------------------------------------------------------------
		private void lstAvalTbls_ItemActivate(object sender, System.EventArgs e) {
			foreach(ListViewItem i in lstAvalTbls.Items)
				i.BackColor = SystemColors.Window;
			lstAvalTbls.SelectedItems[0].BackColor = Color.Pink;
		}
		//-------------------------------------------------------------------
		private void lblCol_Click(object sender, System.EventArgs e) {
			Label lbl = (Label) sender;

			if(ColorDlg.ShowDialog() == DialogResult.OK) {
				lbl.BackColor = ColorDlg.Color;
			}
		}
		//-------------------------------------------------------------------
		private void chkHighlight_CheckedChanged(object sender, System.EventArgs e) {
			if(chkHighlight.Checked) {
				foreach(Label a in arrlblCol)
					a.Enabled = true;
				foreach(Label b in arrlblSubj) {
					b.Enabled = true;
					b.ForeColor = SystemColors.WindowText;
				}
			}
			else {
				foreach(Label a in arrlblCol)
					a.Enabled = false;
				foreach(Label b in arrlblSubj) {
					b.Enabled = false;
					b.ForeColor = SystemColors.GrayText;
				}
			}
		}
		//-------------------------------------------------------------------
		private void btnOK_Click(object sender, System.EventArgs e) {
			char[] CharArr;
			const string ErrMsgTxt = "Ошибка расписания звонков";

			foreach(ListViewItem i in lstAvalTbls.Items) {
				if(i.BackColor == Color.Pink)
					Settings.DefaultTable = i.SubItems[3].Text;
			}

			Settings.HighlightMode = chkHighlight.Checked;
			for(int i = 0; i < Settings.HighlightColors.Length; i++)
				Settings.HighlightColors[i] = arrlblCol[i].BackColor;


			CharArr = txtParaStartHours.Text.ToCharArray();
			foreach(char i in CharArr) {
				if(!char.IsDigit(i)) {
					MessageBox.Show("Неверно записан час начала занятий!",
						ErrMsgTxt, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}

			CharArr = txtParaStartMinutes.Text.ToCharArray();
			foreach(char i in CharArr) {
				if(!char.IsDigit(i)) {
					MessageBox.Show("Неверно записана минута начала занятий!",
						ErrMsgTxt, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			Settings.FirstPara = new TimeSpan(Convert.ToInt32(txtParaStartHours.Text),
				Convert.ToInt32(txtParaStartMinutes.Text), 0);

			CharArr = txtParaLen.Text.ToCharArray();
			foreach(char i in CharArr) {
				if(!char.IsDigit(i)) {
					MessageBox.Show("Неверно записана продолжительность пары!",
						ErrMsgTxt, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			Settings.ParaLen = TimeSpan.FromMinutes(Convert.ToInt32(txtParaLen.Text));

			CharArr = txtBreakLen.Text.ToCharArray();
			foreach(char i in CharArr) {
				if(!char.IsDigit(i)) {
					MessageBox.Show("Неверно записана длинна перемены!",
						ErrMsgTxt, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			Settings.BreakLen = TimeSpan.FromMinutes(Convert.ToInt32(txtBreakLen.Text));

			CharArr = txtBigBreakLen.Text.ToCharArray();
			foreach(char i in CharArr) {
				if(!char.IsDigit(i)) {
					MessageBox.Show("Неверно записана длинна большой перемены!",
						ErrMsgTxt, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			Settings.BigBreakLen = TimeSpan.FromMinutes(Convert.ToInt32(txtBigBreakLen.Text));

			if(!radEven.Checked && !radOdd.Checked) {
				MessageBox.Show("Не указан тип недели!", 
					ErrMsgTxt, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Settings.CurrWeekType = radOdd.Checked ? Tbl.WeekType.NECHET : Tbl.WeekType.CHET;

			Settings.Autostart = chkAutostart.Checked;
			if(chkAutostart.Checked)
				Settings.MakeAppAutostart();
			else
				Settings.CancelAutostart();
			Settings.NotifyOnParaStart = chkNotifyOnParaStart.Checked;
			Settings.NotifyOnParaEnd = chkNotifyOnParaEnd.Checked;
			Settings.PlaySndOnParaStart = chkParaStartSnd.Checked;
			Settings.PlaySndOnParaEnd = chkParaEndSnd.Checked;
			Settings.ParaStartSoundPath = btnSelParaStartSound.Text;
			Settings.ParaEndSoundPath = btnSelParaEndSound.Text;

			Settings.SaveSettings(Settings.CfgFilePath);
			this.Close();
		}
		//-------------------------------------------------------------------
		private void btnSelParaSound_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			Button btn = sender as Button;

			dlg.Filter = "Wave файлы (*.wav)|*.wav|Все файлы (*.*)|*.*";
			if(btn.Text != "...") dlg.InitialDirectory = btn.Text;
			if(dlg.ShowDialog() != DialogResult.OK) return;
			btn.Text = dlg.FileName;
		}
		//-------------------------------------------------------------------
		private void btnSelParaSound_TextChanged(object sender, EventArgs e) {
			Button btn = sender as Button;
			tlpMain.SetToolTip((Control) btn, btn.Text);
		}
		//-------------------------------------------------------------------
		private void chkAutostart_CheckedChanged(object sender, EventArgs e) {
			CheckBox chk = sender as CheckBox;

			grpAutostart.Enabled = chk.Checked;
		}
	}
}
