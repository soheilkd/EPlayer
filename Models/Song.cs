using System;
using System.Linq;
using System.Windows.Media;
using EPlayer.Extensions;
using TagLib;

namespace EPlayer.Models
{
	[Serializable]
	public class Song : MediaItem
	{
		public static readonly string[] ValidFormats = new[]
		{
			".mp3",
			".wav",
			".aiff",
			".aac",
			".ogg",
			".wma",
			".flac",
			".alac"
		};


		public string Artist { get; set; } = "Unkown Artist";
		public string Album { get; set; } = "Unknown Album";
		public string AlbumArtist { get; set; } = "Unknown Artist";
		public uint Track { get; set; } = 0;
		public uint Year { get; set; } = 0;
		public string[] Genres { get; set; } = Array.Empty<string>();
		public string Lyrics { get; set; } = string.Empty;

		public Song() : base() { }
		public Song(string path) : base(path) { }

		public bool IsIn(Album album) => album.Songs.Contains(this);
		public bool DoesPairWith(Album album) => album.Name == Album && album.Artist == AlbumArtist;

		protected override void ReadInfoFromFile(File file)
		{
			Tag tag = file.Tag;

			if (!string.IsNullOrWhiteSpace(tag.FirstPerformer))
			{
				Artist = tag.FirstPerformer;
			}

			if (!string.IsNullOrWhiteSpace(tag.FirstAlbumArtist))
			{
				AlbumArtist = tag.FirstAlbumArtist;
			}

			if (!string.IsNullOrWhiteSpace(tag.Album))
			{
				Album = tag.Album;
			}

			if (!string.IsNullOrWhiteSpace(tag.Title))
			{
				Title = tag.Title;
			}
			else
			{
				Title = FileTitle;
			}

			Track = tag.Track;
			Year = tag.Year;
			Genres = tag.Genres;
			Lyrics = tag.Lyrics;
		}

		public ImageSource GetImage()
		{
			try
			{
				using var file = File.Create(FilePath);
				var picsToByte = file.Tag.Pictures.FirstOrDefault().Data.ToArray();
				return picsToByte.GetImageSource();
			}
			catch (Exception) { return default; }
		}
	}
}
