using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TileBeautify {
	internal static class Program {
		[STAThread]
		private static void Main() {
			//解决高分屏缩放模糊问题
			if (Environment.OSVersion.Version.Major >= 6) {
				SetProcessDPIAware();
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormEditPicture());
		}

		[DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();
	}
}