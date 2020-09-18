using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EPlayer.Controls;
using EPlayer.Extensions;
using TagLib;
using static System.IO.Path;

namespace EPlayer.Models
{
	//TODO: Cleanup
	[Serializable]
	public class Song
	{
		public static readonly string[] ValidFormats = new[]
		{
			".mp3",
			".wav",
			".aac",
			".ac3",
			".wma",
			".flac",
		};


		public string FilePath { get; private set; } = string.Empty;
		public DateTime AdditionDate { get; set; } = DateTime.Today;

		public List<DateTime> Plays { get; set; } = new List<DateTime>();
		public string FileTitle { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(Title))
					return Title;
				else if (!string.IsNullOrWhiteSpace(FileTitle))
					return FileTitle;
				else
					return "Unkown";
			}
		}
		public bool IsPlaying { get; set; } = false;
		public TimeSpan Duration { get; set; } = TimeSpan.Zero;
		public bool PossiblyCorrupt { get; set; }

		public string Artist { get; set; } = "Unkown Artist";
		public string Album { get; set; } = "Unknown Album";
		public string AlbumArtist { get; set; } = "Unknown Artist";
		public uint Track { get; set; } = 0;
		public uint Year { get; set; } = 0;
		public string[] Genres { get; set; } = Array.Empty<string>();
		public string Lyrics { get; set; } = string.Empty;
		[NonSerialized]
		private ImageSource imageCache;
		public ImageSource Image
		{
			get
			{
				if (imageCache != default)
					return imageCache;
				try
				{
					using var file = File.Create(FilePath);
					var pic = file.Tag.Pictures.FirstOrDefault();
					var picsToByte = pic != default ? pic.Data.ToArray() : default; //TODO:Refactor
					imageCache = picsToByte != default ? picsToByte.GetImageSource() : SegoeIcon.LikeFilled.Render();
					return imageCache;
				}
				catch (Exception) { return default; }
			}
		}

		public Song() : base() { }
		public Song(string path)
		{
			if (ValidFormats.Contains(GetExtension(path).ToLower()))
			{
				FilePath = path;
				FileTitle = GetFileNameWithoutExtension(path);
				try
				{
					var file = File.Create(path);
					if (file.Tag != null)
						ReadInfoFromFile(file);
					else
						PossiblyCorrupt = true;
				}
				catch (Exception)
				{
					PossiblyCorrupt = true;
				}
			}
			else
			{
				PossiblyCorrupt = true;
			}
		}

		public bool IsIn(Album album) => album.Songs.Contains(this);
		public bool DoesPairWith(Album album) => album.Name == Album && album.Artist == AlbumArtist;

		 void ReadInfoFromFile(File file)
		{
			Tag tag = file.Tag;
			Title = tag.Title;
			Duration = file.Properties.Duration;

			if (!string.IsNullOrWhiteSpace(tag.FirstPerformer))
				Artist = tag.FirstPerformer;
			if (!string.IsNullOrWhiteSpace(tag.FirstAlbumArtist))
				AlbumArtist = tag.FirstAlbumArtist;
			if (!string.IsNullOrWhiteSpace(tag.Album))
				Album = tag.Album;
			if (!string.IsNullOrWhiteSpace(tag.Title))
				Title = tag.Title;

			Track = tag.Track;
			Year = tag.Year;
			Genres = tag.Genres;
			Lyrics = tag.Lyrics;
		}
	}
}
