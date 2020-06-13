using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;
using EPlayer.Models;

namespace EPlayer.Tabs
{
	public partial class AlbumsTab : TabItem
	{
		public event TypedEventHandler<Album> AlbumRequested;
		public AlbumsTab()
		{
			InitializeComponent();
		}

		private void MetroTabItem_Loaded(object sender, RoutedEventArgs e)
		{
			Action<Album> onClick = (album) => AlbumRequested?.Invoke(this, album);
			System.Collections.Generic.IEnumerable<Controls.Tile> tiles =
				from album
				in Library.Albums
				select ContentLoader.GetTileForAlbum(album, onClick);
			ContentLoader.LoadTiles(MainWrapPanel, tiles);
		}
	}
}
