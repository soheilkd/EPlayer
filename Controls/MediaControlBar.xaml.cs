using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;
using EPlayer.Models;
using NAudio.Wave;

namespace EPlayer.Controls
{
	public partial class MediaControlBar : UserControl
	{
		private readonly MediaController Controller = new MediaController();
		public MusicPlayer Player
		{
			get => Controller.Player;
			set => Controller.Player = value;
		}

		public MediaControlBar() => InitializeComponent();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Controller.Slider = ProgressBar;
			Controller.VolumeSlider = VolumeSlider;
			Controller.ControlButtons = Controls;
		}
	}
}
