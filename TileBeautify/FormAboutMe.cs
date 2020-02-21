using System.Diagnostics;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormAboutMe : Form {
        public FormAboutMe() {
            InitializeComponent();
            new ResizeFont().KeepFontSize(this);
            llbGitHub.Click+=(sender,e)=> Process.Start("explorer.exe", FormEditConfig.gitHubUrl);
            llbBiliBili.Click += (sender, e) => Process.Start("explorer.exe", "https://www.bilibili.com/video/av87431625");
        }
    }
}