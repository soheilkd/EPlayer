using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using EPlayer.Extensions;
using EPlayer.Models;
using EPlayer.Serialization;

namespace EPlayer.MusicLibrary
{
	//TODO: Needs refactor
	public class Library
	{
		private LibraryData data = new LibraryData();
		private static string LibraryPath => PathHelper.AppdataPath + "Library.bin";
		public ObservableCollection<Song> Songs => data.Songs;
		public ObservableCollection<Album> Albums => data.Albums;
		public ObservableCollection<Artist> Artists => data.Artists;
		public ObservableCollection<Playlist> Playlists => data.Playlists;

		public event TypedEventHandler<Song> NewSongAdded;

		public Library()
		{
			Load();
		}

		public void Load()
		{
			data = LibraryData.LoadFrom(LibraryPath);
			RefreshLibraries();
			ReadNewData(Environment.GetCommandLineArgs());
		}

		public void Save() => data.SaveTo(LibraryPath);

		public void RefreshLibraries()
		{
			var common = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
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
			throw new NotImplementedException();
			return from song in Songs
				  // where song.Plays.Since(since).Till(till).Any()
				  // orderby song.Plays.Since(since).Till(till).Count()
				   select song;
		}


		public void AddFromPaths(params string[] paths)
		{
			for (var i = 0; i < paths.Length; i++)
			{
				var files = Directory.GetFiles(paths[i], "*.*", SearchOption.AllDirectories);
				AddNewFiles(files);
			}
		}

		public void RemoveDeletedFiles()
		{
			for (var i = 0; i < Songs.Count; i++)
			{
				if (!File.Exists(Songs[i].FilePath))
				{
					Songs.RemoveAt(i);
					i--;
				}
			}
		}
		public void AddNewFiles(string[] files)
		{
			Song holder;
			for (var i = 0; i < files.Length; i++)
			{
				if (!Songs.Any(item => item.FilePath == files[i]))
				{
					holder = new Song(files[i]);
					if (!holder.PossiblyCorrupt)
						Songs.Add(holder);
				}
			}
		}
	}
}
