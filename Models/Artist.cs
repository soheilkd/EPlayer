using System;
using System.Collections.Generic;

namespace EPlayer.Models
{
	[Serializable]
	public class Artist
	{
		public string Name { get; set; }
		public string ImageUri { get; set; }
		public List<Album> Albums { get; set; }
		public IEnumerable<Song> Songs
		{
			get
			{
				foreach (Album album in Albums)
				{
					foreach (Song song in album.Songs)
					{
						yield return song;
					}
				}
			}
		}

		public Artist() { }
		public Artist(Album album)
		{
			Name = album.Artist;
			Albums = new List<Album> { album };
		}
	}
}
