using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static EPlayer.Controls.CustomControlHelper;

namespace EPlayer.Controls
{
	public partial class IconButton : Button
	{
		public IconButton()
		{
			InitializeComponent();
		}

		static IconButton()
		{
			//Define default value 20 for FontSize
			FontSizeProperty.OverrideDefault(20D);
			//Define default value of LightGray for Foreground
			ForegroundProperty.OverrideDefault(Brushes.LightGray, false);
			//Define default value of Transparent for background
			BackgroundProperty.OverrideDefault(Brushes.Transparent, false);
		}

		public static DependencyProperty IconProperty = RegisterProperty(nameof(Icon), nameof(OnIconChange), SegoeIcon.Smile);
		public SegoeIcon Icon
		{
			get => (SegoeIcon)GetValue(IconProperty);
			set => SetValue(IconProperty, value);
		}
		private void OnIconChange(SegoeIcon newIcon) => Content = char.ConvertFromUtf32((int)newIcon);
	}
}
