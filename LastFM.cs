using System;
using System.Linq;
using System.Net;
using System.Windows.Media.Imaging;
using EPlayer.Extensions;
using IF.Lastfm.Core.Api;
using static EPlayer.Windows.LastFMSecrets;

namespace EPlayer
{
	public static partial class LastFM
	{
		private static readonly LastfmClient Client = new LastfmClient(ApiKey, ApiSecret, null);
		public static bool IsConnected => Client != null;

		public static async void LoadAlbumImage(string albumName, Action<BitmapImage> onSuccess)
		{
			//TODO: Search can be improved by checking if artistName also matches (needs to be added in arguments)
			IF.Lastfm.Core.Api.Helpers.PageResponse<IF.Lastfm.Core.Objects.LastAlbum> search = await Client.Album.SearchAsync(albumName);
			IF.Lastfm.Core.Objects.LastAlbum first = search.FirstOrDefault();
			if (first != null)
			{
				if (TryDownload(first.Images.FirstOrDefault().AbsoluteUri, out var data))
					onSuccess(data.ToBitmap());
			}
		}
		public static async void LoadArtistImage(string artistName, Action<BitmapImage> onSuccess)
		{
			IF.Lastfm.Core.Api.Helpers.PageResponse<IF.Lastfm.Core.Objects.LastArtist> search = await Client.Artist.SearchAsync(artistName);
			IF.Lastfm.Core.Objects.LastArtist first = search.FirstOrDefault();
			if (first != null)
			{
				if (TryDownload(first.MainImage.FirstOrDefault().AbsoluteUri, out var data))
					onSuccess(data.ToBitmap());
			}
		}

		private static bool TryDownload(string url, out byte[] data)
		{
			data = Download(url);
			return data != null;
		}

		private static byte[] Download(string url)
		{
			if (string.IsNullOrWhiteSpace(url))
			{
				return null;
			}

			using var client = new WebClient();
			try { return client.DownloadData(url); }
			catch (Exception) { return null; }
		}
	}
}