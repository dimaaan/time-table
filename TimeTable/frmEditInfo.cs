using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TimeTable {
	public class frmEditInfo : System.Windows.Forms.Form {

		/// <summary>Если истина - включен режим редактирования доп. инфы</summary>
		public bool Edit;

		/// <summary>Если иcтина - показывает текущую доп. информацию</summary>
		public bool ShowCurrInfo;

		private System.Windows.Forms.Label lblVer;
		private System.Windows.Forms.Label lblGroup;
		private System.Windows.Forms.Label lblCourse;
		private System.Windows.Forms.Label lblTerm;
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.TextBox txtGroup;
		private System.Windows.Forms.TextBox txtCourse;
		private System.Windows.Forms.TextBox txtTerm;
		private System.Windows.Forms.Label lblVerVal;
		private System.Windows.Forms.Button btnOK;
		private PictureBox picInfo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmEditInfo(bool EditMode, bool _ShowCurrInfo) {
			InitializeComponent();
			Edit = EditMode;
			ShowCurrInfo = _ShowCurrInfo;
		}
		protected override void Dispose( bool disposing ) {
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblVer = new System.Windows.Forms.Label();
			this.lblGroup = new System.Windows.Forms.Label();
			this.lblCourse = new System.Windows.Forms.Label();
			this.lblTerm = new System.Windows.Forms.Label();
			this.lblHeader = new System.Windows.Forms.Label();
			this.txtGroup = new System.Windows.Forms.TextBox();
			this.txtCourse = new System.Windows.Forms.TextBox();
			this.txtTerm = new System.Windows.Forms.TextBox();
			this.lblVerVal = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.picInfo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize) (this.picInfo)).BeginInit();
			this.SuspendLayout();
			// 
			// lblVer
			// 
			this.lblVer.Location = new System.Drawing.Point(13, 170);
			this.lblVer.Name = "lblVer";
			this.lblVer.Size = new System.Drawing.Size(144, 16);
			this.lblVer.TabIndex = 0;
			this.lblVer.Text = "Версия формата файла:";
			// 
			// lblGroup
			// 
			this.lblGroup.Location = new System.Drawing.Point(60, 79);
			this.lblGroup.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.lblGroup.Name = "lblGroup";
			this.lblGroup.Size = new System.Drawing.Size(56, 16);
			this.lblGroup.TabIndex = 1;
			this.lblGroup.Text = "Группа:";
			this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblCourse
			// 
			this.lblCourse.Location = new System.Drawing.Point(60, 111);
			this.lblCourse.Name = "lblCourse";
			this.lblCourse.Size = new System.Drawing.Size(56, 16);
			this.lblCourse.TabIndex = 2;
			this.lblCourse.Text = "Курс:";
			this.lblCourse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblTerm
			// 
			this.lblTerm.Location = new System.Drawing.Point(60, 143);
			this.lblTerm.Name = "lblTerm";
			this.lblTerm.Size = new System.Drawing.Size(56, 16);
			this.lblTerm.TabIndex = 3;
			this.lblTerm.Text = "Семестр:";
			this.lblTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblHeader
			// 
			this.lblHeader.Location = new System.Drawing.Point(8, 8);
			this.lblHeader.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new System.Drawing.Size(208, 70);
			this.lblHeader.TabIndex = 4;
			this.lblHeader.Text = "Это дополнительные данные, которые хранятся вместе с расписанием. Для изменения э" +
				"той информации необходимо изменить расписание.";
			// 
			// txtGroup
			// 
			this.txtGroup.Location = new System.Drawing.Point(124, 79);
			this.txtGroup.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.txtGroup.MaxLength = 20;
			this.txtGroup.Name = "txtGroup";
			this.txtGroup.Size = new System.Drawing.Size(100, 20);
			this.txtGroup.TabIndex = 5;
			// 
			// txtCourse
			// 
			this.txtCourse.Location = new System.Drawing.Point(124, 111);
			this.txtCourse.MaxLength = 1;
			this.txtCourse.Name = "txtCourse";
			this.txtCourse.Size = new System.Drawing.Size(100, 20);
			this.txtCourse.TabIndex = 6;
			// 
			// txtTerm
			// 
			this.txtTerm.Location = new System.Drawing.Point(124, 143);
			this.txtTerm.MaxLength = 1;
			this.txtTerm.Name = "txtTerm";
			this.txtTerm.Size = new System.Drawing.Size(100, 20);
			this.txtTerm.TabIndex = 7;
			// 
			// lblVerVal
			// 
			this.lblVerVal.Location = new System.Drawing.Point(165, 170);
			this.lblVerVal.Name = "lblVerVal";
			this.lblVerVal.Size = new System.Drawing.Size(56, 16);
			this.lblVerVal.TabIndex = 8;
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.btnOK.Location = new System.Drawing.Point(47, 194);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(124, 23);
			this.btnOK.TabIndex = 9;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// picInfo
			// 
			this.picInfo.Image = TimeTable.Properties.Resources.Info;
			this.picInfo.Location = new System.Drawing.Point(12, 95);
			this.picInfo.Name = "picInfo";
			this.picInfo.Size = new System.Drawing.Size(48, 48);
			this.picInfo.TabIndex = 11;
			this.picInfo.TabStop = false;
			// 
			// frmEditInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(226, 222);
			this.ControlBox = false;
			this.Controls.Add(this.picInfo);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblVerVal);
			this.Controls.Add(this.txtTerm);
			this.Controls.Add(this.txtCourse);
			this.Controls.Add(this.txtGroup);
			this.Controls.Add(this.lblHeader);
			this.Controls.Add(this.lblTerm);
			this.Controls.Add(this.lblCourse);
			this.Controls.Add(this.lblGroup);
			this.Controls.Add(this.lblVer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEditInfo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Дополнительная информация";
			this.Load += new System.EventHandler(this.frmEditInfo_Load);
			((System.ComponentModel.ISupportInitialize) (this.picInfo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

			}
		#endregion

		private void frmEditInfo_Load(object sender, System.EventArgs e) {
			if(Edit) {
				btnOK.Text = string.Format("{0}  Сохранить  {1}", frmMain.RTriang,
					frmMain.LTriang);
			}
			else {
				btnOK.Text = string.Format("{0}  OK  {1}", frmMain.RTriang,
					frmMain.LTriang);
			}
			if(ShowCurrInfo) {
				txtGroup.Text = Tbl.Info.Group;
				txtCourse.Text = Tbl.Info.Course.ToString();
				txtTerm.Text = Tbl.Info.Term.ToString();
			}
			lblVerVal.Text = Tbl.Info.Ver.ToString();
			if(!Edit)
				txtGroup.ReadOnly = txtCourse.ReadOnly = txtTerm.ReadOnly = true;
			btnOK.Focus();
		}

		private void btnOK_Click(object sender, System.EventArgs e) {
			if(Edit) {
				if(txtGroup.Text == "") {
					MessageBox.Show("Вы не заполнили поле \"Группа\"", "", 
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				else if(txtCourse.Text == "") {
					MessageBox.Show("Вы не заполнили поле \"Курс\"", "",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				else if(txtTerm.Text == "") {
					MessageBox.Show("Вы не заполнили поле\"Семестр\"", "",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				try{
					Tbl.Info.Group = txtGroup.Text;
					Tbl.Info.Course = Convert.ToInt32(txtCourse.Text);
					Tbl.Info.Term = Convert.ToInt32(txtTerm.Text);
				}
				catch(FormatException) {
					MessageBox.Show("Введенные данные не верны!", "", 
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			Close();
		}

	}
}
