using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using EPlayer.Media;
using EPlayer.Models;

namespace EPlayer.Tabs
{
	public partial class HomeTab : TabItem
	{
		public event TypedEventHandler<Artist> ArtistRequested;
		public event TypedEventHandler<Album> AlbumRequested;
		public HomeTab()
		{
			InitializeComponent();
		}

		private void MetroTabItem_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Run(delegate
			  {
																				   //TODO: Refactor
																				   IEnumerable<Song> recentSongs = SongLibraryHelper.GetRecentlyAdded();
				  IEnumerable<Song> mostPlayedSongs = SongLibraryHelper.GetMostPlayed(DateTime.Now.AddDays(-14));
				  var recentObservable = new List<Song>();
				  var mostObservable = new List<Song>();
				  foreach (Song item in recentSongs)
					  recentObservable.Add(item);
				  foreach (Song item in mostPlayedSongs)
					  mostObservable.Add(item);
				  Dispatcher.Invoke(() =>
				  {
					  RecentSongsBox.Songs = recentObservable;
					  MostPlayedSongsBox.Songs = mostObservable;
					  RecentSongsBox.SongRequested += (_, f) => App.MusicPlayer.Play(f);
					  MostPlayedSongsBox.SongRequested += (_, f) => App.MusicPlayer.Play(f);
				  });
			  });
		}
	}
}
