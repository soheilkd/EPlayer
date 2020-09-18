using System.Threading.Tasks;
using System.Windows.Controls;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Media;
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
			MainListBox.Songs = new SongQueue(album.Songs);
			TitleTextBlock.Text = album.Name;
			MainImage.Source = SegoeIcon.Album.Render();
			Task.Run(() => LoadImage(album));
		}

		private async void LoadImage(Album album)
		{
			await Task.Delay(1); //Ignore error
			var image = await ImageController.LoadAlbumImage(album.Name);
			Dispatcher.Invoke(delegate { MainImage.Source = image; });
		}
	}
}
