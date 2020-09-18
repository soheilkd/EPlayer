using System;
using System.Linq;
using System.Windows;
using System.Windows.Shell;
using AdonisUI;
using AdonisUI.Controls;
using EPlayer.Extensions;
using EPlayer.Models;

namespace EPlayer.Windows
{
	//TODO: Clean
	public partial class MainWindow : AdonisWindow
	{
		internal static event TypedEventHandler<Artist> ArtistDisplayRequested;
		internal static event TypedEventHandler<Album> AlbumDisplayRequested;

		public MainWindow()
		{
			InitializeComponent();

			Drop += MainWindow_Drop;

			App.InstanceInvoked += (_, e) => Focus();
			App.MusicPlayer.SongChanged += MusicPlayer_SongChanged; 
		}

		private void MusicPlayer_SongChanged(object sender, TypedEventArgs<Song> e)
		{
			Title = $"Elephant Player | {e.Parameter.Title}";
			BackgroundImage.Source = e.Parameter.Image;
		}
		

		private void MainWindow_Drop(object sender, DragEventArgs e)
		{
			var data = e.Data.GetData(DataFormats.FileDrop) as string[];
			App.MusicLibrary.ReadNewData(data);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TaskbarItemInfo = new TaskbarItemInfo();
			var taskbarController = new Taskbar.Controller(TaskbarItemInfo);
			taskbarController.BindMusicPlayer(App.MusicPlayer);

			ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);

			ControlBar.Player = App.MusicPlayer;
		}

		public static void RequestAlbum(Album album) => AlbumDisplayRequested.Invoke(album);
		public static void RequestArtist(Artist artist) => ArtistDisplayRequested.Invoke(artist);

		private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ControlBar.Visibility = TabControl.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}