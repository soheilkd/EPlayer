using System;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Contents;
using EPlayer.Models;

namespace EPlayer
{
	public partial class MainContent : Grid
	{
		private readonly ArtistContent MainArtistContent = new ArtistContent();
		private readonly AlbumContent MainAlbumContent = new AlbumContent();

		public MainContent()
		{
			InitializeComponent();
		}

		private void ContentGrid_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				ControlBar.MediaPlayer = App.MusicPlayer.Player;
			}
			catch (Exception) { }
		}

		private void Button_Click(object sender, RoutedEventArgs e) => ExtraContent.IsEnabled = !ExtraContent.IsEnabled;

		private void ArtistsTab_ArtistRequested(object sender, TypedEventArgs<Artist> e)
		{
			MainArtistContent.LoadForArtist(e);
			ExtraContent.Content = MainArtistContent;
			ExtraContent.Header = e.Parameter.Name;
			ExtraContent.IsEnabled = true;
		}

		private void AlbumsTab_AlbumRequested(object sender, TypedEventArgs<Album> e)
		{
			MainAlbumContent.LoadForAlbum(e);
			ExtraContent.Content = MainAlbumContent;
			ExtraContent.Header = e.Parameter.Name;
			ExtraContent.IsEnabled = true;
		}

		private void SongsTab_SongRequested(object sender, TypedEventArgs<Song> e)
		{
			App.MusicPlayer.Play(e);
		}

		private void ContentGrid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			MainArtistContent.Width = e.NewSize.Width / 3;
			MainAlbumContent.Width = e.NewSize.Width / 3;
		}
	}
}
