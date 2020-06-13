using System;
using System.Windows.Controls;
using EPlayer.Extensions;
using EPlayer.Models;
using LibVLCSharp.Shared;
using VlcMedia = LibVLCSharp.Shared.Media;

namespace EPlayer.Media
{
	public class MusicPlayer
	{
		public event TypedEventHandler<Song> MediaChanged;
		public event TypedEventHandler<MediaQueue<Song>> QueueChanged;
		public event TypedEventHandler<Song> MediaFailed;
		public event TypedEventHandler<MediaState> MediaStateChanged;

		private LibVLC lazyLibVLC;
		private LibVLC LibVlc
		{
			get
			{
				if (lazyLibVLC == null)
				{
					Core.Initialize();
					lazyLibVLC = new LibVLC();
				}
				return lazyLibVLC;
			}
		}
		private MediaPlayer lazyPlayer;
		public MediaPlayer Player
		{
			get
			{
				if (lazyPlayer == null)
					lazyPlayer = new MediaPlayer(LibVlc);
				return lazyPlayer;
			}
		}

		private MediaQueue<Song> queue = new MediaQueue<Song>();
		public MediaQueue<Song> Queue
		{
			get => queue;
			set
			{
				if (queue != value)
				{
					queue = value;
					QueueChanged?.Invoke(queue);
				}
			}
		}
		public Song Current => Queue.Current;

		public MusicPlayer()
		{

		}

		public void Next() => Play(Queue.Next());

		public void Previous() => Play(Queue.Previous());

		public void Play(Song song, MediaQueue<Song> queue = default)
		{
			var media = new VlcMedia(LibVlc, new Uri(song.FilePath));
			Player.Play(media);
			UpdateQueue(song, queue);
			MediaStateChanged.Invoke(MediaState.Play);
		}
		public void Pause()
		{
			Player.Pause();
			MediaStateChanged?.Invoke(MediaState.Pause);
		}

		private void UpdateQueue(Song song, MediaQueue<Song> queue)
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
