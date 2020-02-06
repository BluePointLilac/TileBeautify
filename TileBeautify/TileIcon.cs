using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TileBeautify {
	//加载资源图标
	internal class TileIcon {
		private readonly List<Icon> IconList = new List<Icon>();// 记录图标
		private IntPtr[] largeIcon, smallIcon;
		public static readonly Color selectColor = Color.FromArgb(0, 120, 215);//选中图标后的背景颜色
		[DllImport("Shell32.dll")]
		private static extern int ExtractIconEx(string libName, int iconIndex, IntPtr[] largeIcon, IntPtr[] smallIcon, int nIcons);

		//加载资源文件中所有的图标
		public void LoadIcon(string filePath) {
			if (IconList.Count > 0) return;
			largeIcon = new IntPtr[1000];
			smallIcon = new IntPtr[1000];
			ExtractIconEx(filePath, 0, largeIcon, smallIcon, 1000);
			IconList.Clear();
			for (int i = 0; i < largeIcon.Length; i++) {
				try {
					Icon item = Icon.FromHandle(largeIcon[i]);
					IconList.Add(item);
				}
				catch (Exception) {
					break;
				}
			}
		}

		//将所有图标显示在一个容器内
		public void ShowIcon(Panel panel) {
			var flp = new FlowLayoutPanel {
				Parent = panel,
				Dock = DockStyle.Fill,
				AutoScroll = true,
				Name = "flp"
			};
			for (int i = 0; i < IconList.Count; i++) {
				Bitmap image = IconList[i].ToBitmap();
				//本来一个图片框就能完成的事，又被高分屏影响了
				var picParent = new PictureBox {
					Parent = flp,
					Size = new Size(48, 48)
					//Margin = new Padding(4),//设置每个PictureBox间距为4
				};
				var pic = new PictureBox {
					Parent = picParent,
					Location = new Point(8, 8),
					SizeMode = PictureBoxSizeMode.StretchImage,
					Size = new Size(32, 32),
					Image = image,
					Name = i.ToString()
				};
				picParent.Click += delegate (object sender, EventArgs e) {
					foreach (PictureBox pictureBox2 in flp.Controls) {
						pictureBox2.BackColor = Color.Transparent;
					}
					picParent.BackColor = selectColor;
				};
				pic.Click += delegate (object sender, EventArgs e) {
					foreach (PictureBox p in flp.Controls) {
						p.BackColor = Color.Transparent;
					}
					picParent.BackColor = selectColor;
				};
			}
		}

		//将指定资源文件中指定序号图标转换为图片
		public Image ToImage(string iconPath, int iconIndex) {
			LoadIcon(iconPath);
			Image image;
			Image result;
			try {
				Icon icon = IconList[iconIndex];
				image = icon.ToBitmap();
			}
			catch (Exception) {
				result = null;
				return result;
			}
			result = image;
			return result;
		}
	}
}