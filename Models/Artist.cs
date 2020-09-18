using System;
using System.Collections.Generic;

namespace EPlayer.Models
{
	[Serializable]
	public class Artist
	{
		public string Name { get; set; } = "Unknown Artist";
		public List<Album> Albums { get; set; } = new List<Album>();
		public IEnumerable<Song> Songs
		{
			get
			{
				foreach (Album album in Albums)
					foreach (Song song in album.Songs)
						yield return song;
			}
		}

		public Artist() { }
		public Artist(string name) => Name = name;
	}
}
