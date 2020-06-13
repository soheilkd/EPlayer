using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace EPlayer.Controls
{
	public partial class MediaProgressBar : Grid
	{
		public event TypedEventHandler<long> PositionChanged;
		//To make sure user is the one changing position, not the application (such as media progress)
		public bool IsUserChangingPosition { get; set; } = false;

		private readonly Stopwatch watch = new Stopwatch(); //To send PositionChanged every 50ms, not constantly
		public long WatchElapsedMilliseconds => watch.ElapsedMilliseconds; //To measure how long song is played constantly

		#region Properties
		private long duration;
		public long Duration
		{
			get => duration;
			set
			{
				duration = value;
				Dispatcher.Invoke(delegate
				{
					FullTimeLabel.Content = FormatTimeSpan(TimeSpan.FromMilliseconds(value));
					PositionSlider.Maximum = value;
					PositionSlider.SmallChange = value / 100;
					PositionSlider.LargeChange = value / 10;
				});
			}
		}

		private long position;
		public long Position
		{
			get => position;
			set
			{
				position = value;
				if (IsUserChangingPosition)
					PositionChanged?.Invoke(this, value);

				Dispatcher.Invoke(delegate
				{
					CurrentTimeLabel.Content = FormatTimeSpan(TimeSpan.FromMilliseconds(value));
					PositionSlider.Value = value;
				});
			}
		}

		#endregion
		public MediaProgressBar()
		{
			InitializeComponent();
		}

		public void SlidePosition(FlowDirection direction, bool small = true)
		{
			if (direction == FlowDirection.LeftToRight)
			{
				PositionSlider.Value += small ? PositionSlider.SmallChange : PositionSlider.LargeChange;
			}
			else
			{
				PositionSlider.Value -= small ? PositionSlider.SmallChange : PositionSlider.LargeChange;
			}
		}

		private static string FormatTimeSpan(TimeSpan timeSpan) => string.Format("{0:mm\\:ss}", timeSpan);
		//Idk where this came from, but I'll keep it anyway to test if it works better than formatting
		private static string FormatTimeSpan2(TimeSpan time) => time.ToString("c").Substring(3, 5);

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			PreviewMouseDown += (_, __) => IsUserChangingPosition = true;
			PreviewMouseUp += (_, __) => IsUserChangingPosition = false;
			MouseLeave += (_, __) => IsUserChangingPosition = false;
		}

		private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (IsUserChangingPosition)
			{
				if (watch.ElapsedMilliseconds > 100 || !watch.IsRunning)
				{
					Position = (long)e.NewValue;
					watch.Restart();
				}
			}
		}
	}
}
