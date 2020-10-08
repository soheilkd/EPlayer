using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EPlayer.Imaging
{
    public static class EncodingHelper
    {
		public static BitmapImage GetBitmapImage(byte[] data)
		{
			//Test
			if (data == null || data.Length == 0)
				return null;
			var converter = new ImageSourceConverter();
			var obj = converter.ConvertFrom(data);
			return obj as BitmapImage;

			if (data == null || data.Length == 0)
				return default;

			using var memStream = new MemoryStream();
			for (var i = 0; i < data.Length; i++)
				memStream.WriteByte(data[i]);

			return ImageStreamHelper.GetBitmapImage(memStream);
		}

		public static ImageSource GetImageSource(byte[] bytes)
		{
			if (bytes.Length == 0)
				return null;
			var converter = new ImageSourceConverter();
			var obj = converter.ConvertFrom(bytes);
			return obj as ImageSource;
		}

		public static BitmapEncoder GetPngEncoder(BitmapFrame frame)
		{
			if (frame == null)
				return null;

			var encoder = new PngBitmapEncoder();
			encoder.Frames.Add(frame);
			return encoder;
		}
	}
}
