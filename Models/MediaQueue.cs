using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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
					throw new ArgumentOutOfRangeException(nameof(value));

				_Position = value;
			}
		}

		public SongQueue(IEnumerable<Song> collection) : base(collection) { }
		public SongQueue() : base() { }

		public void MoveNext()
		{
			if (++Position >= Count)
				Position = 0;
		}
		public void MovePrevious()
		{
			if (Position == 0)
				Position = Count - 1;
			else
				Position--;
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