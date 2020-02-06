using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
	public partial class FormReplaceIcon : Form {
		public string IconPath { get; set; }

		public int IconIndex { get; set; }

		public FormReplaceIcon(string iconPath) {
			this.InitializeComponent();
			this.IconPath = null;
			this.IconIndex = -1;
			this.CancelButton = this.btnCancel;
			this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
			bool flag = iconPath != null;
			if (flag) {
				this.txtPath.Text = iconPath;
				TileIcon tileIcon = new TileIcon();
				tileIcon.LoadIcon(iconPath);
				tileIcon.ShowIcon(this.pnlICO);
			}
			this.btnBrowse.Click += new EventHandler(this.BtnBrowse_Click);
			this.btnOK.Click += new EventHandler(this.BtnOK_Click);
			this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);
		}

		private void BtnBrowse_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog {
				Title = "更改图标",
				Filter = "图标文件|*.ico;*.exe;*.dll|程序|*.exe|库|*.dll|图标|*.ico|所有文件|*.*"
			};
			bool flag = openFileDialog.ShowDialog() != DialogResult.OK;
			if (!flag) {
				string fileName = openFileDialog.FileName;
				this.txtPath.Text = fileName;
				this.pnlICO.Controls.Clear();
				TileIcon tileIcon = new TileIcon();
				tileIcon.LoadIcon(fileName);
				tileIcon.ShowIcon(this.pnlICO);
			}
		}

		private void BtnOK_Click(object sender, EventArgs e) {
			Control[] array = this.pnlICO.Controls.Find("flp", false);
			bool flag = array.Length != 0;
			if (flag) {
				Control control = array[0];
				foreach (PictureBox pictureBox in control.Controls) {
					bool flag2 = pictureBox.BackColor == TileIcon.selectColor;
					if (flag2) {
						this.IconPath = this.txtPath.Text;
						foreach (PictureBox pictureBox2 in pictureBox.Controls) {
							this.IconIndex = Convert.ToInt32(pictureBox2.Name);
						}
						base.Dispose();
					}
				}
			}
			bool flag3 = this.IconIndex == -1;
			if (flag3) {
				MessageBox.Show("请选择一个图标", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e) {
			this.IconPath = null;
			this.IconIndex = -1;
			base.Dispose();
		}
	}
}