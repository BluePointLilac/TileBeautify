using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TileBeautify {
	//图片按比例放缩
	internal class PictureZoom {
		public static Image ZoomPic(Image image, double scale) {
			int ow = image.Width;
			int oh = image.Height;
			int nw = Convert.ToInt32(ow * scale);
			int nh = Convert.ToInt32(oh * scale);
			Bitmap bitmap = new Bitmap(nw, nh);
			Graphics g = Graphics.FromImage(bitmap);
			g.Clear(Color.Transparent);
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.DrawImage(image, new Rectangle(0, 0, nw, nh), 0, 0, ow, oh, GraphicsUnit.Pixel);
			g.Dispose();
			return bitmap;
		}
	}
}