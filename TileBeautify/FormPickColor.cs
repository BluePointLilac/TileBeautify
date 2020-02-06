using System;
using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormPickColor : Form {
        public Color PickedColor { get; set; }
        public FormPickColor() {
            InitializeComponent();
            btnESC.SendToBack();//隐藏取消按钮
            this.CancelButton = btnESC;
            btnESC.Click += (sender, e) => this.Dispose();

            picScreenShot.Dock = DockStyle.Fill;
            picScreenShot.Image = SnipScreen();
            picScreenShot.MouseClick += PickColorOK;
        }

        private void PickColorOK(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) this.Dispose();
            else if (e.Button == MouseButtons.Left) {
                Bitmap bitmap = (Bitmap)picScreenShot.Image;
                this.PickedColor = bitmap.GetPixel(e.X, e.Y);
                this.Dispose();
            }
        }

        //屏幕截图
        private Bitmap SnipScreen() {
            Bitmap bitmap = null;
            try {
                float fx = PrimaryScreen.ScaleX;
                float fy = PrimaryScreen.ScaleY;
                int sLeft = SystemInformation.VirtualScreen.Left;
                int sTop = SystemInformation.VirtualScreen.Top;
                int sWidth = (int)(SystemInformation.VirtualScreen.Width * fx);
                int sHeight = (int)(SystemInformation.VirtualScreen.Height * fy);

                bitmap = new Bitmap(sWidth, sHeight);
                Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(sLeft, sTop, 0, 0, bitmap.Size);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                this.Dispose();
            }
            return bitmap;
        }
    }
}