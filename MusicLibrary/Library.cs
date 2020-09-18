using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using EPlayer.Extensions;
using EPlayer.Models;

namespace EPlayer.Library
{
	public class MusicLibrary
	{
		private readonly string libraryPath = App.Path + "Library.bin";
		private LibraryData data = new LibraryData();
		public ObservableCollection<Song> Songs => data.Songs;
		public ObservableCollection<Album> Albums => data.Albums;
		public ObservableCollection<Artist> Artists => data.Artists;
		public ObservableCollection<Playlist> Playlists => data.Playlists;

		public event TypedEventHandler<Song> NewSongAdded;

		public MusicLibrary()
		{
			Load();

			Songs.CollectionChanged += (_, e) => ProcessSongForAlbums(e);
			Albums.CollectionChanged += (_, e) => ProcessAlbumForArtists(e);
		}

		public void Load()
		{
			data = LibraryData.LoadFrom(libraryPath);
			RefreshLibraries();
			ReadNewData(Environment.GetCommandLineArgs());
		}

		public void Save() => data.SaveTo(libraryPath);

		public void RefreshLibraries()
		{
			var common  = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
			var music = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			ScanFolder(common);
			ScanFolder(music);
		}

		public void ScanFolder(string path)
		{
			var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
			for (var i = 0; i < files.Length; i++)
				TryAddFile(files[i], out _);
		}

		public bool TryAddFile(string filePath, out Song addedItem)
		{
			addedItem = Songs.FirstOrDefault(song => song.FilePath == filePath);
			if (addedItem == default)
			{
				addedItem = new Song(filePath);
				if (!addedItem.PossiblyCorrupt)
				{
					Songs.Insert(0, addedItem);
					return true;
				}
				else
					return false;
			}
			else
				return false;
		}

		public void SortSongs()
		{
			var sorted = Songs.OrderByDescending(each => each.AdditionDate);
			Songs.Clear();
			foreach (Song item in sorted) Songs.Add(item);
		}

		public void ReadNewData(string[] data)
		{
			Song lastAddedItem = default;
			for (var i = 0; i < data.Length; i++)
			{
				if (TryAddFile(data[i], out var item))
					lastAddedItem = item;
			}
			if (lastAddedItem is Song song)
				NewSongAdded.Invoke(song);
		}

		public IEnumerable<Song> GetMostPlayed(DateTime since, DateTime till = default)
		{
			if (till == default)
				till = DateTime.Now;

			return from song in Songs
				   where song.Plays.Since(since).Till(till).Any()
				   orderby song.Plays.Since(since).Till(till).Count()
				   select song;
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
