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
			void onClick(Artist artist) => MainWindow.RequestArtist(artist);
			var tiles =
				from artist
				in App.MusicLibrary.Artists
				select ContentLoader.GetTileForArtist(artist, onClick);
			ContentLoader.LoadTiles(MainWrapPanel, tiles);
		}
	}
}
