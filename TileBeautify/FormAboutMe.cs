using System.Diagnostics;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormAboutMe : Form {
        public FormAboutMe() {
            InitializeComponent();
            llbGitHub.Click+=(sender,e)=> Process.Start("explorer.exe", "https://www.gitgub.com");
            llbBiliBili.Click += (sender, e) => Process.Start("explorer.exe", "https://space.bilibili.com/34492771");
        }
    }
}