using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EPlayer.Imaging;

namespace EPlayer.Extensions
{
	//TODO: Not properly documented
	//TODO: Refactor
	public static class ImagingExtension
	{
		public static byte[] ToBytes(this BitmapImage bitmapImage)
		{
			if (bitmapImage == null)
				return null;

			using var ms = new MemoryStream();
			var frame = BitmapFrame.Create(bitmapImage);
			var encoder = EncodingHelper.GetPngEncoder(frame);
			encoder.Save(ms);
			return ms.ToArray();
		}

		public static Stream ToStream(this BitmapFrame frame)
		{
			if (frame == null)
				return null;

			var stream = new MemoryStream();
			var encoder = EncodingHelper.GetPngEncoder(frame);
			encoder.Save(stream);
			stream.Flush();
			return stream;
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
	}
}
