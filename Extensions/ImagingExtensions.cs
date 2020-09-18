using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EPlayer.Controls;

namespace EPlayer.Extensions
{
	//TODO: Not properly documented
	//TODO: Refactor
	public static class ImagingExtension
	{
		public static BitmapImage Render(this SegoeIcon icon)
		{
			IconButton iconButton = icon.GetButton();
			BitmapFrame frame = iconButton.GetFrame();
			return frame.ToStream().ToBitmap();
		}
		public static IconButton GetButton(this SegoeIcon icon, Size size = default)
		{
			var button = new IconButton() { Icon = icon };
			PrepareIconButton(button, size);
			return button;
		}
		private static void PrepareIconButton(IconButton button, Size size)
		{
			if (size == default)
				size = new Size(200, 200);
			button.FontSize = 50;
			var rect = new Rect(size);
			button.Measure(size);
			button.Arrange(rect);
		}
		public static BitmapFrame GetFrame(this Visual visual)
		{
			var bmp = new RenderTargetBitmap(200, 200, 96, 96, PixelFormats.Default);
			bmp.Render(visual);
			return BitmapFrame.Create(bmp);
		}

		public static ImageSource GetImageSource(this byte[] bytes)
		{
			if (bytes.Length == 0)
				return null;
			var converter = new ImageSourceConverter();
			var obj = converter.ConvertFrom(bytes);
			return obj as ImageSource;

			var image = new BitmapImage();
			using var ms = new MemoryStream(bytes);
			image.BeginInit();
			image.CacheOption = BitmapCacheOption.OnLoad;
			image.StreamSource = ms;
			image.EndInit();
			return image;
		}
		public static ImageSource ToImageSource(this System.Drawing.Image image)
		{
			using var ms = new MemoryStream();

			image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			ms.Seek(0, SeekOrigin.Begin);

			var bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
			bitmapImage.StreamSource = ms;
			bitmapImage.EndInit();

			return bitmapImage;
		}
		public static BitmapImage ToBitmap(this byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return default;
			}

			using var memStream = new MemoryStream();
			for (var i = 0; i < data.Length; i++)
			{
				memStream.WriteByte(data[i]);
			}

			return memStream.ToBitmap();
		}
		public static BitmapImage ToBitmap(this Stream stream)
		{
			if (stream == null || stream.Length == 0)
			{
				return null;
			}

			var image = new BitmapImage();
			image.BeginInit();
			image.CacheOption = BitmapCacheOption.OnLoad;
			image.StreamSource = stream;
			image.EndInit();
			return image;
		}
		public static Stream ToStream(this BitmapFrame frame)
		{
			if (frame == null)
			{
				return null;
			}

			var stream = new MemoryStream();
			BitmapEncoder encoder = frame.GetEncoder();
			encoder.Save(stream);
			stream.Flush();
			return stream;
		}
		public static BitmapEncoder GetEncoder(this BitmapFrame frame)
		{
			if (frame == null)
			{
				return null;
			}

			var encoder = new PngBitmapEncoder();
			encoder.Frames.Add(frame);
			return encoder;
		}
		public static byte[] ToData(this BitmapImage bitmapImage)
		{
			if (bitmapImage == null)
			{
				return null;
			}

			using var ms = new MemoryStream();
			BitmapFrame.Create(bitmapImage).GetEncoder().Save(ms);
			return ms.ToArray();
		}
	}
}
