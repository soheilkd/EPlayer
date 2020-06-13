using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Windows;

namespace EPlayer.Controls
{
	public partial class VideoListBox : ListBox
	{
		public event TypedEventHandler<MediaItem> MediaRemoved;

		private MediaQueue<Video> queue;
		public MediaQueue<Video> Queue
		{
			get
			{
				//TODO need a better comparison of queue and songs here
				if (queue != null && Videos.Count == queue.Count)
				{
					return queue;
				}
				else
				{
					queue = new MediaQueue<Video>(Videos);
					return queue;
				}
			}
		}

		private ObservableCollection<Video> videos;
		public ObservableCollection<Video> Videos
		{
			get => videos;
			set
			{
				ItemsSource = value;
				videos = value;
			}
		}

		public VideoListBox()
		{
			InitializeComponent();
		}


		private void Menu_RemoveClick(object sender, RoutedEventArgs e)
		{
			foreach (Video video in SelectedItems)
			{
				Videos.Remove(video);
				MediaRemoved?.Invoke(video);
			}
		}
		private void Menu_DeleteClick(object sender, RoutedEventArgs e)
		{
			var msg = "Sure? These will be deleted:\r\n";
			foreach (Video video in SelectedItems)
			{
				msg += $"{video.FilePath}\r\n";
			}
			if (MessageBox.Show(msg, "Sure?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
			{
				return;
			}

			foreach (Video item in SelectedItems)
			{
				File.Delete(item.FilePath);
				Videos.Remove(item);
				MediaRemoved?.Invoke(item);
			}
		}
		private void Menu_LocationClick(object sender, RoutedEventArgs e)
		{
			foreach (Video item in SelectedItems)
			{
				Process.Start("explorer.exe", "/select," + item.FilePath);
			}
		}
		private void Menu_PropertiesClick(object sender, RoutedEventArgs e)
		{
			foreach (Video item in SelectedItems)
			{
				var window = new PropertiesWindow(item);
				window.Show();
			}
		}

		private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (SelectedItem == null)
			{
				return;
			}
			//VideoWindow.LoadVideo(SelectedItem as Video);
			//VideoWindow.Show();
		}

		//private readonly VideoWindow VideoWindow = new VideoWindow();

		private void ListBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			//OrganizeAddToPlaylistMenu();
			//OrganizeRemoveFromPlaylistMenu();
		}
	}
}
