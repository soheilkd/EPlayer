using System.Windows.Controls;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Models;

namespace EPlayer.Contents
{
	public partial class ArtistContent : Grid
	{
		public ArtistContent()
		{
			InitializeComponent();
		}

		public ArtistContent(Artist artist) : this()
		{
			LoadForArtist(artist);
		}

		public void LoadForArtist(Artist artist)
		{
			MainStackPanel.Children.Clear();
			foreach (Album album in artist.Albums)
				MainStackPanel.Children.Add(new AlbumContent(album));
			MainImage.Source = SegoeIcon.Person.Render();
			TitleTextBlock.Text = artist.Name;
		}
	}
}
