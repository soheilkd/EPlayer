using System;
using System.Collections.Generic;
using EPlayer.Models;
using EPlayer.Serialization;

namespace EPlayer.Media
{
	[Serializable]
	public class LibraryData : LoadableObject<LibraryData>
	{
		public List<Song> Songs = new List<Song>();
		public List<Album> Albums = new List<Album>();
		public List<Artist> Artists = new List<Artist>();
		public List<Video> Videos = new List<Video>();
		public List<Playlist> Playlists = new List<Playlist>();
	}
}
