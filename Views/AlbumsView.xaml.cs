using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Library;
using EPlayer.Media;
using EPlayer.Models;
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
			void onClick(Album album) => MainWindow.RequestAlbum(album);
			var tiles =
				from album
				in App.MusicLibrary.Albums
				select ContentLoader.GetTileForAlbum(album, onClick);
			ContentLoader.LoadTiles(MainWrapPanel, tiles);
		}
	}
}
