﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using EPlayer.Models;

namespace EPlayer.Media
{
	public static class MediaCollectionExtension
	{
		public static void RefreshWithPaths(this IList<Song> collection, params string[] paths)
		{
			for (var i = 0; i < paths.Length; i++)
			{
				var files = Directory.GetFiles(paths[i], "*.*", SearchOption.AllDirectories);
				RemoveDeletedFiles(collection);
				AddNewFiles(collection, files);
			}
		}
		public static void RemoveDeletedFiles(this IList<Song> collection) 
		{
			for (var i = 0; i < collection.Count; i++)
			{
				if (!File.Exists(collection[i].FilePath))
				{
					collection.RemoveAt(i);
					i--;
				}
			}
		}
		public static void AddNewFiles(this IList<Song> collection, string[] files) 
		{
			Song holder;
			for (var i = 0; i < files.Length; i++)
			{
				if (!collection.Any(item => item.FilePath == files[i]))
				{
					holder = new Song(files[i]);
					if (!holder.PossiblyCorrupt)
						collection.Add(holder);
				}
			}
		}
	}
}
