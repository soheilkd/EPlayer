using System;
using System.Collections.Generic;

namespace EPlayer.Models
{
	[Serializable]
	public class Album
	{
		public string Name { get; set; }
		public string Artist { get; set; }
		public List<Song> Songs { get; set; }

		public Album() { }
		public Album(Song song) : base()
		{
			Name = song.Album;
			Artist = song.AlbumArtist;
			Songs = new List<Song>() { song };
		}
	}
}
