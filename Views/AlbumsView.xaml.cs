using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
			var tiles = App.MusicLibrary.Albums.Select(album => ContentLoader.GetTileForAlbum(album));
			ContentLoader.LoadTiles(MainWrapPanel, tiles);
		}
	}
}
