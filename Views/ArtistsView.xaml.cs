using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Models;
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
			var tiles = App.MusicLibrary.Artists.Select(artist => ContentLoader.GetTileForArtist(artist));
			ContentLoader.LoadTiles(MainWrapPanel, tiles);
		}
	}
}
