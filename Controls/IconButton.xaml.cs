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
			//Define default value 26 for FontSize
			FontSizeProperty.OverrideDefault(26D);
			//Define default value of Transparent for background
			BackgroundProperty.OverrideDefault(Brushes.Transparent, false);
			//Define default content
			ContentProperty.OverrideDefault(SegoeIcon.Smile);
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
