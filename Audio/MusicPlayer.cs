using System;
using System.Threading;
using EPlayer.Audio;
using EPlayer.Extensions;
using EPlayer.Models;
using NAudio.Wave;

namespace EPlayer.Media
{
	public class MusicPlayer
	{
		public AudioPlayer AudioPlayer { get; } = new AudioPlayer();

		public event TypedEventHandler<Song> SongChanged;
		public event TypedEventHandler<SongQueue> QueueChanged;

		public event TypedEventHandler<PlaybackState> PlaybackStateChanged
		{
			add => AudioPlayer.PlaybackStateChanged += value;
			remove => AudioPlayer.PlaybackStateChanged -= value;
		}

		private SongQueue queue = new SongQueue();
		public SongQueue Queue
		{
			get => queue;
			set
			{
				if (queue != value)
				{
					queue = value;
					QueueChanged.Invoke(value);
				}
			}
		}
		public Song Current => Queue.Current;

		public PlaybackState PlaybackState => AudioPlayer.PlaybackState;

		public TimeSpan Position
		{
			get => AudioPlayer.Position;
			set => AudioPlayer.Position = value;
		}

		public TimeSpan Length => AudioPlayer.Length;

		public float Volume
		{
			get => AudioPlayer.Volume;
			set => AudioPlayer.Volume = value;
		}

		public MusicPlayer()
		{

		}

		public void Next() => Play(Queue.Next());

		public void Previous() => Play(Queue.Previous());

		public void Play(Song song, SongQueue queue = default)
		{
			AudioPlayer.Open(song.FilePath);
			SongChanged.Invoke(song);
			UpdateQueue(song, queue);
			song.Plays.Add(DateTime.Now);
		}


		public void Pause() => AudioPlayer.Pause();
		public void Play() => AudioPlayer.Play(); 
		public void Stop() => AudioPlayer.Stop();

		private void UpdateQueue(Song song, SongQueue queue)
		{
			if (queue == null)
				queue = Queue;
			if (queue.Contains(song))
			{
				queue.Position = queue.IndexOf(song);
			}
			else
			{
				queue.Add(song);
			}

			Queue = queue;
		}
	}
}