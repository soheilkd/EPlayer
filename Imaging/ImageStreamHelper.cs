using System.IO;
using System.Windows.Media.Imaging;

namespace EPlayer.Imaging
{
    public static class ImageStreamHelper
	{
		public static BitmapImage GetBitmapImage(Stream stream)
		{
			if (stream == null || stream.Length == 0)
				return null;

			var image = new BitmapImage();
			image.BeginInit();
			image.CacheOption = BitmapCacheOption.OnLoad;
			image.StreamSource = stream;
			image.EndInit();
			return image;
		}

	}
}
