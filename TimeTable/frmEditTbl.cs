using System;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.IO;

namespace TimeTable {
	public class frmEditTbl : System.Windows.Forms.Form 
	{
		public enum EditMode {
			NEW_TABLE,
			EDIT_TABLE
		}

		// ---------------------------------------------------------------------------------

		public EditMode Mode;

		/// <summary>Здесь храниться редактируемое расписание</summary>
		private Tbl.Para[,,] EditTbl;

		private TextBox[,] txtSubjs;
		private TextBox[,] txtLocats;
		private ComboBox[,] cmbTypes;

        /// <summary>
        /// Label с циферкой в кружке, контекстное меню которого сейчас открыто. 
        /// null - если сейчас нет открытых контекстных меню
        /// </summary>
        private Label ConMnuLabel;

		private const char ModifedChar = '*';

		/// <summary>
		/// Эта строка отображается с остальными типами пар, 
		/// означает отмену задания типа пары. (нужно когда случайно что-то выбрал
		/// и надо опять сделать пустую пару)
		/// </summary>
		private const string NoParaType = "[Пусто]";

		/// <summary>
		/// Изменены ли элементы управляния отображающие расписание на заданный день недели
		/// </summary>
		private bool[] ModifedCtrls;

		/// <summary> Cюда сохраняем расписание</summary>
		private string SaveFile;

		// ---------------------------------------------------------------------------------
		
		#region Controls Declarations
		private System.Windows.Forms.GroupBox groupEven;
		private System.Windows.Forms.Label lbl1Even;
		private System.Windows.Forms.TextBox txtSubj1Even;
		private System.Windows.Forms.TextBox txtLocat1Even;
		private System.Windows.Forms.ComboBox cmbType1Even;
		private System.Windows.Forms.ComboBox cmbType2Even;
		private System.Windows.Forms.TextBox txtLocat2Even;
		private System.Windows.Forms.TextBox txtSubj2Even;
		private System.Windows.Forms.Label lbl2Even;
		private System.Windows.Forms.ComboBox cmbType3Even;
		private System.Windows.Forms.TextBox txtLocat3Even;
		private System.Windows.Forms.TextBox txtSubj3Even;
		private System.Windows.Forms.Label lbl3Even;
		private System.Windows.Forms.ComboBox cmbType4Even;
		private System.Windows.Forms.TextBox txtLocat4Even;
		private System.Windows.Forms.TextBox txtSubj4Even;
		private System.Windows.Forms.Label lbl4Even;
		private System.Windows.Forms.ComboBox cmbType5Even;
		private System.Windows.Forms.TextBox txtLocat5Even;
		private System.Windows.Forms.TextBox txtSubj5Even;
		private System.Windows.Forms.Label lbl5Even;
		private System.Windows.Forms.ComboBox cmbType6Even;
		private System.Windows.Forms.TextBox txtLocat6Even;
		private System.Windows.Forms.TextBox txtSubj6Even;
		private System.Windows.Forms.Label lbl6Even;
		private System.Windows.Forms.Label lblSubjEven;
		private System.Windows.Forms.Label lblLocatEven;
		private System.Windows.Forms.Label lblTypeEven;
		private System.Windows.Forms.GroupBox groupOdd;
		private System.Windows.Forms.Label lblTypeOdd;
		private System.Windows.Forms.Label lblLocatOdd;
		private System.Windows.Forms.Label lblSubjOdd;
		private System.Windows.Forms.ComboBox cmbType6Odd;
		private System.Windows.Forms.TextBox txtLocat6Odd;
		private System.Windows.Forms.Label lbl6Odd;
		private System.Windows.Forms.ComboBox cmbType5Odd;
		private System.Windows.Forms.TextBox txtLocat5Odd;
		private System.Windows.Forms.Label lbl5Odd;
		private System.Windows.Forms.ComboBox cmbType4Odd;
		private System.Windows.Forms.TextBox txtLocat4Odd;
		private System.Windows.Forms.Label lbl4Odd;
		private System.Windows.Forms.ComboBox cmbType3Odd;
		private System.Windows.Forms.TextBox txtLocat3Odd;
		private System.Windows.Forms.Label lbl3Odd;
		private System.Windows.Forms.ComboBox cmbType2Odd;
		private System.Windows.Forms.TextBox txtLocat2Odd;
		private System.Windows.Forms.Label lbl2Odd;
		private System.Windows.Forms.ComboBox cmbType1Odd;
		private System.Windows.Forms.TextBox txtLocat1Odd;
		private System.Windows.Forms.Label lbl1Odd;
		private System.Windows.Forms.Label lblWeekDay;
		private System.Windows.Forms.Label lblLine;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtSubj6Odd;
		private System.Windows.Forms.TextBox txtSubj5Odd;
		private System.Windows.Forms.TextBox txtSubj4Odd;
		private System.Windows.Forms.TextBox txtSubj3Odd;
		private System.Windows.Forms.TextBox txtSubj2Odd;
		private System.Windows.Forms.TextBox txtSubj1Odd;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ToolTip ToolTip;
		private System.Windows.Forms.Button btnCopyToEven;
		private System.Windows.Forms.Button btnCopyToOdd;
		private System.Windows.Forms.ProgressBar prbDayFilled;
		private System.Windows.Forms.Button btnNextDay;
		private System.Windows.Forms.Button btnPrevDay;
        private ContextMenuStrip cmnuParaMenu;
        private ToolStripMenuItem mnuCopy;
        private ToolStripMenuItem mnuDel;
		private System.ComponentModel.IContainer components;
		#endregion

		// ---------------------------------------------------------------------------------

		public frmEditTbl(string FilePath) {
			InitializeComponent();
			if(Mode == EditMode.NEW_TABLE)
				EditTbl = new Tbl.Para[2, Tbl.DAY_IN_WEEK, Tbl.MAX_PARA_COUNT];
			else
				EditTbl = (Tbl.Para[,,]) Tbl.Table.Clone();
			txtSubjs = new TextBox[2, Tbl.MAX_PARA_COUNT];
			txtLocats = new TextBox[2, Tbl.MAX_PARA_COUNT];
			cmbTypes = new ComboBox[2, Tbl.MAX_PARA_COUNT];
			ModifedCtrls = new bool[Tbl.DAY_IN_WEEK];
			SaveFile = frmMain.CurrWorkTblPath;

			#region InitControlArrays
			txtSubjs[0, 0] = txtSubj1Even;
			txtSubjs[0, 1] = txtSubj2Even;
			txtSubjs[0, 2] = txtSubj3Even;
			txtSubjs[0, 3] = txtSubj4Even;
			txtSubjs[0, 4] = txtSubj5Even;
			txtSubjs[0, 5] = txtSubj6Even;

			txtSubjs[1, 0] = txtSubj1Odd;
			txtSubjs[1, 1] = txtSubj2Odd;
			txtSubjs[1, 2] = txtSubj3Odd;
			txtSubjs[1, 3] = txtSubj4Odd;
			txtSubjs[1, 4] = txtSubj5Odd;
			txtSubjs[1, 5] = txtSubj6Odd;

			txtLocats[0, 0] = txtLocat1Even;
			txtLocats[0, 1] = txtLocat2Even;
			txtLocats[0, 2] = txtLocat3Even;
			txtLocats[0, 3] = txtLocat4Even;
			txtLocats[0, 4] = txtLocat5Even;
			txtLocats[0, 5] = txtLocat6Even;

			txtLocats[1, 0] = txtLocat1Odd;
			txtLocats[1, 1] = txtLocat2Odd;
			txtLocats[1, 2] = txtLocat3Odd;
			txtLocats[1, 3] = txtLocat4Odd;
			txtLocats[1, 4] = txtLocat5Odd;
			txtLocats[1, 5] = txtLocat6Odd;

			cmbTypes[0, 0] = cmbType1Even;
			cmbTypes[0, 1] = cmbType2Even;
			cmbTypes[0, 2] = cmbType3Even;
			cmbTypes[0, 3] = cmbType4Even;
			cmbTypes[0, 4] = cmbType5Even;
			cmbTypes[0, 5] = cmbType6Even;

			cmbTypes[1, 0] = cmbType1Odd;
			cmbTypes[1, 1] = cmbType2Odd;
			cmbTypes[1, 2] = cmbType3Odd;
			cmbTypes[1, 3] = cmbType4Odd;
			cmbTypes[1, 4] = cmbType5Odd;
			cmbTypes[1, 5] = cmbType6Odd;
			#endregion
		}

