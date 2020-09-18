using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;
using EPlayer.Models;

namespace EPlayer.Views
{
	public partial class SongsView : ContentControl
	{
		public SongsView()
		{
			InitializeComponent();
		}

		private void MetroTabItem_GotFocus(object sender, RoutedEventArgs e)
		{
			if (MainListBox.Songs == null)
				MainListBox.Songs = new SongQueue(App.MusicLibrary.Songs);
		}
	}
}