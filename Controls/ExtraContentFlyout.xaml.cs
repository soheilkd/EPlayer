using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static EPlayer.Controls.CustomControlHelper;

namespace EPlayer.Controls
{
	public partial class ExtraContentFlyout : ContentControl
	{
		public static DependencyProperty HeaderProperty = RegisterProperty<string>(nameof(Header));
		public string Header
		{
			get => (string)GetValue(HeaderProperty);
			set => SetValue(HeaderProperty, value);
		}

		public ExtraContentFlyout()
		{
			InitializeComponent();
		}

		private void root_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (IsEnabled)
				Focus();
		}

		private void parentGrid_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if ((e.OriginalSource as FrameworkElement).Name == "parentGrid")
				IsEnabled = false;
		}

		private void IconButton_Click(object sender, RoutedEventArgs e) => IsEnabled = !IsEnabled;
	}
}
