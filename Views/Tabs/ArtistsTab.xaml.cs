using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Controls;
using EPlayer.Models;
using EPlayer.Pages;
using EPlayer.Windows;

namespace EPlayer.Views
{
	public partial class ArtistsView : ContentControl
	{
		public ArtistsView()
		{
			InitializeComponent();
		}

		private void MetroTabItem_Loaded(object sender, RoutedEventArgs e)
		{
			var tiles = from artist in App.MusicLibrary.Artists
						select GetTileForArtist(artist);
			foreach (var item in tiles)
				MainWrapPanel.Children.Add(item);
		}
		private static Tile GetTileForArtist(Artist artist)
		{
			var tile = new Tile();
			tile.Title = artist.Name;
			tile.Click += (_, __) => MainWindow.RequestPage(new ArtistPage(artist));
			return tile;
		}
	}
}
