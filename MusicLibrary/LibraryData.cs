using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Serialization;

namespace EPlayer.Library
{
	//For saving the library data to a file to retrieve 
	[Serializable]
	public class LibraryData : LoadableObject<LibraryData>
	{
		public ObservableCollection<Song> Songs = new ObservableCollection<Song>();
		public ObservableCollection<Album> Albums = new ObservableCollection<Album>();
		public ObservableCollection<Artist> Artists = new ObservableCollection<Artist>();
		public ObservableCollection<Playlist> Playlists = new ObservableCollection<Playlist>();

	}
}