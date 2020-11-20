using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Models;

namespace EPlayer.Views
{
	public partial class HomeView : ContentControl
	{
		public HomeView()
		{
			InitializeComponent();
		}

		private void MetroTabItem_Loaded(object sender, RoutedEventArgs e)
		{
			var mostPlayedSongs = App.MusicLibrary.GetMostPlayed(DateTime.Now.AddDays(-14));
			MostPlayedSongsBox.Songs = new SongQueue(mostPlayedSongs);
			var recentSongs = App.MusicLibrary.Songs.Take(10);
			
		}
	}
}
