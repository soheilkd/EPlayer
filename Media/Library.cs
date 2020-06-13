using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EPlayer.Models;
using EPlayer.Windows;


namespace EPlayer.Media
{
	public static class Library
	{
		private static readonly string libraryPath = App.Path + "Library.bin";
		private static LibraryData data = new LibraryData();
		public static List<Song> Songs => data.Songs;
		public static List<Album> Albums => data.Albums;
		public static List<Artist> Artists => data.Artists;
		public static List<Video> Videos => data.Videos;
		public static List<Playlist> Playlists => data.Playlists;

		public static void Load()
		{
			data = LibraryData.LoadFrom(libraryPath);
			RefreshLibraries();
			ReadNewData(Environment.GetCommandLineArgs());
		}

		public static void Save() => data.SaveTo(libraryPath);

		public static void RefreshLibraries()
		{
			//ScanFolder(KnownFolders.Videos.Path);
			//ScanFolder(KnownFolders.Music.Path);
		}

		public static void ScanFolder(string path)
		{
			var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
			for (var i = 0; i < files.Length; i++)
				TryAddFile(files[i], out _);
		}

		//TODO: Refactor
		public static bool TryAddFile(string filePath, out MediaItem addedItem)
		{
			var ext = new FileInfo(filePath).Extension.ToLower();
			if (Song.ValidFormats.Contains(ext))
			{
				addedItem = Songs.FirstOrDefault(song => song.FilePath == filePath);
				if (addedItem == default)
				{
					Songs.Insert(0, new Song(filePath));
					addedItem = Songs.Last();
				}
			}
			else if (Video.ValidFormats.Contains(filePath))
			{
				addedItem = Videos.Last();
				if (Videos.Any(video => video.FilePath == filePath))
				{
					addedItem = Videos.First(video => video.FilePath == filePath);
				}
				else
				{
					Videos.Insert(0, new Video(filePath));
					addedItem = Videos.Last();
				}
			}
			else
			{
				addedItem = null;
			}

			return addedItem != null;
		}

		public static void SortSongs()
		{
			IOrderedEnumerable<Song> sorted = Songs.OrderByDescending(each => each.AdditionDate);
			data.Songs.Clear();
			foreach (Song item in sorted) data.Songs.Add(item);
		}

		public static void ReadNewData(string[] data)
		{
			MediaItem lastAddedItem = default;
			for (var i = 0; i < data.Length; i++)
			{
				if (Library.TryAddFile(data[i], out MediaItem item))
					lastAddedItem = item;
			}
			if (lastAddedItem is Song song)
				MainWindow.InvokeNewSongAdded(song);
		}
	}
}
