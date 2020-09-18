using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;
using EPlayer.Models;
using NAudio.Wave;

namespace EPlayer.Controls
{
	//TODO: There is Dispatcher.Invoke all over the place. Find a better solution
	public class MediaController
	{
		private readonly Timer PositionTimer;
		public event TypedEventHandler<TimeSpan> PositionChanged;

		//To make sure user is the one changing position, not the application (such as media progress)
		public bool IsUserChangingPosition { get; set; } = false;
		private readonly Stopwatch watch = new Stopwatch(); //To send PositionChanged every 50ms, not constantly
		private MusicPlayer player;
		public MusicPlayer Player
		{
			get => player;
			set
			{
				player = value;
				AssignMediaEvents();
				BindVolume();
				BindButtons();
			}
		}

		private Slider slider;
		public Slider Slider
		{
			get => slider;
			set
			{
				slider = value;
				BindSlider();
			}
		}

		private MediaButtons controlButtons;
		public MediaButtons ControlButtons
		{
			get => controlButtons;
			set
			{
				controlButtons = value;
				BindButtons();
			}
		}

		private VolumeSlider volumeSlider;
		public VolumeSlider VolumeSlider
		{
			get => volumeSlider;
			set
			{
				volumeSlider = value;
				BindVolume();
			}
		}
		private void BindSlider()
		{
			Slider.PreviewMouseDown += (_, __) => IsUserChangingPosition = true;
			Slider.PreviewMouseUp += (_, __) => IsUserChangingPosition = false;
			Slider.MouseLeave += (_, __) => IsUserChangingPosition = false;
			Slider.ValueChanged += Slider_ValueChanged;
			/*

		private TimeSpan length = TimeSpan.Zero;
		public TimeSpan Length
		{
			get => length;
			set
			{
				length = value;
				Maximum = value.TotalMilliseconds;
				Maximum = value.TotalMilliseconds;
				SmallChange = value.TotalMilliseconds / 100;
				LargeChange = value.TotalMilliseconds / 10;
			}
		}
		}*/
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (IsUserChangingPosition)
			{
				if (watch.ElapsedMilliseconds > 100 || !watch.IsRunning)
				{
					Player.Position = TimeSpan.FromSeconds(e.NewValue);
					PositionChanged?.Invoke(this, Player.Position);
					watch.Restart();
				}
			}
		}

		private void BindButtons()
		{
			if (Player == null || ControlButtons == null)
				return;
			ControlButtons.Next += (_, __) => Player.Next();
			ControlButtons.Previous += (_, __) => Player.Previous();
			ControlButtons.Play += (_, __) => Player.Play();
			ControlButtons.Pause += (_, __) => Player.Pause();
			Player.PlaybackStateChanged += (_, e) => ControlButtons.Dispatcher.Invoke(() => ControlButtons.IsPlaying = e.Parameter == PlaybackState.Playing);
		}
		public MediaController()
		{
			PositionTimer = new Timer(SyncPosition , null, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(250));
		}
		private void SyncPosition(object state)
		{
			if (Slider != null && Player != null)
			{
				Slider.Dispatcher.Invoke(() => Slider.Value = Player.Position.TotalSeconds);
			}
		}

		private void AssignMediaEvents()
		{
			Player.SongChanged += Player_SongChanged;
		}

		private void Player_SongChanged(object sender, TypedEventArgs<Song> e)
		{
			Slider.Maximum = Player.Length.TotalSeconds;
			Slider.Value = 0;
			//WaveBar.LoadWaveBar(e.Parameter.FilePath);
		}

		private void Player_PlaybackStateChanged(object sender, TypedEventArgs<PlaybackState> e)
		{
			ControlButtons.IsPlaying = e.Parameter == PlaybackState.Playing;
		}

		private void BindVolume()
		{
			if (VolumeSlider != null)
				VolumeSlider.VolumeChanged += (_, e) => Player.Volume = (int)e;
		}
	}
}
