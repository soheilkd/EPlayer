using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Converters;
using EPlayer.Extensions;

namespace EPlayer.Controls
{
	public partial class MediaProgressBar : Slider
	{
		/*
		#region Properties
		private TimeSpan length = TimeSpan.Zero;
		public TimeSpan Length
		{
			get => length;
			set
			{
				length = value;
				Dispatcher.Invoke(delegate
				{
					FullTimeLabel.Content = value.FormatToString();
					PositionSlider.Maximum = value.TotalMilliseconds;
					PositionSlider.SmallChange = value.TotalMilliseconds / 100;
					PositionSlider.LargeChange = value.TotalMilliseconds / 10;
				});
			}
		}

		private TimeSpan position = TimeSpan.Zero;	
		public TimeSpan Position
		{
			get => position;
			set
			{
				position = value;
				if (IsUserChangingPosition)
					PositionChanged?.Invoke(this, value);
				Dispatcher.Invoke(delegate
				{
					CurrentTimeLabel.Content = value.FormatToString();
					PositionSlider.Value = value.TotalMilliseconds;
				});
			}
		}

		#endregion*/
		public MediaProgressBar() => InitializeComponent();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}

	public class TimeConverter : ValueConverter<double, string, object>
	{
		public override string Convert(double value, object parameter)
		{
			return TimeSpan.FromSeconds(value).FormatToString();
		}
		public override double ConvertBack(string value, object parameter)
		{
			return double.Parse(value);
		}
	}
}
