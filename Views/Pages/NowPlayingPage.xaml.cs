﻿using System.Windows;
using System.Windows.Controls;
using EPlayer.Controls.Media;
using EPlayer.Models;

namespace EPlayer.Views
{
	public partial class NowPlayingView : UserControl
	{
		private readonly Controller Controller = new Controller();
		public NowPlayingView() => InitializeComponent();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Controller.Player = App.MusicPlayer;
			Controller.Slider = ProgressBar;
			Controller.ControlButtons = Controls;
			App.MusicPlayer.SongChanged += Player_SongChanged;
		}

		private void Player_SongChanged(object sender, TypedEventArgs<Song> e)
		{
			AlbumArtImage.Source = e.Parameter.Image;
			TitleBlock.Text = e.Parameter.Title;
			ArtistBlock.Text = e.Parameter.Artist;
			AlbumBlock.Text = e.Parameter.Album;
		}
	}
}
