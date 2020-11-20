using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EPlayer.Models;

namespace EPlayer.Pages
{
	/// <summary>
	/// Interaction logic for AlbumPage.xaml
	/// </summary>
	public partial class AlbumPage : Page
	{
		public AlbumPage()
		{
			InitializeComponent();
		}
		public AlbumPage(Album album) : this()
		{
			LoadForAlbum(album);
		}

		public void LoadForAlbum(Album album)
		{
			MainListBox.Songs = new SongQueue(album.Songs);
			TitleTextBlock.Text = album.Name;
		}
	}
}
