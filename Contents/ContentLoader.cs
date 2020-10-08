using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Pages;
using EPlayer.Windows;

namespace EPlayer
{
	//TODO: Refactor
	public static class ContentLoader
	{
		public static void LoadTiles(WrapPanel panel, IEnumerable<Tile> tiles)
		{
			foreach (Tile tile in tiles)
				panel.Children.Add(tile);
		}

		public static Tile GetTile<T>(T obj, Func<string, Task<BitmapSource>> imageLoader, Func<Page> pageLoader)
		{
			var tile = new Tile() { Title = obj.ToString() };
			Task.Run(async delegate
			{
				var image = await imageLoader(obj.ToString());
				tile.Dispatcher.Invoke(() => tile.ImageSource = image);
			});
			tile.Click += (_, __) => MainWindow.RequestPage(pageLoader());
			return tile;
		}

		//TODO: These 2 need to be more descriptive
		public static Tile GetTileForArtist(Artist artist)
		{
			return GetTile(artist,
				ImageController.LoadArtistImage,
				() => new ArtistPage(artist));
		}

		public static Tile GetTileForAlbum(Album album)
		{
			return GetTile(album,
				ImageController.LoadAlbumImage,
				() => new AlbumPage(album));
		}
	}
}