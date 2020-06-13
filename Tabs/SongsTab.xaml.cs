using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;
using EPlayer.Models;

namespace EPlayer.Tabs
{
	public partial class SongsTab : TabItem
	{
		public SongsTab()
		{
			InitializeComponent();
		}

		private void MetroTabItem_GotFocus(object sender, RoutedEventArgs e)
		{
			if (MainListBox.Songs == null)
				MainListBox.Songs = Library.Songs;
		}

		private void MainListBox_SongRequested(object sender, TypedEventArgs<Song> e)
		{
			App.MusicPlayer.Play(e);
		}
	}
}