		// ---------------------------------------------------------------------------------

		protected override void Dispose( bool disposing ) {
			if( disposing )
				if(components != null) components.Dispose();
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.groupEven = new System.Windows.Forms.GroupBox();
            this.lblTypeEven = new System.Windows.Forms.Label();
            this.lblLocatEven = new System.Windows.Forms.Label();
            this.lblSubjEven = new System.Windows.Forms.Label();
            this.cmbType6Even = new System.Windows.Forms.ComboBox();
            this.txtLocat6Even = new System.Windows.Forms.TextBox();
            this.txtSubj6Even = new System.Windows.Forms.TextBox();
            this.lbl6Even = new System.Windows.Forms.Label();
            this.cmnuParaMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDel = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbType5Even = new System.Windows.Forms.ComboBox();
            this.txtLocat5Even = new System.Windows.Forms.TextBox();
            this.txtSubj5Even = new System.Windows.Forms.TextBox();
            this.lbl5Even = new System.Windows.Forms.Label();
            this.cmbType4Even = new System.Windows.Forms.ComboBox();
            this.txtLocat4Even = new System.Windows.Forms.TextBox();
            this.txtSubj4Even = new System.Windows.Forms.TextBox();
            this.lbl4Even = new System.Windows.Forms.Label();
            this.cmbType3Even = new System.Windows.Forms.ComboBox();
            this.txtLocat3Even = new System.Windows.Forms.TextBox();
            this.txtSubj3Even = new System.Windows.Forms.TextBox();
            this.lbl3Even = new System.Windows.Forms.Label();
            this.cmbType2Even = new System.Windows.Forms.ComboBox();
            this.txtLocat2Even = new System.Windows.Forms.TextBox();
            this.txtSubj2Even = new System.Windows.Forms.TextBox();
            this.lbl2Even = new System.Windows.Forms.Label();
            this.cmbType1Even = new System.Windows.Forms.ComboBox();
            this.txtLocat1Even = new System.Windows.Forms.TextBox();
            this.txtSubj1Even = new System.Windows.Forms.TextBox();
            this.lbl1Even = new System.Windows.Forms.Label();
            this.groupOdd = new System.Windows.Forms.GroupBox();
            this.lblTypeOdd = new System.Windows.Forms.Label();
            this.lblLocatOdd = new System.Windows.Forms.Label();
            this.lblSubjOdd = new System.Windows.Forms.Label();
            this.cmbType6Odd = new System.Windows.Forms.ComboBox();
            this.txtLocat6Odd = new System.Windows.Forms.TextBox();
            this.txtSubj6Odd = new System.Windows.Forms.TextBox();
            this.lbl6Odd = new System.Windows.Forms.Label();
            this.cmbType5Odd = new System.Windows.Forms.ComboBox();
            this.txtLocat5Odd = new System.Windows.Forms.TextBox();
            this.txtSubj5Odd = new System.Windows.Forms.TextBox();
            this.lbl5Odd = new System.Windows.Forms.Label();
            this.cmbType4Odd = new System.Windows.Forms.ComboBox();
            this.txtLocat4Odd = new System.Windows.Forms.TextBox();
            this.txtSubj4Odd = new System.Windows.Forms.TextBox();
            this.lbl4Odd = new System.Windows.Forms.Label();
            this.cmbType3Odd = new System.Windows.Forms.ComboBox();
            this.txtLocat3Odd = new System.Windows.Forms.TextBox();
            this.txtSubj3Odd = new System.Windows.Forms.TextBox();
            this.lbl3Odd = new System.Windows.Forms.Label();
            this.cmbType2Odd = new System.Windows.Forms.ComboBox();
            this.txtLocat2Odd = new System.Windows.Forms.TextBox();
            this.txtSubj2Odd = new System.Windows.Forms.TextBox();
            this.lbl2Odd = new System.Windows.Forms.Label();
            this.cmbType1Odd = new System.Windows.Forms.ComboBox();
            this.txtLocat1Odd = new System.Windows.Forms.TextBox();
            this.txtSubj1Odd = new System.Windows.Forms.TextBox();
            this.lbl1Odd = new System.Windows.Forms.Label();
            this.lblWeekDay = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnCopyToEven = new System.Windows.Forms.Button();
            this.btnCopyToOdd = new System.Windows.Forms.Button();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.prbDayFilled = new System.Windows.Forms.ProgressBar();
            this.groupEven.SuspendLayout();
            this.cmnuParaMenu.SuspendLayout();
            this.groupOdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupEven
            // 
            this.groupEven.Controls.Add(this.lblTypeEven);
            this.groupEven.Controls.Add(this.lblLocatEven);
            this.groupEven.Controls.Add(this.lblSubjEven);
            this.groupEven.Controls.Add(this.cmbType6Even);
            this.groupEven.Controls.Add(this.txtLocat6Even);
            this.groupEven.Controls.Add(this.txtSubj6Even);
            this.groupEven.Controls.Add(this.lbl6Even);
            this.groupEven.Controls.Add(this.cmbType5Even);
            this.groupEven.Controls.Add(this.txtLocat5Even);
            this.groupEven.Controls.Add(this.txtSubj5Even);
            this.groupEven.Controls.Add(this.lbl5Even);
            this.groupEven.Controls.Add(this.cmbType4Even);
            this.groupEven.Controls.Add(this.txtLocat4Even);
            this.groupEven.Controls.Add(this.txtSubj4Even);
            this.groupEven.Controls.Add(this.lbl4Even);
            this.groupEven.Controls.Add(this.cmbType3Even);
            this.groupEven.Controls.Add(this.txtLocat3Even);
            this.groupEven.Controls.Add(this.txtSubj3Even);
            this.groupEven.Controls.Add(this.lbl3Even);
            this.groupEven.Controls.Add(this.cmbType2Even);
            this.groupEven.Controls.Add(this.txtLocat2Even);
            this.groupEven.Controls.Add(this.txtSubj2Even);
            this.groupEven.Controls.Add(this.lbl2Even);
            this.groupEven.Controls.Add(this.cmbType1Even);
            this.groupEven.Controls.Add(this.txtLocat1Even);
            this.groupEven.Controls.Add(this.txtSubj1Even);
            this.groupEven.Controls.Add(this.lbl1Even);
            this.groupEven.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupEven.Location = new System.Drawing.Point(8, 198);
            this.groupEven.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.groupEven.Name = "groupEven";
            this.groupEven.Size = new System.Drawing.Size(312, 144);
            this.groupEven.TabIndex = 0;
            this.groupEven.TabStop = false;
            this.groupEven.Text = "Четная (2ая) неделя";
            // 
            // lblTypeEven
            // 
            this.lblTypeEven.Location = new System.Drawing.Point(200, 16);
            this.lblTypeEven.Name = "lblTypeEven";
            this.lblTypeEven.Size = new System.Drawing.Size(80, 16);
            this.lblTypeEven.TabIndex = 31;
            this.lblTypeEven.Text = "Тип занятия";
            this.lblTypeEven.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblLocatEven
            // 
            this.lblLocatEven.Location = new System.Drawing.Point(136, 16);
            this.lblLocatEven.Name = "lblLocatEven";
            this.lblLocatEven.Size = new System.Drawing.Size(32, 16);
            this.lblLocatEven.TabIndex = 30;
            this.lblLocatEven.Text = "Где";
            this.lblLocatEven.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblSubjEven
            // 
            this.lblSubjEven.Location = new System.Drawing.Point(32, 16);
            this.lblSubjEven.Name = "lblSubjEven";
            this.lblSubjEven.Size = new System.Drawing.Size(80, 16);
            this.lblSubjEven.TabIndex = 29;
            this.lblSubjEven.Text = "Предмет";
            this.lblSubjEven.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbType6Even
            // 
            this.cmbType6Even.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType6Even.FormattingEnabled = true;
            this.cmbType6Even.Location = new System.Drawing.Point(184, 112);
            this.cmbType6Even.Name = "cmbType6Even";
            this.cmbType6Even.Size = new System.Drawing.Size(120, 21);
            this.cmbType6Even.TabIndex = 28;
            this.cmbType6Even.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat6Even
            // 
            this.txtLocat6Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat6Even.Location = new System.Drawing.Point(128, 112);
            this.txtLocat6Even.Name = "txtLocat6Even";
            this.txtLocat6Even.Size = new System.Drawing.Size(56, 20);
            this.txtLocat6Even.TabIndex = 27;
            // 
            // txtSubj6Even
            // 
            this.txtSubj6Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj6Even.Location = new System.Drawing.Point(24, 112);
            this.txtSubj6Even.Name = "txtSubj6Even";
            this.txtSubj6Even.Size = new System.Drawing.Size(104, 20);
            this.txtSubj6Even.TabIndex = 26;
            this.txtSubj6Even.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl6Even
            // 
            this.lbl6Even.BackColor = System.Drawing.SystemColors.Window;
            this.lbl6Even.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl6Even.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl6Even.Location = new System.Drawing.Point(8, 112);
            this.lbl6Even.Name = "lbl6Even";
            this.lbl6Even.Size = new System.Drawing.Size(16, 20);
            this.lbl6Even.TabIndex = 25;
            this.lbl6Even.Tag = "25";
            this.lbl6Even.Text = "o";
            this.lbl6Even.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl6Even.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmnuParaMenu
            // 
            this.cmnuParaMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy,
            this.mnuDel});
            this.cmnuParaMenu.Name = "cmnuParaMenu";
            this.cmnuParaMenu.Size = new System.Drawing.Size(247, 48);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(246, 22);
            this.mnuCopy.Text = "Копировать на другую неделю";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuDel
            // 
            this.mnuDel.Name = "mnuDel";
            this.mnuDel.Size = new System.Drawing.Size(246, 22);
            this.mnuDel.Text = "Удалить";
            this.mnuDel.Click += new System.EventHandler(this.mnuDel_Click);
            // 
            // cmbType5Even
            // 
            this.cmbType5Even.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType5Even.FormattingEnabled = true;
            this.cmbType5Even.Location = new System.Drawing.Point(184, 96);
            this.cmbType5Even.Name = "cmbType5Even";
            this.cmbType5Even.Size = new System.Drawing.Size(120, 21);
            this.cmbType5Even.TabIndex = 24;
            this.cmbType5Even.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat5Even
            // 
            this.txtLocat5Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat5Even.Location = new System.Drawing.Point(128, 96);
            this.txtLocat5Even.Name = "txtLocat5Even";
            this.txtLocat5Even.Size = new System.Drawing.Size(56, 20);
            this.txtLocat5Even.TabIndex = 23;
            // 
            // txtSubj5Even
            // 
            this.txtSubj5Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj5Even.Location = new System.Drawing.Point(24, 96);
            this.txtSubj5Even.Name = "txtSubj5Even";
            this.txtSubj5Even.Size = new System.Drawing.Size(104, 20);
            this.txtSubj5Even.TabIndex = 22;
            this.txtSubj5Even.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl5Even
            // 
            this.lbl5Even.BackColor = System.Drawing.SystemColors.Window;
            this.lbl5Even.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl5Even.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl5Even.Location = new System.Drawing.Point(8, 96);
            this.lbl5Even.Name = "lbl5Even";
            this.lbl5Even.Size = new System.Drawing.Size(16, 20);
            this.lbl5Even.TabIndex = 21;
            this.lbl5Even.Tag = "24";
            this.lbl5Even.Text = "n";
            this.lbl5Even.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl5Even.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType4Even
            // 
            this.cmbType4Even.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType4Even.FormattingEnabled = true;
            this.cmbType4Even.Location = new System.Drawing.Point(184, 80);
            this.cmbType4Even.Name = "cmbType4Even";
            this.cmbType4Even.Size = new System.Drawing.Size(120, 21);
            this.cmbType4Even.TabIndex = 20;
            this.cmbType4Even.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat4Even
            // 
            this.txtLocat4Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat4Even.Location = new System.Drawing.Point(128, 80);
            this.txtLocat4Even.Name = "txtLocat4Even";
            this.txtLocat4Even.Size = new System.Drawing.Size(56, 20);
            this.txtLocat4Even.TabIndex = 19;
            // 
            // txtSubj4Even
            // 
            this.txtSubj4Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj4Even.Location = new System.Drawing.Point(24, 80);
            this.txtSubj4Even.Name = "txtSubj4Even";
            this.txtSubj4Even.Size = new System.Drawing.Size(104, 20);
            this.txtSubj4Even.TabIndex = 18;
            this.txtSubj4Even.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl4Even
            // 
            this.lbl4Even.BackColor = System.Drawing.SystemColors.Window;
            this.lbl4Even.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl4Even.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl4Even.Location = new System.Drawing.Point(8, 80);
            this.lbl4Even.Name = "lbl4Even";
            this.lbl4Even.Size = new System.Drawing.Size(16, 20);
            this.lbl4Even.TabIndex = 17;
            this.lbl4Even.Tag = "23";
            this.lbl4Even.Text = "m";
            this.lbl4Even.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl4Even.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType3Even
            // 
            this.cmbType3Even.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType3Even.FormattingEnabled = true;
            this.cmbType3Even.Location = new System.Drawing.Point(184, 64);
            this.cmbType3Even.Name = "cmbType3Even";
            this.cmbType3Even.Size = new System.Drawing.Size(120, 21);
            this.cmbType3Even.TabIndex = 16;
            this.cmbType3Even.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat3Even
            // 
            this.txtLocat3Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat3Even.Location = new System.Drawing.Point(128, 64);
            this.txtLocat3Even.Name = "txtLocat3Even";
            this.txtLocat3Even.Size = new System.Drawing.Size(56, 20);
            this.txtLocat3Even.TabIndex = 15;
            // 
            // txtSubj3Even
            // 
            this.txtSubj3Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj3Even.Location = new System.Drawing.Point(24, 64);
            this.txtSubj3Even.Name = "txtSubj3Even";
            this.txtSubj3Even.Size = new System.Drawing.Size(104, 20);
            this.txtSubj3Even.TabIndex = 14;
            this.txtSubj3Even.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl3Even
            // 
            this.lbl3Even.BackColor = System.Drawing.SystemColors.Window;
            this.lbl3Even.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl3Even.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl3Even.Location = new System.Drawing.Point(8, 64);
            this.lbl3Even.Name = "lbl3Even";
            this.lbl3Even.Size = new System.Drawing.Size(16, 20);
            this.lbl3Even.TabIndex = 13;
            this.lbl3Even.Tag = "22";
            this.lbl3Even.Text = "l";
            this.lbl3Even.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl3Even.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType2Even
            // 
            this.cmbType2Even.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType2Even.FormattingEnabled = true;
            this.cmbType2Even.Location = new System.Drawing.Point(184, 48);
            this.cmbType2Even.Name = "cmbType2Even";
            this.cmbType2Even.Size = new System.Drawing.Size(120, 21);
            this.cmbType2Even.TabIndex = 12;
            this.cmbType2Even.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat2Even
            // 
            this.txtLocat2Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat2Even.Location = new System.Drawing.Point(128, 48);
            this.txtLocat2Even.Name = "txtLocat2Even";
            this.txtLocat2Even.Size = new System.Drawing.Size(56, 20);
            this.txtLocat2Even.TabIndex = 11;
            // 
            // txtSubj2Even
            // 
            this.txtSubj2Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj2Even.Location = new System.Drawing.Point(24, 48);
            this.txtSubj2Even.Name = "txtSubj2Even";
            this.txtSubj2Even.Size = new System.Drawing.Size(104, 20);
            this.txtSubj2Even.TabIndex = 10;
            this.txtSubj2Even.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl2Even
            // 
            this.lbl2Even.BackColor = System.Drawing.SystemColors.Window;
            this.lbl2Even.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl2Even.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl2Even.Location = new System.Drawing.Point(8, 48);
            this.lbl2Even.Name = "lbl2Even";
            this.lbl2Even.Size = new System.Drawing.Size(16, 20);
            this.lbl2Even.TabIndex = 9;
            this.lbl2Even.Tag = "21";
            this.lbl2Even.Text = "k";
            this.lbl2Even.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl2Even.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType1Even
            // 
            this.cmbType1Even.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType1Even.FormattingEnabled = true;
            this.cmbType1Even.Location = new System.Drawing.Point(184, 32);
            this.cmbType1Even.Name = "cmbType1Even";
            this.cmbType1Even.Size = new System.Drawing.Size(120, 21);
            this.cmbType1Even.TabIndex = 8;
            this.cmbType1Even.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat1Even
            // 
            this.txtLocat1Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat1Even.Location = new System.Drawing.Point(128, 32);
            this.txtLocat1Even.Name = "txtLocat1Even";
            this.txtLocat1Even.Size = new System.Drawing.Size(56, 20);
            this.txtLocat1Even.TabIndex = 7;
            // 
            // txtSubj1Even
            // 
            this.txtSubj1Even.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj1Even.Location = new System.Drawing.Point(24, 32);
            this.txtSubj1Even.Name = "txtSubj1Even";
            this.txtSubj1Even.Size = new System.Drawing.Size(104, 20);
            this.txtSubj1Even.TabIndex = 6;
            this.txtSubj1Even.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl1Even
            // 
            this.lbl1Even.BackColor = System.Drawing.SystemColors.Window;
            this.lbl1Even.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl1Even.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl1Even.Location = new System.Drawing.Point(8, 32);
            this.lbl1Even.Name = "lbl1Even";
            this.lbl1Even.Size = new System.Drawing.Size(16, 20);
            this.lbl1Even.TabIndex = 0;
            this.lbl1Even.Tag = "20";
            this.lbl1Even.Text = "j";
            this.lbl1Even.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl1Even.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // groupOdd
            // 
            this.groupOdd.Controls.Add(this.lblTypeOdd);
            this.groupOdd.Controls.Add(this.lblLocatOdd);
            this.groupOdd.Controls.Add(this.lblSubjOdd);
            this.groupOdd.Controls.Add(this.cmbType6Odd);
            this.groupOdd.Controls.Add(this.txtLocat6Odd);
            this.groupOdd.Controls.Add(this.txtSubj6Odd);
            this.groupOdd.Controls.Add(this.lbl6Odd);
            this.groupOdd.Controls.Add(this.cmbType5Odd);
            this.groupOdd.Controls.Add(this.txtLocat5Odd);
            this.groupOdd.Controls.Add(this.txtSubj5Odd);
            this.groupOdd.Controls.Add(this.lbl5Odd);
            this.groupOdd.Controls.Add(this.cmbType4Odd);
            this.groupOdd.Controls.Add(this.txtLocat4Odd);
            this.groupOdd.Controls.Add(this.txtSubj4Odd);
            this.groupOdd.Controls.Add(this.lbl4Odd);
            this.groupOdd.Controls.Add(this.cmbType3Odd);
            this.groupOdd.Controls.Add(this.txtLocat3Odd);
            this.groupOdd.Controls.Add(this.txtSubj3Odd);
            this.groupOdd.Controls.Add(this.lbl3Odd);
            this.groupOdd.Controls.Add(this.cmbType2Odd);
            this.groupOdd.Controls.Add(this.txtLocat2Odd);
            this.groupOdd.Controls.Add(this.txtSubj2Odd);
            this.groupOdd.Controls.Add(this.lbl2Odd);
            this.groupOdd.Controls.Add(this.cmbType1Odd);
            this.groupOdd.Controls.Add(this.txtLocat1Odd);
            this.groupOdd.Controls.Add(this.txtSubj1Odd);
            this.groupOdd.Controls.Add(this.lbl1Odd);
            this.groupOdd.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupOdd.Location = new System.Drawing.Point(8, 32);
            this.groupOdd.Name = "groupOdd";
            this.groupOdd.Size = new System.Drawing.Size(312, 144);
            this.groupOdd.TabIndex = 1;
            this.groupOdd.TabStop = false;
            this.groupOdd.Text = "Нечетная (1ая) неделя";
            // 
            // lblTypeOdd
            // 
            this.lblTypeOdd.Location = new System.Drawing.Point(200, 16);
            this.lblTypeOdd.Name = "lblTypeOdd";
            this.lblTypeOdd.Size = new System.Drawing.Size(80, 16);
            this.lblTypeOdd.TabIndex = 31;
            this.lblTypeOdd.Text = "Тип занятия";
            this.lblTypeOdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblLocatOdd
            // 
            this.lblLocatOdd.Location = new System.Drawing.Point(136, 16);
            this.lblLocatOdd.Name = "lblLocatOdd";
            this.lblLocatOdd.Size = new System.Drawing.Size(32, 16);
            this.lblLocatOdd.TabIndex = 30;
            this.lblLocatOdd.Text = "Где";
            this.lblLocatOdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblSubjOdd
            // 
            this.lblSubjOdd.Location = new System.Drawing.Point(32, 16);
            this.lblSubjOdd.Name = "lblSubjOdd";
            this.lblSubjOdd.Size = new System.Drawing.Size(80, 16);
            this.lblSubjOdd.TabIndex = 29;
            this.lblSubjOdd.Text = "Предмет";
            this.lblSubjOdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbType6Odd
            // 
            this.cmbType6Odd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType6Odd.FormattingEnabled = true;
            this.cmbType6Odd.Location = new System.Drawing.Point(184, 112);
            this.cmbType6Odd.Name = "cmbType6Odd";
            this.cmbType6Odd.Size = new System.Drawing.Size(120, 21);
            this.cmbType6Odd.TabIndex = 28;
            this.cmbType6Odd.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat6Odd
            // 
            this.txtLocat6Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat6Odd.Location = new System.Drawing.Point(128, 112);
            this.txtLocat6Odd.Name = "txtLocat6Odd";
            this.txtLocat6Odd.Size = new System.Drawing.Size(56, 20);
            this.txtLocat6Odd.TabIndex = 27;
            // 
            // txtSubj6Odd
            // 
            this.txtSubj6Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj6Odd.Location = new System.Drawing.Point(24, 112);
            this.txtSubj6Odd.Name = "txtSubj6Odd";
            this.txtSubj6Odd.Size = new System.Drawing.Size(104, 20);
            this.txtSubj6Odd.TabIndex = 26;
            this.txtSubj6Odd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl6Odd
            // 
            this.lbl6Odd.BackColor = System.Drawing.SystemColors.Window;
            this.lbl6Odd.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl6Odd.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl6Odd.Location = new System.Drawing.Point(8, 112);
            this.lbl6Odd.Name = "lbl6Odd";
            this.lbl6Odd.Size = new System.Drawing.Size(16, 20);
            this.lbl6Odd.TabIndex = 25;
            this.lbl6Odd.Tag = "15";
            this.lbl6Odd.Text = "o";
            this.lbl6Odd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl6Odd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType5Odd
            // 
            this.cmbType5Odd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType5Odd.FormattingEnabled = true;
            this.cmbType5Odd.Location = new System.Drawing.Point(184, 96);
            this.cmbType5Odd.Name = "cmbType5Odd";
            this.cmbType5Odd.Size = new System.Drawing.Size(120, 21);
            this.cmbType5Odd.TabIndex = 24;
            this.cmbType5Odd.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat5Odd
            // 
            this.txtLocat5Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat5Odd.Location = new System.Drawing.Point(128, 96);
            this.txtLocat5Odd.Name = "txtLocat5Odd";
            this.txtLocat5Odd.Size = new System.Drawing.Size(56, 20);
            this.txtLocat5Odd.TabIndex = 23;
            // 
            // txtSubj5Odd
            // 
            this.txtSubj5Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj5Odd.Location = new System.Drawing.Point(24, 96);
            this.txtSubj5Odd.Name = "txtSubj5Odd";
            this.txtSubj5Odd.Size = new System.Drawing.Size(104, 20);
            this.txtSubj5Odd.TabIndex = 22;
            this.txtSubj5Odd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl5Odd
            // 
            this.lbl5Odd.BackColor = System.Drawing.SystemColors.Window;
            this.lbl5Odd.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl5Odd.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl5Odd.Location = new System.Drawing.Point(8, 96);
            this.lbl5Odd.Name = "lbl5Odd";
            this.lbl5Odd.Size = new System.Drawing.Size(16, 20);
            this.lbl5Odd.TabIndex = 21;
            this.lbl5Odd.Tag = "14";
            this.lbl5Odd.Text = "n";
            this.lbl5Odd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl5Odd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType4Odd
            // 
            this.cmbType4Odd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType4Odd.FormattingEnabled = true;
            this.cmbType4Odd.Location = new System.Drawing.Point(184, 80);
            this.cmbType4Odd.Name = "cmbType4Odd";
            this.cmbType4Odd.Size = new System.Drawing.Size(120, 21);
            this.cmbType4Odd.TabIndex = 20;
            this.cmbType4Odd.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat4Odd
            // 
            this.txtLocat4Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat4Odd.Location = new System.Drawing.Point(128, 80);
            this.txtLocat4Odd.Name = "txtLocat4Odd";
            this.txtLocat4Odd.Size = new System.Drawing.Size(56, 20);
            this.txtLocat4Odd.TabIndex = 19;
            // 
            // txtSubj4Odd
            // 
            this.txtSubj4Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj4Odd.Location = new System.Drawing.Point(24, 80);
            this.txtSubj4Odd.Name = "txtSubj4Odd";
            this.txtSubj4Odd.Size = new System.Drawing.Size(104, 20);
            this.txtSubj4Odd.TabIndex = 18;
            this.txtSubj4Odd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl4Odd
            // 
            this.lbl4Odd.BackColor = System.Drawing.SystemColors.Window;
            this.lbl4Odd.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl4Odd.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl4Odd.Location = new System.Drawing.Point(8, 80);
            this.lbl4Odd.Name = "lbl4Odd";
            this.lbl4Odd.Size = new System.Drawing.Size(16, 20);
            this.lbl4Odd.TabIndex = 17;
            this.lbl4Odd.Tag = "13";
            this.lbl4Odd.Text = "m";
            this.lbl4Odd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl4Odd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType3Odd
            // 
            this.cmbType3Odd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType3Odd.FormattingEnabled = true;
            this.cmbType3Odd.Location = new System.Drawing.Point(184, 64);
            this.cmbType3Odd.Name = "cmbType3Odd";
            this.cmbType3Odd.Size = new System.Drawing.Size(120, 21);
            this.cmbType3Odd.TabIndex = 16;
            this.cmbType3Odd.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat3Odd
            // 
            this.txtLocat3Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat3Odd.Location = new System.Drawing.Point(128, 64);
            this.txtLocat3Odd.Name = "txtLocat3Odd";
            this.txtLocat3Odd.Size = new System.Drawing.Size(56, 20);
            this.txtLocat3Odd.TabIndex = 15;
            // 
            // txtSubj3Odd
            // 
            this.txtSubj3Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj3Odd.Location = new System.Drawing.Point(24, 64);
            this.txtSubj3Odd.Name = "txtSubj3Odd";
            this.txtSubj3Odd.Size = new System.Drawing.Size(104, 20);
            this.txtSubj3Odd.TabIndex = 14;
            this.txtSubj3Odd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl3Odd
            // 
            this.lbl3Odd.BackColor = System.Drawing.SystemColors.Window;
            this.lbl3Odd.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl3Odd.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl3Odd.Location = new System.Drawing.Point(8, 64);
            this.lbl3Odd.Name = "lbl3Odd";
            this.lbl3Odd.Size = new System.Drawing.Size(16, 20);
            this.lbl3Odd.TabIndex = 13;
            this.lbl3Odd.Tag = "12";
            this.lbl3Odd.Text = "l";
            this.lbl3Odd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl3Odd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType2Odd
            // 
            this.cmbType2Odd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType2Odd.FormattingEnabled = true;
            this.cmbType2Odd.Location = new System.Drawing.Point(184, 48);
            this.cmbType2Odd.Name = "cmbType2Odd";
            this.cmbType2Odd.Size = new System.Drawing.Size(120, 21);
            this.cmbType2Odd.TabIndex = 12;
            this.cmbType2Odd.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat2Odd
            // 
            this.txtLocat2Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat2Odd.Location = new System.Drawing.Point(128, 48);
            this.txtLocat2Odd.Name = "txtLocat2Odd";
            this.txtLocat2Odd.Size = new System.Drawing.Size(56, 20);
            this.txtLocat2Odd.TabIndex = 11;
            // 
            // txtSubj2Odd
            // 
            this.txtSubj2Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj2Odd.Location = new System.Drawing.Point(24, 48);
            this.txtSubj2Odd.Name = "txtSubj2Odd";
            this.txtSubj2Odd.Size = new System.Drawing.Size(104, 20);
            this.txtSubj2Odd.TabIndex = 10;
            this.txtSubj2Odd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl2Odd
            // 
            this.lbl2Odd.BackColor = System.Drawing.SystemColors.Window;
            this.lbl2Odd.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl2Odd.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl2Odd.Location = new System.Drawing.Point(8, 48);
            this.lbl2Odd.Name = "lbl2Odd";
            this.lbl2Odd.Size = new System.Drawing.Size(16, 20);
            this.lbl2Odd.TabIndex = 9;
            this.lbl2Odd.Tag = "11";
            this.lbl2Odd.Text = "k";
            this.lbl2Odd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl2Odd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // cmbType1Odd
            // 
            this.cmbType1Odd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType1Odd.FormattingEnabled = true;
            this.cmbType1Odd.Location = new System.Drawing.Point(184, 32);
            this.cmbType1Odd.Name = "cmbType1Odd";
            this.cmbType1Odd.Size = new System.Drawing.Size(120, 21);
            this.cmbType1Odd.TabIndex = 8;
            this.cmbType1Odd.SelectedIndexChanged += new System.EventHandler(this.CmbBox_Modifed);
            // 
            // txtLocat1Odd
            // 
            this.txtLocat1Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocat1Odd.Location = new System.Drawing.Point(128, 32);
            this.txtLocat1Odd.Name = "txtLocat1Odd";
            this.txtLocat1Odd.Size = new System.Drawing.Size(56, 20);
            this.txtLocat1Odd.TabIndex = 7;
            // 
            // txtSubj1Odd
            // 
            this.txtSubj1Odd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubj1Odd.Location = new System.Drawing.Point(24, 32);
            this.txtSubj1Odd.Name = "txtSubj1Odd";
            this.txtSubj1Odd.Size = new System.Drawing.Size(104, 20);
            this.txtSubj1Odd.TabIndex = 6;
            this.txtSubj1Odd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxes_Modifed);
            // 
            // lbl1Odd
            // 
            this.lbl1Odd.BackColor = System.Drawing.SystemColors.Window;
            this.lbl1Odd.ContextMenuStrip = this.cmnuParaMenu;
            this.lbl1Odd.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl1Odd.Location = new System.Drawing.Point(8, 32);
            this.lbl1Odd.Name = "lbl1Odd";
            this.lbl1Odd.Size = new System.Drawing.Size(16, 20);
            this.lbl1Odd.TabIndex = 0;
            this.lbl1Odd.Tag = "10";
            this.lbl1Odd.Text = "j";
            this.lbl1Odd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl1Odd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblsParaNo_MouseDown);
            // 
            // lblWeekDay
            // 
            this.lblWeekDay.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWeekDay.ForeColor = System.Drawing.Color.DarkRed;
            this.lblWeekDay.Location = new System.Drawing.Point(8, 8);
            this.lblWeekDay.Name = "lblWeekDay";
            this.lblWeekDay.Size = new System.Drawing.Size(312, 24);
            this.lblWeekDay.TabIndex = 3;
            this.lblWeekDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLine
            // 
            this.lblLine.BackColor = System.Drawing.SystemColors.WindowText;
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine.Location = new System.Drawing.Point(8, 185);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(248, 4);
            this.lblLine.TabIndex = 4;
            this.lblLine.Text = "label2";
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOK.Location = new System.Drawing.Point(198, 349);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 24);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Сохранить";
            this.ToolTip.SetToolTip(this.btnOK, "Сохранить введенное расписание");
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(198, 381);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.ToolTip.SetToolTip(this.btnCancel, "Отменить создание/изменение расписания. Введенные данные буду потеряны.");
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ToolTip
            // 
            this.ToolTip.AutoPopDelay = 5000;
            this.ToolTip.InitialDelay = 500;
            this.ToolTip.ReshowDelay = 100;
            // 
            // btnCopyToEven
            // 
            this.btnCopyToEven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyToEven.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCopyToEven.Location = new System.Drawing.Point(296, 177);
            this.btnCopyToEven.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnCopyToEven.Name = "btnCopyToEven";
            this.btnCopyToEven.Size = new System.Drawing.Size(24, 23);
            this.btnCopyToEven.TabIndex = 8;
            this.ToolTip.SetToolTip(this.btnCopyToEven, "Скопировать расписание из четной (2ой) недели в нечетную(1ую)");
            this.btnCopyToEven.Click += new System.EventHandler(this.btnCopyToEven_Click);
            // 
            // btnCopyToOdd
            // 
            this.btnCopyToOdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyToOdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCopyToOdd.Location = new System.Drawing.Point(264, 177);
            this.btnCopyToOdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.btnCopyToOdd.Name = "btnCopyToOdd";
            this.btnCopyToOdd.Size = new System.Drawing.Size(24, 23);
            this.btnCopyToOdd.TabIndex = 9;
            this.ToolTip.SetToolTip(this.btnCopyToOdd, "Скопировать расписание из нечетной (1ой) недели в четную(2ую)");
            this.btnCopyToOdd.Click += new System.EventHandler(this.btnCopyToOdd_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextDay.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnNextDay.Location = new System.Drawing.Point(94, 381);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(56, 23);
            this.btnNextDay.TabIndex = 11;
            this.btnNextDay.Text = "f";
            this.ToolTip.SetToolTip(this.btnNextDay, "Перейти к заполнению следующего дня");
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.Enabled = false;
            this.btnPrevDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevDay.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnPrevDay.Location = new System.Drawing.Point(38, 381);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(56, 23);
            this.btnPrevDay.TabIndex = 12;
            this.btnPrevDay.Text = "f";
            this.ToolTip.SetToolTip(this.btnPrevDay, "Перейти к заполнению предыдущего дня");
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // prbDayFilled
            // 
            this.prbDayFilled.Location = new System.Drawing.Point(6, 349);
            this.prbDayFilled.Maximum = 6;
            this.prbDayFilled.Name = "prbDayFilled";
            this.prbDayFilled.Size = new System.Drawing.Size(176, 23);
            this.prbDayFilled.Step = 1;
            this.prbDayFilled.TabIndex = 10;
            // 
            // frmEditTbl
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(330, 411);
            this.ControlBox = false;
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.prbDayFilled);
            this.Controls.Add(this.btnCopyToOdd);
            this.Controls.Add(this.btnCopyToEven);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblWeekDay);
            this.Controls.Add(this.groupOdd);
            this.Controls.Add(this.groupEven);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEditTbl";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор расписания";
            this.Load += new System.EventHandler(this.frmEditTbl_Load);
            this.groupEven.ResumeLayout(false);
            this.groupEven.PerformLayout();
            this.cmnuParaMenu.ResumeLayout(false);
            this.groupOdd.ResumeLayout(false);
            this.groupOdd.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		// ---------------------------------------------------------------------------------

		private void frmEditTbl_Load(object sender, System.EventArgs e) {
			// инициализация элементов управления
			btnOK.Text = string.Format(	"{0} {1} {2}", 
										frmMain.RTriang,
										btnOK.Text,
										frmMain.LTriang);
			btnCancel.Text = string.Format(	"{0}  {1}  {2}", 
											frmMain.RTriang,
											btnCancel.Text,
											frmMain.LTriang);
			btnCopyToEven.Text = frmMain.UpArrow;
			btnCopyToOdd.Text = frmMain.DownArrow;
			btnPrevDay.Text = frmMain.LeftArrow;
			btnNextDay.Text = frmMain.RightArrow;

			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				LoadParaTypeToCmbBox(cmbTypes[0, i]);
				LoadParaTypeToCmbBox(cmbTypes[1, i]);
			}

			lblWeekDay.Text = frmMain.Romb + "   " +
				Tbl.WeekDayNames[prbDayFilled.Value] + "   " + frmMain.Romb;

			switch(Mode) {
				case EditMode.NEW_TABLE:
					this.Text = "Создание нового расписания";
					break;
				case EditMode.EDIT_TABLE:
					this.Text = "Редактирование расписания";
					ShowDayTable(Tbl.Table, 0);
					EditTbl = (Tbl.Para[,,]) Tbl.Table.Clone();
					break;
			}
		}

		// ---------------------------------------------------------------------------------

		private void btnOK_Click(object sender, System.EventArgs e) {
			string MsgTxt = "Вы не заполнили расписание на:\n";
			bool NonModifedDayExist = false;
			int FreeDayCount = 0;

			SaveDayToEditTbl(prbDayFilled.Value);
			if(!ChekTblValid()) return;

			if(Mode == EditMode.NEW_TABLE) {
				for(int i = 0; i < Tbl.DAY_IN_WEEK; i++)
					if(!ModifedCtrls[i]) {
						FreeDayCount++;
						MsgTxt += "    " + Tbl.WeekDayNames[i] + "\n";
						NonModifedDayExist = true;
					}
				if(FreeDayCount == Tbl.DAY_IN_WEEK) {
					MessageBox.Show("Расписание не заполнено!", "!!!", 
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			
			if(NonModifedDayExist) {
				DialogResult res;

				MsgTxt += "Вы действительно хотите оставить эти дни пустыми, т.к. это выходной?";
				res = MessageBox.Show(MsgTxt, "Свободные от занятий дни", 
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
					MessageBoxDefaultButton.Button2);
				if(res == DialogResult.No) return;
			}

			frmEditInfo dlg = new frmEditInfo(true,
				Mode == EditMode.EDIT_TABLE ? true : false);
			dlg.ShowDialog();

			// сохраняем расписание
			if(Mode == EditMode.EDIT_TABLE) {
				Tbl.SaveTblToFile(SaveFile, EditTbl);
			}
			else { // если это новое расписание отображаем диалог сохранения файла
				SaveFileDialog SaveDlg = new SaveFileDialog();
				string BackUpCurrDir = Directory.GetCurrentDirectory();

				SaveDlg.AddExtension	= true;
				SaveDlg.DefaultExt		= "xml";
				SaveDlg.Filter			= "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
				SaveDlg.OverwritePrompt = true;
				SaveDlg.InitialDirectory= Settings.TblsFolder;
				SaveDlg.Title			= "Сохранить расписание";
				SaveDlg.RestoreDirectory= true;
ShowSaveDlg:
				if(SaveDlg.ShowDialog() == DialogResult.OK) {
					string[] StrArr;
					DialogResult res = DialogResult.Yes;

					StrArr = SaveDlg.FileName.Split(@"\".ToCharArray());
					if(StrArr[StrArr.Length - 2] != Settings.TblsFolder) { 
						// если файл сохраняется не в папке с расписаниями - показать предупреждение
						string txt = string.Format(
							"Для того, чтобы расписание отображалось в списке доступных " +
							"расписаний вам надо сохранить его в папке\n{0}.\n" +
							"Вы выбрали другую папку.\n\nВы действительно хотите сохранить "  +
							"расписание туда?", Settings.TblsFolder);

						res = MessageBox.Show(txt, "Подтвердите сохранение",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question,
							MessageBoxDefaultButton.Button2);
					}
					if(res == DialogResult.Yes) {
						File.Copy(Path.GetFullPath(Settings.EmptyTblFile), SaveDlg.FileName, true);
						Tbl.SaveTblToFile(SaveDlg.FileName, EditTbl);
					}
					else {
						goto ShowSaveDlg;
					}
				}
				else {
					return;
				}
			}
			
			this.DialogResult = DialogResult.OK;
			Close();
		}

		// ---------------------------------------------------------------------------------

		private void btnCancel_Click(object sender, System.EventArgs e) {
			DialogResult res;
			string text, caption;

			for(int i = 0; i < ModifedCtrls.Length; i++)
				if(ModifedCtrls[i]) goto Ask;
			btnCancel.DialogResult = DialogResult.Cancel;
			this.Close();
			return;

		Ask:
			if(Mode == EditMode.NEW_TABLE) {
				text = "Вы действительно хотите отменить создание нового расписания?";
				caption = "Отмена создания расписания";
			}
			else {
				text = "Вы действительно хотите отменить изменение расписания?\n(Введенные данные будут потеряны)";
				caption = "Отмена внесения изменений";
			}
			res = MessageBox.Show(text, caption, MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if(res == DialogResult.Yes) {
				DialogResult = DialogResult.Cancel;
				this.Close();
			}
			else return;
		}

		// ---------------------------------------------------------------------------------

		private void btnPrevDay_Click(object sender, System.EventArgs e) {
			if(!FillDayChanging()) return;
			if(!btnNextDay.Enabled) btnNextDay.Enabled = true;
			prbDayFilled.Value--;
			FillDayChanged();
			if(prbDayFilled.Value == 0) {
				btnPrevDay.Enabled = false;
				btnNextDay.Focus();
				return;
			}
		}

		// ---------------------------------------------------------------------------------

		private void btnNextDay_Click(object sender, System.EventArgs e) {
			if(!FillDayChanging()) return;
			if(!btnPrevDay.Enabled) btnPrevDay.Enabled = true;
			prbDayFilled.Value++;
			FillDayChanged();
			if(prbDayFilled.Value == Tbl.DAY_IN_WEEK - 1) {
				btnNextDay.Enabled = false;
				btnPrevDay.Focus();
				return;
			}
		}

		// ---------------------------------------------------------------------------------

		private void btnCopyToOdd_Click(object sender, System.EventArgs e) {
			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				txtSubjs[1, i].Text = txtSubjs[0, i].Text;
				txtLocats[1, i].Text = txtLocats[0, i].Text;
				cmbTypes[1, i].SelectedIndex = cmbTypes[0, i].SelectedIndex;
			}
		}

		// ---------------------------------------------------------------------------------

		private void btnCopyToEven_Click(object sender, System.EventArgs e) {
			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				txtSubjs[0, i].Text = txtSubjs[1, i].Text;
				txtLocats[0, i].Text = txtLocats[1, i].Text;
				cmbTypes[0, i].SelectedIndex = cmbTypes[1, i].SelectedIndex;
			}
		}

		// ---------------------------------------------------------------------------------
		/// <summary>
		/// Обработчик изменения текста/Выбранного значения для всех элементов управляния,
		/// отображающих расписания
		/// </summary>
		private void TextBoxes_Modifed(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			ModifedCtrls[prbDayFilled.Value] = true;
			if(lblWeekDay.Text[lblWeekDay.Text.Length - 1] != ModifedChar)
				lblWeekDay.Text += ModifedChar;
		}

		private void CmbBox_Modifed(object sender, System.EventArgs e) {
			ComboBox cmb = (ComboBox) sender;

			if(cmb.SelectedIndex == -1) return;
			if(cmb.Items[cmb.SelectedIndex].ToString() == NoParaType)
				cmb.SelectedIndex = -1;
		}

		// ==================================================================================

		/// <summary>
		/// Показывает расписание в контролах на день № Day
		/// Расписание береться из 3D массива пар Table
		/// </summary>
		private void ShowDayTable(Tbl.Para[,,] Table,int Day) {
			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				for(int i2 = 0; i2 < 2; i2++) {
					if(Table[i2, Day, i].Name == null) {
						txtSubjs[i2, i].Text = "";
						txtLocats[i2, i].Text = "";
						cmbTypes[i2, i].SelectedIndex = -1;
					}
					else {
						txtSubjs[i2, i].Text = Table[i2, Day, i].Name;
						txtLocats[i2, i].Text = Table[i2, Day, i].Location;
						cmbTypes[i2, i].SelectedIndex = (int) Table[i2, Day, i].Type;
					}
				}
			}
		}

		// ---------------------------------------------------------------------------------
		/// <summary>Сохраняет расписание из контролов в EditTbl[*, Day, *]</summary>
		private void SaveDayToEditTbl(int Day) {
			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				EditTbl[0, Day, i].Name = txtSubjs[0, i].Text;
				EditTbl[1, Day, i].Name = txtSubjs[1, i].Text;
				EditTbl[0, Day, i].Location = txtLocats[0, i].Text;
				EditTbl[1, Day, i].Location = txtLocats[1, i].Text;
				EditTbl[0, Day, i].Type = (Tbl.ParaType) cmbTypes[0, i].SelectedIndex;
				EditTbl[1, Day, i].Type = (Tbl.ParaType) cmbTypes[1, i].SelectedIndex;
			}
		}

		// ---------------------------------------------------------------------------------

		/// <summary>
		/// Загружает список типов занятий в ComboBox
		/// </summary>
		private void LoadParaTypeToCmbBox(ComboBox cmb) {
			cmb.Items.Clear();
			for(int i = 0; i < Tbl.PARA_TYPE_COUNT; i++)
				cmb.Items.Add(Tbl.ParaTypeNames[i]);
			cmb.Items.Add(NoParaType);
		}

		/// <summary>Очищает все контролы, которые показывают расписание</summary>
		private void ClearCtrls() {
			for(int i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				txtSubjs[0, i].Clear();
				txtSubjs[1, i].Clear();
				txtLocats[0, i].Clear();
				txtLocats[1, i].Clear();
				cmbTypes[0, i].SelectedIndex = -1;
				cmbTypes[1, i].SelectedIndex = -1;
			}
		}

		//---------------------------------------------------------------------------------
		/// <summary>Проверяет чтобы расписание в контролах было записано правильно. </summary>
		private bool ChekTblValid() {
			string res;
			int i, i2;

			for(i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
				for(i2 = 0; i2 < 2; i2++) {

					if(txtSubjs[i2, i].Text != "") {
						if(txtLocats[i2, i].Text == "") {
							res = string.Format("Для пары №{0} не указана аудитория", i + 1);
							ShowErrMsg(res);
							return false;
						}
						else if(cmbTypes[i2, i].SelectedIndex == -1) {
							res = string.Format("Для пары №{0} не указан тип занятия", i + 1);
							ShowErrMsg(res);
							return false;
						}
					}

					if(txtLocats[i2, i].Text != "") {
						if(txtSubjs[i2, i].Text == "") {
							res = string.Format("Для пары №{0} - {1} указано название предмета, но не указана аудитория",
								i + 1, txtSubjs[i2, i].Text);
							ShowErrMsg(res);
							return false;
						}
						else if(cmbTypes[i2, i].SelectedIndex == -1) {
							res = string.Format("Для пары {0} - {1} не указан тип занятий",
								i + 1, txtSubjs[i2, i].Text);
							ShowErrMsg(res);
							return false;
						}
					}

					if(cmbTypes[i2, i].SelectedIndex != -1) {
						if(txtSubjs[i2, i].Text == "") {
							res = string.Format("Для пары {0} не указано название предмета",
								i + 1);
							ShowErrMsg(res);
							return false;
						}
						else if(txtLocats[i2, i].Text == "") {
							res = string.Format("Для пары {0} не указана аудитория", i + 1);
							ShowErrMsg(res);
							return false;
						}
					}
				} // for(i2 = 0; i2 < 2; i2++) {
			} // for(i = 0; i < Tbl.MAX_PARA_COUNT; i++) {
			return true;
		}

		// ---------------------------------------------------------------------------------

		private void ShowErrMsg(string Msg) {
			MessageBox.Show(Msg, "Ошибка ввода расписания",
				MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		// ---------------------------------------------------------------------------------
		/// <summary>
		/// Вызывается из обработчиков клика btnNextDay и btnPrevDay,
		/// перед тем, как день измениться. Проверяет правильность записи 
		/// расписания и при ошибке возвращает false
		/// </summary>
		private bool FillDayChanging() {
			if(!ChekTblValid()) return false;
			//если на текущий день расписание изменили/ввели, сохраняем его
			if(ModifedCtrls[prbDayFilled.Value])
				SaveDayToEditTbl(prbDayFilled.Value);
			return true;
		}

		// ---------------------------------------------------------------------------------
		/// <summary>
		/// Вызывается из обработчиков клика btnNextDay и btnPrevDay,
		/// после того, как день уже изменился
		/// </summary>
		private void FillDayChanged() {
			lblWeekDay.Text = frmMain.Romb + "   " +
				Tbl.WeekDayNames[prbDayFilled.Value] + "   " + frmMain.Romb;
			if(ModifedCtrls[prbDayFilled.Value])
				lblWeekDay.Text += ModifedChar;
			/* если на след./прев. день расписание ввели/изменили, показываем сохраненное
			если нет, то показываем либо пустое (в режиме NEW_TABLE) либо текущее сохраненное
			(в режиме EDIT_TABLE)
			*/
			if(ModifedCtrls[prbDayFilled.Value])
				ShowDayTable(EditTbl, prbDayFilled.Value);
			else {
				if(Mode == EditMode.NEW_TABLE)
					ClearCtrls();
				else
					ShowDayTable(Tbl.Table, prbDayFilled.Value);
			}
		}

        // ---------------------------------------------------------------------------------

        /// <summary>Копирует пару на противоположную неделю</summary>
        private void mnuCopy_Click(object sender, EventArgs e) {
            /* sender - Label  с циферкой. У каждого label'а в свойстве Tag
             записано 2значное число. 1ая цифра - неделя (1 или 2), 
             2ая индекс в массивах txtSubs, txtLocals, cmbTypes (начиная с 0)*/

            string strTag = ConMnuLabel.Tag.ToString();
            int     DestWeekT = Convert.ToInt16(strTag[0].ToString()) - 1,
                    SrcWeekT = DestWeekT == 0 ? 1 : 0,
                    ParaIndex = Convert.ToInt16(strTag[1].ToString());

            txtSubjs[DestWeekT, ParaIndex].Text = txtSubjs[SrcWeekT, ParaIndex].Text;
            txtLocats[DestWeekT, ParaIndex].Text = txtLocats[SrcWeekT, ParaIndex].Text;
            cmbTypes[DestWeekT, ParaIndex].SelectedIndex = cmbTypes[SrcWeekT, ParaIndex].SelectedIndex;
        }

        // ---------------------------------------------------------------------------------

        private void mnuDel_Click(object sender, EventArgs e) {
            /* sender - Label  с циферкой. У каждого label'а в свойстве Tag
             записано 2значное число. 1ая цифра - неделя (1 или 2), 
             2ая индекс в массивах txtSubs, txtLocals, cmbTypes (начиная с 0)*/

            string  strTag = ConMnuLabel.Tag.ToString();
            int     WeekT = Convert.ToInt16(strTag[0].ToString()),
                    ParaIndex = Convert.ToInt16(strTag[1].ToString());

            WeekT = WeekT == 2 ? 0 : 1;
            txtSubjs[WeekT, ParaIndex].Text = "";
            txtLocats[WeekT, ParaIndex].Text = "";
            cmbTypes[WeekT, ParaIndex].SelectedIndex = cmbTypes[WeekT, ParaIndex].Items.Count - 1;
        }

        // ---------------------------------------------------------------------------------

        private void lblsParaNo_MouseDown(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Right)
                ConMnuLabel = (Label)sender;
        }

	}
}
