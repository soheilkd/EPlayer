
using System.Windows;
using System.Windows.Controls;
using EPlayer.Media;

namespace EPlayer.Controls.Media
{
	public partial class MainControlBar : UserControl
	{
		private readonly Controller Controller = new Controller();
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
			Controller.ArtworkImage = Image;
		}
	}
}
