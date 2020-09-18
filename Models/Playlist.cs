using System;
using System.Collections.Generic;

namespace EPlayer.Models
{
	[Serializable]
	public class Playlist
	{
		public List<Song> Songs { get; set; }
		public string Name { get; set; }
		public byte[] DisplayImage { get; set; }
	}
}
