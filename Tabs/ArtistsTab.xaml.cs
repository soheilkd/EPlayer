using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;
using EPlayer.Models;

namespace EPlayer.Tabs
{
	public partial class ArtistsTab : TabItem
	{
		public event TypedEventHandler<Artist> ArtistRequested;
		public ArtistsTab()
		{
			InitializeComponent();
		}

		private void MetroTabItem_Loaded(object sender, RoutedEventArgs e)
		{
			void onClick(Artist artist) => ArtistRequested?.Invoke(this, artist);
			System.Collections.Generic.IEnumerable<Controls.Tile> tiles =
				from artist
				in Library.Artists
				select ContentLoader.GetTileForArtist(artist, onClick);
			ContentLoader.LoadTiles(MainWrapPanel, tiles);
		}
	}
}
