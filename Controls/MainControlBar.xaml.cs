
using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;

namespace EPlayer.Controls
{
	public partial class MainControlBar : UserControl
	{
		private readonly MediaController Controller = new MediaController();
		public MusicPlayer Player
		{
			get => Controller.Player;
			set => Controller.Player = value;
		}

		public MainControlBar() => InitializeComponent();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Controller.Slider = ProgressBar;
			Controller.VolumeSlider = VolumeSlider;
			Controller.ControlButtons = Controls;
		}
	}
}
