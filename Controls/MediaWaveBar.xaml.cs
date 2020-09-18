using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Converters;
using EPlayer.Extensions;
using EPlayer.Wave;

namespace EPlayer.Controls
{
	public partial class MediaWaveBar : Slider
	{
		private readonly WaveFormRenderer Renderer = new WaveFormRenderer();

		public MediaWaveBar() => InitializeComponent();

		private void ToogleLoadingBar(bool enabled)
		{
			//Image1.Visibility = enabled ? Visibility.Hidden : Visibility.Visible;
			//Image2.Visibility = enabled ? Visibility.Hidden : Visibility.Visible;
			//LoadingBar.Visibility = enabled ? Visibility.Visible : Visibility.Hidden;
			//LoadingBar.IsIndeterminate = enabled;
		}
		public void LoadWaveBar(string file)
		{
			ToogleLoadingBar(true);
			Task.Run(() =>
			Renderer.Render(file, Renderer.GrayBlocks, Renderer.OrangeBlocks))
				.ContinueWith(images =>
				{
					Dispatcher.Invoke(delegate
					{
						//Image1.Source = images.Result[0].ToImageSource();
						//Image2.Source = images.Result[1].ToImageSource();
						ToogleLoadingBar(false);
					});
				});
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}

	public class WaveBarProgressConverter : ValueConverter<double, double, string>
	{
		public override double Convert(double value, string parameter)
		{
			var par = parameter.Split('|').Select(each => double.Parse(each)).ToArray();
			return value * par[0] / par[1];
		}
		public override double ConvertBack(double value, string parameter)
		{
			var par = parameter.Split('|').Select(each => double.Parse(each)).ToArray();
			return value * par[1] / par[0];
		}
	}
}
