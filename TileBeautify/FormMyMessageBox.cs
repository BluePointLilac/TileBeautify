using System.Drawing;
using System.Windows.Forms;

namespace TileBeautify
{
    public partial class FormMyMessageBox : Form
    {
        public FormMyMessageBox(string message)
        {
            InitializeComponent();
            new ResizeFont().KeepFontSize(this);
            lblMessage.Text = message;
            this.Width = lblMessage.Width + 6;
            this.Height = lblMessage.Height + 6;
            Rectangle screenArea = Screen.GetWorkingArea(this);
            int sW = screenArea.Width;
            int sH = screenArea.Height;
            this.Left = (int)((sW - this.Width) * 0.5);
            this.Top = (int)(sH * 0.8);
            int i = 0;
            timer.Start();
            timer.Tick += (a, b) =>
            {
                i++;
                if (i == 1 || i == 3 || i == 5) lblMessage.ForeColor = Color.Red;
                else if (i == 2 || i == 4) lblMessage.ForeColor = Color.White;
                else if (i == 6) this.Dispose();
            };
        }
    }
}