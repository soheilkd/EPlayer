using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EPlayer.Controls;
using EPlayer.Extensions;

namespace EPlayer
{
	public static class ImageController
	{
		private static readonly Dictionary<string, BitmapSource> cache = new Dictionary<string, BitmapSource>();
		private static readonly string ImageCacheFolder = App.Path + "Cache\\";

		public static readonly BitmapSource DefaultAlbumImage = SegoeIcon.Album.Render();
		public static readonly BitmapSource DefaultArtistImage = SegoeIcon.Person.Render();

		public static async Task<BitmapSource> LoadArtistImage(string artist) 
		{
			var key = $"artist%{artist}";
			return await LoadImage(key,
				downloader: LastFM.DownloadArtistImage)
				?? DefaultArtistImage;
		}
		public static async Task<BitmapSource> LoadAlbumImage(string album)
		{
			var key = $"album%{album}";
			return await LoadImage(key,
				downloader: LastFM.DownloadAlbumImage)
				?? DefaultAlbumImage;
		}


		private static async Task<BitmapSource> LoadImage(string key, Func<string, Task<BitmapImage>> downloader)
		{
			//First, try loading from cache
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
					//Try downloading image
					image = await Download(key, downloader);
					if (image != default)
						return image;
					else //Failed to get image. Return default
						return default;
				}
			}
		}


		private static BitmapSource LoadFromCache(string key)
		{
			if (cache.ContainsKey(key))
				return cache[key];
			else
				return default;
		}

		private static async Task<BitmapSource> Download(string key, Func<string, Task<BitmapImage>> downloader)
		{
			var image = await downloader(key);
			if (image != default)
			{
				cache.Add(key, image);
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
				var result = data.ToBitmap();
				cache.Add(key, result);
				return result;
			}
			else
				return default;
		}
		private static void SaveToDisk(string key, BitmapImage image)
		{
			var path = ImageCacheFolder + key;
			var data = image.ToData();
			File.WriteAllBytes(path, data);
		}
	}
}
