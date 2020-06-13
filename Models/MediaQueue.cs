using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EPlayer.Models
{
	[Serializable]
	public class MediaQueue<TMedia> : ObservableCollection<TMedia> where TMedia : MediaItem
	{
		private int _Position;
		public TMedia Current => Position < Count ? this[Position] : default;
		public int Position
		{
			get => _Position;
			set
			{
				if (value >= Count)
				{
					value = 0;
				}

				_Position = value;
				RefreshIsPlayings();
			}
		}

		public MediaQueue(IEnumerable<TMedia> collection) : base(collection) { }
		public MediaQueue() : base() { }

		public void RefreshIsPlayings()
		{
			for (var i = 0; i < Count; i++)
			{
				this[i].IsPlaying = false;
			}

			Current.IsPlaying = true;
		}

		public bool MoveNext()
		{
			if (++Position >= Count)
			{
				Position = 0;
			}

			return true;
		}
		public bool MovePrevious()
		{
			if (Position == 0)
			{
				Position = Count - 1;
			}
			else
			{
				Position--;
			}

			return true;
		}

		public TMedia Next()
		{
			MoveNext();
			return Current;
		}
		public TMedia Previous()
		{
			MovePrevious();
			return Current;
		}

		public void Reset() => Position = 0;

		public void Dispose() => Clear();

		public MediaQueue<TMedia> Search(string query)
		{
			if (!string.IsNullOrWhiteSpace(query))
			{
				IEnumerable<TMedia> col = this.Where(item => item.MatchesQuery(query));
				if (col.Count() != 0)
				{
					return new MediaQueue<TMedia>(col);
				}
			}
			return this;
		}

		public bool Contains(string path, out MediaItem output)
		{
			output = this.Where(item => item.FilePath == path).FirstOrDefault();
			return output != default;
		}
		public bool Contains(string path) => this.Where(item => item.FilePath == path).Count() != 0;
	}

	public class MediaQueue : MediaQueue<MediaItem>
	{
		public MediaQueue(IEnumerable<MediaItem> collection) : base(collection) { }
		public MediaQueue() : base() { }

		public static implicit operator MediaQueue(MediaQueue<Song> v)
		{
			//TODO: Need a better solution here
			return new MediaQueue(v);
		}

		public static implicit operator MediaQueue(MediaQueue<Video> v)
		{
			//TODO: Need a better solution here
			return new MediaQueue(v);
		}
	}
}