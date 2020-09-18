using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static EPlayer.Controls.CustomControlHelper;

namespace EPlayer.Controls
{
	public partial class Tile : Button
	{
		public static DependencyProperty TitleProperty = RegisterProperty<string>(nameof(Title));
		public string Title
		{
			get => GetValue(TitleProperty) as string;
			set => SetValue(TitleProperty, value);
		}

		public static DependencyProperty ImageSourceProperty = RegisterProperty<ImageSource>(nameof(ImageSource));
		public ImageSource ImageSource
		{
			get => GetValue(ImageSourceProperty) as ImageSource;
			set => SetValue(ImageSourceProperty, value);
		}

		public Tile()
		{
			InitializeComponent();
		}

		public void LoadImageFrom(Task<ImageSource> imageLoader)
		{
			imageLoader.Start();
			imageLoader.ContinueWith(image =>
			{
				Dispatcher.Invoke(() => ImageSource = image.Result);
			});
		}
	}
}
