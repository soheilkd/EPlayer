using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Controls;
using EPlayer.Models;
using EPlayer.Pages;
using EPlayer.Windows;

namespace EPlayer.Views
{
	public partial class AlbumsView : ContentControl
	{
		public AlbumsView()
		{
			InitializeComponent();
		}

		private void MetroTabItem_Loaded(object sender, RoutedEventArgs e)
		{
			var tiles = from album in App.MusicLibrary.Albums
						select GetTileForAlbum(album);
			foreach (var item in tiles)
				MainWrapPanel.Children.Add(item);
		}
		private static Tile GetTileForAlbum(Album album)
		{
			var tile = new Tile();
			tile.Title = album.Name;
			tile.Click += (_, __) => MainWindow.RequestPage(new AlbumPage(album));
			return tile;
		}
	}
}
