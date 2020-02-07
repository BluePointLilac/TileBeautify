using System;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormReplaceIcon : Form {
        public string IconPath { get; set; }
        public int IconIndex { get; set; }

        public FormReplaceIcon(string iconPath) {
            this.InitializeComponent();
            this.IconPath = null;
            this.IconIndex = -1;
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            if (iconPath != null) {
                txtPath.Text = iconPath;
                TileIcon tileIcon = new TileIcon();
                tileIcon.LoadIcon(iconPath);
                tileIcon.ShowIcon(pnlICO);
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
                pnlICO.Controls.Clear();
                TileIcon tileIcon = new TileIcon();
                tileIcon.LoadIcon(fileName);
                tileIcon.ShowIcon(pnlICO);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            var array = pnlICO.Controls.Find("flp", false);
            if (array.Length != 0) {
                Control control = array[0];
                foreach (PictureBox picParent in control.Controls) {
                    if (picParent.BackColor == TileIcon.selectColor) {
                        this.IconPath = txtPath.Text;
                        foreach (PictureBox pic in picParent.Controls) {
                            this.IconIndex = Convert.ToInt32(pic.Name);
                        }
                        this.Dispose();
                    }
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