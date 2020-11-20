using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Serialization;

namespace EPlayer.MusicLibrary
{
	//For saving the library data to a file to retrieve 
	[Serializable]
	public class LibraryData : LoadableObject<LibraryData>
	{
		public ObservableCollection<Song> Songs = new ObservableCollection<Song>();
		public ObservableCollection<Album> Albums = new ObservableCollection<Album>();
		public ObservableCollection<Artist> Artists = new ObservableCollection<Artist>();
		public ObservableCollection<Playlist> Playlists = new ObservableCollection<Playlist>();

		public LibraryData()
		{

			Songs.CollectionChanged += (_, e) => ProcessSongForAlbums(e);
			Albums.CollectionChanged += (_, e) => ProcessAlbumForArtists(e);
		}

		private void ProcessAlbumForArtists(NotifyCollectionChangedEventArgs changedArgs)
		{
			Album newAlbum = changedArgs.NewItems.FirstOrDefault<Album>();
			Album oldAlbum = changedArgs.OldItems.FirstOrDefault<Album>();
			if (changedArgs.Action == NotifyCollectionChangedAction.Add)
			{
				AddAlbumToArtists(newAlbum);
			}
			else if (changedArgs.Action == NotifyCollectionChangedAction.Remove)
			{
				RemoveAlbumFromArtists(oldAlbum);
			}
			else if (changedArgs.Action == NotifyCollectionChangedAction.Replace)
			{
				RemoveAlbumFromArtists(oldAlbum);
				AddAlbumToArtists(newAlbum);
			}
		}
		private void AddAlbumToArtists(Album album)
		{
			Artist foundArtist = Artists.FirstOrDefault(artist => artist.Name == album.Artist);
			if (foundArtist == default)
			{
				var artist = new Artist(album.Artist);
				artist.Albums.Add(album);
				Artists.Add(artist);
			}
			else
			{
				foundArtist.Albums.Add(album);
			}
		}
		private void RemoveAlbumFromArtists(Album album)
		{
			Artist artist = Artists.First(artist => artist.Albums.Contains(album));
			artist.Albums.Remove(album);
			if (artist.Albums.Count == 0)
			{
				Artists.Remove(artist);
			}
		}

		private void ProcessSongForAlbums(NotifyCollectionChangedEventArgs changedArgs)
		{
			Song newSong = changedArgs.NewItems.FirstOrDefault<Song>();
			Song oldSong = changedArgs.OldItems.FirstOrDefault<Song>();
			switch (changedArgs.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddSongToAlbums(newSong);
					break;
				case NotifyCollectionChangedAction.Remove:
					RemoveSongFromAlbums(oldSong);
					break;
				case NotifyCollectionChangedAction.Replace:
					RemoveSongFromAlbums(oldSong);
					AddSongToAlbums(newSong);
					break;
				default:
					break;
			}
		}
		private void AddSongToAlbums(Song song)
		{
			Album foundAlbum = Albums.FirstOrDefault(album => song.DoesPairWith(album));
			if (foundAlbum == default)
			{
				Albums.Add(new Album(song));
			}
			else
			{
				foundAlbum.Songs.Add(song);
			}
		}
		private void RemoveSongFromAlbums(Song song)
		{
			Album album = Albums.First(album => album.Songs.Contains(song));
			album.Songs.Remove(song);
			if (album.Songs.Count == 0)
			{
				Albums.Remove(album);
			}
		}

	}
}