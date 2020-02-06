using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TileBeautify {
	//更换鼠标光标，调用API
	internal class ReplaceCursor {
		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursorFromFile(string fileName);
		[DllImport("user32.dll")]
		public static extern IntPtr SetCursor(IntPtr cursorHandle);
		[DllImport("user32.dll")]
		public static extern uint DestroyCursor(IntPtr cursorHandle);

		public static void SetCursor(string curPath, Control control) {
			if (!File.Exists(curPath)) return;
			try {
				Cursor cursor = new Cursor(Cursor.Current.Handle);
				IntPtr intPtr = LoadCursorFromFile(curPath);//加载鼠标光标文件路径
				cursor.GetType().InvokeMember("handle", BindingFlags.Instance 
					| BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetField, null, cursor, new object[]{intPtr});
				control.Cursor = cursor;
			}
			catch (Exception) {}
		}
	}
}