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
	/// Interaction logic for ArtistPage.xaml
	/// </summary>
	public partial class ArtistPage : Page
	{
		public ArtistPage()
		{
			InitializeComponent();
		}
		public ArtistPage(Artist artist) : this()
		{
			LoadForArtist(artist);
		}

		public void LoadForArtist(Artist artist)
		{
			MainStackPanel.Children.Clear();
			foreach (Album album in artist.Albums)
				MainStackPanel.Children.Add(new AlbumPage(album));
			TitleTextBlock.Text = artist.Name;
		}
	}
}
