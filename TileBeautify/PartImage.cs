using System.Drawing;

namespace TileBeautify {
	//图片区域取图
	public class PartImage {
		public static Bitmap GetPart(Image image, int pX, int pY, int pW, int pH) {
			Bitmap bitmap = new Bitmap(pW, pH);
			Graphics g = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(0, 0, pW, pH);
			Rectangle srcRect = new Rectangle(pX, pY, pW, pH);
			g.DrawImage(image, rectangle, srcRect, GraphicsUnit.Pixel);
			return bitmap;
		}
	}
}