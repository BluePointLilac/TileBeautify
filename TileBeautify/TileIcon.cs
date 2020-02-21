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
                    Icon icon = Icon.FromHandle(largeIcon[i]);
                    IconList.Add(icon);
                }
                catch (Exception) {
                    break;
                }
            }
        }

        //将所有图标显示在一个容器内
        public void ShowIcon(FlowLayoutPanel flp) {
            Graphics g = flp.CreateGraphics();
            float scale = 96 / g.DpiY;

            for (int i = 0; i < IconList.Count; i++) {
                Image image = IconList[i].ToBitmap();
                Image newImage = PictureZoom.ZoomPic(image, scale);

                var pic = new PictureBox {
                    Parent = flp,
                    Size = new Size(48, 48),
                    SizeMode = PictureBoxSizeMode.CenterImage,
                    Image = newImage,
                    Name = i.ToString(),
                    //设置每个PictureBox间距为4
                    //Margin = new Padding(4),
                };

                pic.Click += (sender, e) => {
                    foreach (PictureBox p in flp.Controls) {
                        p.BackColor = Color.Transparent;
                    }
                    pic.BackColor = selectColor;
                };
            }
        }

        //将指定资源文件中指定序号图标转换为图片
        public Image ToImage(string iconPath, int iconIndex) {
            LoadIcon(iconPath);
            Image image;
            try {
                Icon icon = IconList[iconIndex];
                image = icon.ToBitmap();
            }
            catch (Exception) {
                image = null;
            }
            return image;
        }
    }
}