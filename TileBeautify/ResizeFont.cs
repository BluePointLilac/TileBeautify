using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify {
    public class ResizeFont {

        private readonly List<Control> allContorlList = new List<Control>();

        //调整字体控件大小(高分屏字体问题）
        public void KeepFontSize(Control parentControl) {
            GetControls(parentControl);

            Graphics g = parentControl.CreateGraphics();
            float scale =96 / g.DpiY;

            foreach (Control ctr in allContorlList) {
                ctr.Font = new Font(ctr.Font.FontFamily, ctr.Font.Size * scale);
                if (ctr.GetType() == typeof(Button)) {
                    var btn = (Button)ctr;
                    btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                }
            }
        }

        //获取所有子控件以及孙控件
        private void GetControls(Control parentControl) {
            foreach (Control ctr in parentControl.Controls) {
                allContorlList.Add(ctr);
                if (ctr.HasChildren) GetControls(ctr);
            }
        }
    }
}