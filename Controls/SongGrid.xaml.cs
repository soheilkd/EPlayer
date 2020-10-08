using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Windows;

namespace EPlayer.Controls
{
	//TODO: Refactor
	public partial class SongGrid : DataGrid
	{
		public event TypedEventHandler<Song[]> DeleteRequested;
		public event TypedEventHandler<Song[]> SongRemoved;

		private SongQueue songs;
		public SongQueue Songs
		{
			get => songs;
			set
			{
				ItemsSource = value;
				songs = value;
			}
		}

		public SongGrid()
		{
			InitializeComponent();
		}

		private void Menu_EditTag(object sender, RoutedEventArgs e)
		{
			foreach (Song song in SelectedItems)
			{
				//Super unprofessinal to add installed app on my pc only, but it'll make my life easier anyway
				Process.Start(@"D:\Apps\Mp3tag\Mp3tag.exe", $"\"{song.FilePath}\"");
			}
		}
		private void Menu_RemoveClick(object sender, RoutedEventArgs e)
		{
			SongRemoved?.Invoke(CastSelectedItems());
			foreach (Song song in SelectedItems)
				Songs.Remove(song);
			Songs = Songs;
		}
		private void Menu_DeleteClick(object sender, RoutedEventArgs e)
		{
			DeleteRequested?.Invoke(CastSelectedItems());
			/*
			var msg = "Sure? These will be deleted:\r\n";
			foreach (Song song in SelectedItems)
			{
				msg += $"{song.FilePath}\r\n";
			}
			if (MessageBox.Show(msg, "Sure?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
			{
				return;
			}

			foreach (Song item in SelectedItems)
			{
				File.Delete(item.FilePath);
				Songs.Remove(item);
				Songs = Songs;
				SongRemoved?.Invoke(item);
			}*/
		}
		private void Menu_LocationClick(object sender, RoutedEventArgs e)
		{
			//LocationRequested?.Invoke(CastSelectedItems()); TODO: Implement
		}
		private void Menu_PropertiesClick(object sender, RoutedEventArgs e)
		{
			foreach (Song item in SelectedItems)
			{
				var window = new PropertiesWindow(item);
				window.Show();
			}
		}

		private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (SelectedItem != null)
				App.MusicPlayer.Play(SelectedItem as Song, Songs);
		}

		private void ListBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			//OrganizeAddToPlaylistMenu();
			//OrganizeRemoveFromPlaylistMenu();
		}
		private void ArtistHyperlink_Click(object sender, RoutedEventArgs e)
		{
			var link = (Hyperlink)e.OriginalSource;
			//TODO: Do better
			//MainWindow.RequestArtist(App.MusicLibrary.Artists.First(art => art.Name == link.NavigateUri.OriginalString));
		}
		private void AlbumHyperlink_Click(object sender, RoutedEventArgs e)
		{
			var link = (Hyperlink)e.OriginalSource;
			//TODO: Do better
			//MainWindow.RequestAlbum(App.MusicLibrary.Albums.First(alb => alb.Name == link.NavigateUri.OriginalString));
		}

		private Song[] CastSelectedItems() => SelectedItems.Cast<Song>().ToArray();
	}
}
