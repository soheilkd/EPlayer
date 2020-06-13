using System;
using TagLib;

namespace EPlayer.Models
{
	//TODO Implement reading bitrate resolution etc
	[Serializable]
	public class Video : MediaItem
	{
		public static readonly string[] ValidFormats = new[]
		{
			".webm",
			".mpg",
			".mp2",
			".mpeg",
			".mpe",
			".mpv",
			".mp4",
			".m4p",
			".m4v",
			".avi",
			".wmv",
			".mov",
			".qt",
			".flv",
			".swf",
			".avchd",
			".mkv"
		};

		public Video() : base() { }
		public Video(string path) : base(path) { }

		protected override void ReadInfoFromFile(File file) => base.ReadInfoFromFile(file);
	}
}
