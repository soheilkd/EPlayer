using System.Windows;
using System.Windows.Controls;

namespace EPlayer.Controls.Media
{
	public partial class VolumeSlider : UserControl
	{
		public event TypedEventHandler<double> VolumeChanged;
		private double volume;
		public double Volume
		{
			get => volume;
			set
			{
				volume = value;
				MainSlider.Value = value;
				VolumeChanged?.Invoke(this, value);
			}
		}


		public VolumeSlider()
		{
			InitializeComponent();
		}

		private void MainSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Volume = e.NewValue;
	}
}
