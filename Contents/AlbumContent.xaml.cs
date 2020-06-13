using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Models;

namespace EPlayer.Contents
{
	public partial class AlbumContent : Grid
	{
		public AlbumContent()
		{
			InitializeComponent();
		}

		public AlbumContent(Album album) : this()
		{
			LoadForAlbum(album);
		}

		public void LoadForAlbum(Album album)
		{
			MainListBox.Songs = album.Songs;
			TitleTextBlock.Text = album.Name;
			MainImage.Source = SegoeIcon.Album.Render();
			Task.Run(() => LoadImage(album));
		}

		private async void LoadImage(Album album)
		{
			await Task.Delay(1); //Ignore error
			ImageSource image = album.Songs.First().GetImage();
			Dispatcher.Invoke(delegate { MainImage.Source = image; });
		}
	}
}
