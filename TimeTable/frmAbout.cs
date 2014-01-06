using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TimeTable {
	public class frmAbout: System.Windows.Forms.Form {
		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.Button btnOK;
		private PictureBox pictureBox1;
		private PictureBox pcbFon;
		private System.ComponentModel.Container components = null;

		public frmAbout() {
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.btnOK = new System.Windows.Forms.Button();
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pcbFon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.pcbFon)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			this.btnOK.Location = new System.Drawing.Point(88, 192);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(152, 24);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtInfo
			// 
			this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInfo.Location = new System.Drawing.Point(17, 64);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtInfo.Size = new System.Drawing.Size(304, 112);
			this.txtInfo.TabIndex = 2;
			this.txtInfo.Text = resources.GetString("txtInfo.Text");
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = TimeTable.Properties.Resources.SimplyLogo;
			this.pictureBox1.Location = new System.Drawing.Point(69, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(192, 40);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// pcbFon
			// 
			this.pcbFon.Image = TimeTable.Properties.Resources.Frame;
			this.pcbFon.Location = new System.Drawing.Point(0, 0);
			this.pcbFon.Name = "pcbFon";
			this.pcbFon.Size = new System.Drawing.Size(24, 24);
			this.pcbFon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pcbFon.TabIndex = 3;
			this.pcbFon.TabStop = false;
			// 
			// frmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(338, 232);
			this.Controls.Add(this.txtInfo);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.pcbFon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "О программе";
			this.Load += new System.EventHandler(this.frmAbout_Load);
			((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.pcbFon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void frmAbout_Load(object sender, System.EventArgs e) {
			pcbFon.Size = this.ClientSize;
			btnOK.Text = string.Format("{0} {1} {2}", frmMain.RTriang, 
				btnOK.Text, frmMain.LTriang);
			Icon = SystemIcons.Information;
		}

	}
}
