using Microsoft.Win32;
using System;
using System.Drawing;

namespace TileBeautify {
	//获取系统主题色
	internal class ThemeColor {
		public static Color GetThemeColor() {
			RegistryKey currentUser = Registry.CurrentUser;
			RegistryKey registryKey = currentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Accent", true);
			object value = registryKey.GetValue("AccentColorMenu");
			int value2 = Convert.ToInt32(value);
			string text = Convert.ToString(value2, 16);
			string str = text.Substring(6, 2);
			string str2 = text.Substring(4, 2);
			string str3 = text.Substring(2, 2);
			text = "#" + str + str2 + str3;
			return ColorTranslator.FromHtml(text);
		}
	}
}