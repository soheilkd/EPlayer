using System;
using System.Windows;
using System.Windows.Controls;
using LibVLCSharp.Shared;

namespace EPlayer.Controls
{
	//TODO Cleanup
	public partial class MediaControlBar : UserControl
	{
		private MediaPlayer mediaPlayer;
		public MediaPlayer MediaPlayer
		{
			get => mediaPlayer;
			set
			{
				mediaPlayer = value;
				AssignMediaEvents();
				BindMediaElement();
				BindVolume();
			}
		}

		public bool IsPlaying
		{
			get => Dispatcher.Invoke(() => PlayPauseButton.Icon == SegoeIcon.Pause);
			set => Dispatcher.Invoke(() => PlayPauseButton.Icon = value ? SegoeIcon.Pause : SegoeIcon.Play);
		}

		public event EventHandler MediaFailed;
		public event EventHandler MediaEnded;

		public event EventHandler PreviousRequested;
		public event EventHandler NextRequested;

		public MediaControlBar()
		{
			InitializeComponent();
		}

		private void AssignMediaEvents()
		{
			MediaPlayer.EndReached += MediaPlayer_EndReached;
			MediaPlayer.EncounteredError += (_, e) => MediaFailed?.Invoke(this, e);
			MediaPlayer.LengthChanged += (_, e) => ProgressBar.Duration = e.Length;
			MediaPlayer.TimeChanged += (_, e) => ProgressBar.Position = e.Time;
			MediaPlayer.Playing += (_, e) => IsPlaying = true;
			MediaPlayer.Paused += (_, e) => IsPlaying = false;
		}

		private void MediaPlayer_EndReached(object sender, EventArgs e) => MediaEnded?.Invoke(this, e);

		private void BindMediaElement() => ProgressBar.PositionChanged += (_, e) => MediaPlayer.Position = e.Parameter / (float)ProgressBar.Duration;

		private void PreviousButton_Click(object sender, RoutedEventArgs e) => PreviousRequested?.Invoke(this, e);
		private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
		{
			if (IsPlaying)
				MediaPlayer.Pause();
			else
				MediaPlayer.Play();
		}
		private void NextButton_Click(object sender, RoutedEventArgs e) => NextRequested?.Invoke(this, e);

		private void BindVolume() => VolumeSlider.VolumeChanged += (_, e) => MediaPlayer.Volume = (int)e;
	}
}
