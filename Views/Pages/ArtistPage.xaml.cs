using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using EPlayer.Models;

namespace EPlayer.Pages
{
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
			//MainStackPanel.Children.Clear();
			//foreach (Album album in artist.Albums)
			//MainStackPanel.Children.Add(new AlbumPage(album));
			SongGrid.Songs = new SongQueue(artist.Songs);
			TitleTextBlock.Text = artist.Name;
			
		}
	}
}
