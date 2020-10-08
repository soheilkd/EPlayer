using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EPlayer.Extensions;
using EPlayer.Imaging;
using IF.Lastfm.Core.Api;
using static EPlayer.Windows.LastFMSecrets;

namespace EPlayer
{
	public static partial class LastFM
	{
		private static readonly LastfmClient Client = new LastfmClient(ApiKey, ApiSecret, null);
		public static bool IsConnected => Client != null;


		public static async Task<BitmapImage> DownloadAlbumImage(string albumName)
		{
			//TODO: Search can be improved by checking if artistName also matches (needs to be added in arguments)
			var search = Client.Album.SearchAsync(albumName).GetResult();
			var result = search.FirstOrDefault();
			return await DownloadImage(result.Images.FirstOrDefault());
		}
		public static async Task<BitmapImage> DownloadArtistImage(string artistName)
		{
			var search = Client.Artist.SearchAsync(artistName).GetResult();
			var result = search.FirstOrDefault();
			return await DownloadImage(result.MainImage.Largest);
		}

		private static async Task<BitmapImage> DownloadImage(Uri uri)
		{
			if (string.IsNullOrWhiteSpace(uri.AbsoluteUri))
				return default;

			try
			{
				using var client = new WebClient();
				var data = await client.DownloadDataTaskAsync(uri);
				var bitmap = EncodingHelper.GetBitmapImage(data);
				return bitmap;
			}
			catch (Exception)
			{
				return default;
			}
		}
	}
}