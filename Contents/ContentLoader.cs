using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Models;

namespace EPlayer
{
	//TODO: Refactor
	public static class ContentLoader
	{
		private static readonly BitmapImage DefaultArtistImage = SegoeIcon.Person.Render();
		private static readonly BitmapImage DefaultAlbumImage = SegoeIcon.Home.Render();

		public static void LoadTiles(WrapPanel panel, IEnumerable<Tile> tiles)
		{
			foreach (Tile tile in tiles)
				panel.Children.Add(tile);
		}

		//TODO: These 2 need to be more descriptive
		public static Tile GetTileForArtist(Artist artist, Action<Artist> onClick)
		{
			var tile = new Tile()
			{
				Title = artist.Name,
				ImageSource = DefaultArtistImage
			};
			Task.Run(async delegate
			{
				var image = await ImageController.LoadArtistImage(artist.Name);
				tile.Dispatcher.Invoke(() => tile.ImageSource = image);
			});
			tile.Click += (_, __) => onClick(artist);
			return tile;
		}
		public static Tile GetTileForAlbum(Album album, Action<Album> onClick)
		{
			var tile = new Tile()
			{
				Title = album.Name,
				ImageSource = DefaultAlbumImage
			};
			Task.Run(async delegate
			{
				var image = await ImageController.LoadAlbumImage(album.Name);
				tile.Dispatcher.Invoke(() => tile.ImageSource = image);
			});
			tile.Click += (_, __) => onClick(album);
			return tile;
		}
	}
}