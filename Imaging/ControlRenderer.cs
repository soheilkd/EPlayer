using EPlayer.Controls;
using EPlayer.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EPlayer.Imaging
{
	public static class ControlRenderer
	{
		public static BitmapImage RenderIcon(SegoeIcon icon, Size size = default)
		{
			if (size == default)
				size = new Size(100, 100);
			var button = GetButton(icon, size);
			var frame = GetFrame(button, size);
			var stream = frame.ToStream();
			var image = ImageStreamHelper.GetBitmapImage(stream);
			return image;
		}

		private static IconButton GetButton(SegoeIcon icon, Size size = default)
		{
			var button = new IconButton() { Icon = icon };
			ResizeButton(button, size);
			return button;
		}

		private static void ResizeButton(IconButton button, Size size)
		{
			var rect = new Rect(size);
			button.Measure(size);
			button.Arrange(rect);
		}

		private static BitmapFrame GetFrame(Visual visual, Size size)
		{
			var bmp = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Default);
			bmp.Render(visual);
			var frame = BitmapFrame.Create(bmp);
			return frame;
		}
	}
}
