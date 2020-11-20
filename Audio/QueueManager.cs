using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPlayer.Models;

namespace EPlayer.Audio
{
	public class QueueManager
	{
		public List<Song> Songs { get; } = new List<Song>();
		public void UpdateIsPlayings()
		{
			for (var i = 0; i < Songs.Count; i++)
				Songs[i].IsPlaying = false;

			Songs[0].IsPlaying = true;
		}
	}
}
