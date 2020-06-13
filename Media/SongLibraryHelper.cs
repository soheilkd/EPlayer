using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using EPlayer.Extensions;
using EPlayer.Models;

namespace EPlayer.Media
{
	//TODO Refactor
	public static class SongLibraryHelper
	{
		private static List<Song> Songs => Library.Songs;
		private static List<Album> Albums => Library.Albums;
		private static List<Artist> Artists => Library.Artists;

		public static IEnumerable<Song> GetRecentlyAdded(int count = 10) => (from song in Songs select song).Take(count);
		public static IEnumerable<Song> GetMostPlayed(DateTime since, DateTime till = default, int count = 10)
		{
			if (till == default)
			{
				till = DateTime.Now;
			}

			return (from song in Songs
					where song.Plays.Since(since).Till(till).Any()
					orderby song.Plays.Since(since).Till(till).Count()
					select song).Take(count);
		}


		private static void ProcessAlbumForArtists(NotifyCollectionChangedEventArgs changedArgs)
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
		private static void AddAlbumToArtists(Album album)
		{
			Artist foundArtist = Artists.FirstOrDefault(artist => artist.Name == album.Artist);
			if (foundArtist == default)
			{
				Artists.Add(new Artist(album));
			}
			else
			{
				foundArtist.Albums.Add(album);
			}
		}
		private static void RemoveAlbumFromArtists(Album album)
		{
			Artist artist = Artists.First(artist => artist.Albums.Contains(album));
			artist.Albums.Remove(album);
			if (artist.Albums.Count == 0)
			{
				Artists.Remove(artist);
			}
		}

		private static void ProcessSongForAlbums(NotifyCollectionChangedEventArgs changedArgs)
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
		private static void AddSongToAlbums(Song song)
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
		private static void RemoveSongFromAlbums(Song song)
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
