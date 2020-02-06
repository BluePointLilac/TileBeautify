using System;
using System.Runtime.InteropServices;

namespace TileBeautify {
	//获取屏幕缩放比
	//方法来自：https://www.2cto.com/kf/201703/618582.html

	internal class PrimaryScreen {
		private const int HORZRES = 8;
		private const int VERTRES = 10;
		private const int DESKTOPVERTRES = 117;
		private const int DESKTOPHORZRES = 118;
		//获取宽度缩放百分比
		public static float ScaleX {
			get {
				IntPtr dC = GetDC(IntPtr.Zero);
				int deviceCaps = GetDeviceCaps(dC, DESKTOPHORZRES);
				int deviceCaps2 = GetDeviceCaps(dC, HORZRES);
				float result = deviceCaps / (float)deviceCaps2;
				ReleaseDC(IntPtr.Zero, dC);
				return result;
			}
		}
		//获取高度缩放百分比
		public static float ScaleY {
			get {
				IntPtr dC = GetDC(IntPtr.Zero);
				int deviceCaps = GetDeviceCaps(dC, DESKTOPVERTRES);
				int deviceCaps2 = GetDeviceCaps(dC, VERTRES);
				float result = deviceCaps / (float)deviceCaps2;
				ReleaseDC(IntPtr.Zero, dC);
				return result;
			}
		}

		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr ptr);
		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
		[DllImport("user32.dll")]
		private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
	}
}