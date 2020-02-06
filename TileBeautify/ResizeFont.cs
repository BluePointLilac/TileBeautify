using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
    public class ResizeFont {
        //调整字体控件大小(高分屏字体问题）
        public static void KeepFontSize(Control parentControl) {
            Graphics g = parentControl.CreateGraphics();
            float scale = g.DpiY / 96;
            foreach (Control ctr in parentControl.Controls) {
                ctr.Font = new Font(ctr.Font.FontFamily, ctr.Font.Size / scale);
            }
        }
    }
}