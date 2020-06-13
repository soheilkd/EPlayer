using System;

namespace EPlayer.Models
{
	[Serializable]
	public class Playlist : MediaQueue<Song>
	{
		public string Name { get; set; }
		public byte[] DisplayImage { get; set; }
	}
}
