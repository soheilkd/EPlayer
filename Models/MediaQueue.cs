using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EPlayer.Models
{
	[Serializable]
	public class SongQueue : ObservableCollection<Song>
	{
		private int _Position;
		public Song Current => Position < Count ? this[Position] : default;
		public int Position
		{
			get => _Position;
			set
			{
				if (value >= Count)
					throw new ArgumentOutOfRangeException("Position is out of index/total number of items in queue");

				_Position = value;
				RefreshIsPlayings();
			}
		}

		public SongQueue(IEnumerable<Song> collection) : base(collection) { }
		public SongQueue() : base() { }

		public void RefreshIsPlayings()
		{
			for (var i = 0; i < Count; i++)
				this[i].IsPlaying = false;

			Current.IsPlaying = true;
		}

		public bool MoveNext()
		{
			if (++Position >= Count)
				Position = 0;

			return true;
		}
		public bool MovePrevious()
		{
			if (Position == 0)
				Position = Count - 1;
			else
				Position--;

			return true;
		}

		public Song Next()
		{
			MoveNext();
			return Current;
		}
		public Song Previous()
		{
			MovePrevious();
			return Current;
		}
	}
}