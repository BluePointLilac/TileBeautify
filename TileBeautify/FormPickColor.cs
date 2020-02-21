using System;
using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormPickColor : Form {
        public Color PickedColor { get; set; }
        public FormPickColor() {
            InitializeComponent();
            this.KeyDown += (sender, e) => { 
                //按ESC键取消
                if(e.KeyCode== Keys.Escape) this.Dispose();
            };

            picScreenShot.Dock = DockStyle.Fill;
            picScreenShot.Image = SnipScreen();
            picScreenShot.MouseClick += PickColorOK;
        }

        private void PickColorOK(object sender, MouseEventArgs e) {
            //按左键取色,按右键取消
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
                int sLeft = SystemInformation.VirtualScreen.Left;
                int sTop = SystemInformation.VirtualScreen.Top;
                int sWidth = (int)(SystemInformation.VirtualScreen.Width * PrimaryScreen.ScaleX);
                int sHeight = (int)(SystemInformation.VirtualScreen.Height * PrimaryScreen.ScaleY);

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