using System.Windows;
using System.Windows.Input;

using EPlayer.Models;

using Microsoft.Win32;

namespace EPlayer.Windows
{
	//TODO IMPLEMENT
	public partial class PropertiesWindow : Window
	{
		private const char Seperator = ';';

		private readonly OpenFileDialog _OpenArtDialog = new OpenFileDialog()
		{
			CheckFileExists = true,
			Filter = "Images|*.jpg;*.png;*.jpeg",
			Title = "Open artwork"
		};
		//private readonly Song song;

		public PropertiesWindow(MediaItem item) : base()
		{
			TitleBox.Text = item.Title;
			//AlbumBox.Text = item.Album;
			//ArtistBox.Text = item.Artist;
			//AlbumArtistBox.Text = 
			//GenreBox.Text = string.Join(Seperator.ToString(), tag.Genres);
			//CommentBox.Text = tag.Comment;
			//CopyrightBox.Text = tag.Copyright;
			//LyricsBox.Text = tag.Lyrics;
			//ArtworkImage.Source = tag.Pictures.Length >= 1 ? tag.Pictures[0].GetBitmapImage() : IconProvider.GetBitmap(IconType.Music);
		}
		public PropertiesWindow()
		{
			InitializeComponent();
		}

		private void RemoveArtworkClick(object sender, MouseButtonEventArgs e)
		{
			/*_TagFile.Tag.Pictures = new TagLib.IPicture[0];
			ArtworkImage.Source = IconProvider.GetBitmap(IconType.Music);*/
		}
		private void SaveButtonClick(object sender, MouseButtonEventArgs e)
		{
			/*
			_TagFile.Tag.Title = TitleBox.Text;
			_TagFile.Tag.Album = AlbumBox.Text;
			_TagFile.Tag.Performers = ArtistBox.Text.Split(Seperator);
			_TagFile.Tag.AlbumArtists = AlbumArtistBox.Text.Split(Seperator);
			_TagFile.Tag.Genres = GenreBox.Text.Split(Seperator);
			_TagFile.Tag.Comment = CommentBox.Text;
			_TagFile.Tag.Copyright = CopyrightBox.Text;
			_TagFile.Tag.Lyrics = LyricsBox.Text;

			_TagFile.Save();
			_Media.Reload();
			Close();*/
		}
		private void ArtworkImage_MouseUp(object sender, MouseButtonEventArgs e)
		{
			/*
			if (_OpenArtDialog.ShowDialog() ?? false)
			{
				_TagFile.Tag.Pictures = new TagLib.IPicture[] { new TagLib.Picture(_OpenArtDialog.FileName) };
				ArtworkImage.Source = new BitmapImage(new Uri(_OpenArtDialog.FileName));
			}
			*/
		}
	}
}
