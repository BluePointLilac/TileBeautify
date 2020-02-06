using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenURL {
	internal static class Program {
		[STAThread]
		public static void Main() {
			string fileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
			string filePath = "./" + fileName + ".ini";
			if (File.Exists(filePath)) {
				string urlPath = File.ReadAllText(filePath, Encoding.Default);
				Process.Start("explorer.exe", urlPath);
			}
		}
	}
}