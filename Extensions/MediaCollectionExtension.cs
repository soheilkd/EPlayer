using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EPlayer.Models;

namespace EPlayer.Media
{
	public static class MediaCollectionExtension
	{
		public static void RefreshWithPaths<T>(this List<T> collection, params string[] paths) where T : MediaItem
		{
			for (var i = 0; i < paths.Length; i++)
			{
				var files = Directory.GetFiles(paths[i], "*.*", SearchOption.AllDirectories);
				RemoveDeletedFiles(collection);
				AddNewFiles(collection, files);
			}
		}
		public static void RemoveDeletedFiles<T>(this List<T> collection) where T : MediaItem
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
		public static void AddNewFiles<T>(this List<T> collection, string[] files) where T : MediaItem
		{
			T holder;
			Type type = typeof(T);
			for (var i = 0; i < files.Length; i++)
			{
				if (!collection.Any(video => video.FilePath == files[i]))
				{
					holder = Activator.CreateInstance(type, files[i]) as T;
					if (!holder.PossiblyCorrupt)
					{
						collection.Add(Activator.CreateInstance(typeof(T), files[i]) as T);
					}
				}
			}
		}
	}
}
