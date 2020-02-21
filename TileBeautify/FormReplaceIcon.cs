using System;
using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormReplaceIcon : Form {
        public string IconPath { get; set; }
        public int IconIndex { get; set; }

        public FormReplaceIcon(string iconPath) {
            this.InitializeComponent();
            new ResizeFont().KeepFontSize(this);
            this.IconPath = null;
            this.IconIndex = -1;
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            if (iconPath != null) {
                txtPath.Text = iconPath;
                TileIcon tileIcon = new TileIcon();
                tileIcon.LoadIcon(iconPath);
                tileIcon.ShowIcon(flpIcon);
            }
            btnBrowse.Click += BtnBrowse_Click;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnBrowse_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog {
                Title = "更改图标",
                Filter = "图标文件|*.ico;*.exe;*.dll|程序|*.exe|库|*.dll|图标|*.ico|所有文件|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK) {
                string fileName = ofd.FileName;
                txtPath.Text = fileName;
                flpIcon.Controls.Clear();
                TileIcon tileIcon = new TileIcon();
                tileIcon.LoadIcon(fileName);
                tileIcon.ShowIcon(flpIcon);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            foreach (PictureBox pic in flpIcon.Controls) {
                if (pic.BackColor == TileIcon.selectColor) {
                    this.IconPath = txtPath.Text;
                    this.IconIndex = Convert.ToInt32(pic.Name);
                    this.Dispose();
                }
            }
            if (this.IconIndex == -1) {
                MessageBox.Show("请选择一个图标", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            this.IconPath = null;
            this.IconIndex = -1;
            this.Dispose();
        }
    }
}