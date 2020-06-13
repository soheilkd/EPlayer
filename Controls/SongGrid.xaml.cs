using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Windows;

namespace EPlayer.Controls
{
	public partial class SongGrid : ListView
	{
		public event TypedEventHandler<Song> SongRemoved;
		public event TypedEventHandler<Song> SongRequested;
		public event TypedEventHandler<string> ArtistRequested;
		public event TypedEventHandler<string> AlbumRequested;

		private MediaQueue<Song> queue;
		public MediaQueue<Song> Queue
		{
			get
			{
				//TODO need a better comparison of queue and songs here
				if (queue != null && Songs.Count == queue.Count)
				{
					return queue;
				}
				else
				{
					queue = new MediaQueue<Song>(Songs);
					return queue;
				}
			}
		}

		private List<Song> songs;
		public List<Song> Songs
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
			foreach (Song song in SelectedItems)
			{
				Songs.Remove(song);
				SongRemoved?.Invoke(song);
			}
		}
		private void Menu_DeleteClick(object sender, RoutedEventArgs e)
		{
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
				SongRemoved?.Invoke(item);
			}
		}
		private void Menu_LocationClick(object sender, RoutedEventArgs e)
		{
			foreach (Song item in SelectedItems)
				Process.Start("explorer.exe", "/select," + item.FilePath);
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
				SongRequested?.Invoke(this, SelectedItem as Song);
		}

		private void ListBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			//OrganizeAddToPlaylistMenu();
			//OrganizeRemoveFromPlaylistMenu();
		}
		private void ArtistHyperlink_Click(object sender, RoutedEventArgs e)
		{
			var link = (Hyperlink)e.OriginalSource;
			ArtistRequested?.Invoke(this, link.NavigateUri.OriginalString);
		}
		private void AlbumHyperlink_Click(object sender, RoutedEventArgs e)
		{
			var link = (Hyperlink)e.OriginalSource;
			AlbumRequested?.Invoke(this, link.NavigateUri.OriginalString);
		}
	}
}
