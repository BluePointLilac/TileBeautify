using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
    class RepaintGroupBox {
        //绘制GroupBox边框颜色
        public static void Grp_Paint(object sender, PaintEventArgs e) {
            var grp = (GroupBox)sender;
            Pen pen = Pens.Black;
            e.Graphics.Clear(grp.BackColor);
            e.Graphics.DrawString(grp.Text, grp.Font, Brushes.Black, 7, 0);
            e.Graphics.DrawLine(pen, 1, 10, 8, 10);
            e.Graphics.DrawLine(pen, e.Graphics.MeasureString(grp.Text, grp.Font).Width + 8, 10, grp.Width - 2, 10);
            e.Graphics.DrawLine(pen, 1, 10, 1, grp.Height - 2);
            e.Graphics.DrawLine(pen, 1, grp.Height - 2, grp.Width - 2, grp.Height - 2);
            e.Graphics.DrawLine(pen, grp.Width - 2, 10, grp.Width - 2, grp.Height - 2);
        }
    }
}