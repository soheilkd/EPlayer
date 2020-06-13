using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TagLib;

namespace EPlayer.Models
{
	[Serializable]
	public abstract class MediaItem : INotifyPropertyChanged
	{
		#region Notify Property Change
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		protected void Set<T>(ref T target, T value, [CallerMemberName] string propertyName = default)
		{
			target = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		public string FilePath { get; private set; } = string.Empty;
		public DateTime AdditionDate { get; set; } = DateTime.Today;

		private List<DateTime> plays = new List<DateTime>();
		public List<DateTime> Plays { get => plays; set => Set(ref plays, value); }
		private string fileTitle = "Unknown";
		public string FileTitle { get => fileTitle; set => Set(ref fileTitle, value); }
		private string title = "Unknown";
		public string Title { get => title; set => Set(ref title, value); }
		private bool isPlaying = false;
		public bool IsPlaying { get => isPlaying; set => Set(ref isPlaying, value); }
		private TimeSpan duration = TimeSpan.Zero;
		public TimeSpan Duration { get => duration; set => Set(ref duration, value); }
		private bool possiblyCorrupt;
		public bool PossiblyCorrupt { get => possiblyCorrupt; set => Set(ref possiblyCorrupt, value); }

		protected MediaItem() { }

		protected MediaItem(string filePath) : base()
		{
			FilePath = filePath;
			var fileName = new System.IO.FileInfo(filePath).Name;
			FileTitle = fileName.Substring(0, fileName.LastIndexOf('.')); //Not to include file format in title
			try
			{
				var file = File.Create(filePath);
				if (file.Tag != null)
				{
					ReadInfoFromFile(file);
				}
				else
				{
					PossiblyCorrupt = true;
				}
			}
			catch (Exception)
			{
				PossiblyCorrupt = true;
			}
		}

		public virtual bool MatchesQuery(string query) => throw new NotImplementedException();

		protected virtual void ReadInfoFromFile(File file)
		{
			Title = file.Name;
			Duration = file.Properties.Duration;
		}
	}
}
