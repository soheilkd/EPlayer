using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Imaging;
using EPlayer.Serialization;

namespace EPlayer
{
	//TODO: Refactor
	public static class ImageController
	{
		private static readonly Dictionary<string, BitmapSource> Cache = new Dictionary<string, BitmapSource>();
		private static readonly string ImageCacheFolder = PathHelper.AppdataPath + "Cache\\";

		public static readonly BitmapSource DefaultAlbumImage = ControlRenderer.RenderIcon(SegoeIcon.Album);
		public static readonly BitmapSource DefaultArtistImage = ControlRenderer.RenderIcon(SegoeIcon.Person);

		private static async Task<BitmapSource> LoadImage(string key, Func<string, Task<BitmapImage>> loader)
		{
			//Try loading from cache
			var image = LoadFromCache(key);
			if (image != default)
				return image;
			else
			{
				//Try loading from disk
				image = await LoadFromDisk(key);
				if (image != default)
					return image;
				else
				{
					//Try image loader
					image = await LoadAndCache(key, loader);
					if (image != default)
						return image;
				}
			}

			//Failed to get image. Return default
			return default;
		}


		private static BitmapSource LoadFromCache(string key)
		{
			if (Cache.ContainsKey(key))
				return Cache[key];
			else
				return default;
		}

		private static async Task<BitmapSource> LoadAndCache(string key, Func<string, Task<BitmapImage>> Loader)
		{
			var image = await Loader(key);
			if (image != default)
			{
				Cache.Add(key, image);
				SaveToDisk(key, image);
			}
			return image;
		}
		private static async Task<BitmapSource> LoadFromDisk(string key)
		{
			var path = ImageCacheFolder + key;
			if (File.Exists(path))
			{
				var data = await File.ReadAllBytesAsync(path);
				var bitmap = EncodingHelper.GetBitmapImage(data);
				Cache.Add(key, bitmap);
				return bitmap;
			}
			else
				return default;
		}
		private static void SaveToDisk(string key, BitmapImage image)
		{
			var path = ImageCacheFolder + key;
			var data = image.ToBytes();
			File.WriteAllBytes(path, data);
		}

		public static async Task<BitmapSource> LoadArtistImage(string artist)
		{
			var key = $"artist%{artist}";
			return await LoadImage(key,
				loader: LastFM.DownloadArtistImage)
				?? DefaultArtistImage;
		}
		public static async Task<BitmapSource> LoadAlbumImage(string album)
		{
			var key = $"album%{album}";
			var image = await LoadImage(key, LastFM.DownloadAlbumImage);
            return image != default ? image : DefaultAlbumImage;
        }
	}
}
