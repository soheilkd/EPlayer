using System;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using EPlayer.Extensions;
using NAudio.Utils;
using NAudio.Wave;

namespace EPlayer.Audio
{
	//Wrapper for NAudio
	public class AudioPlayer
	{
		private WaveOutEvent WaveOut;
		private AudioFileReader Reader;
		private BackgroundWorker BackgroundWorker = new BackgroundWorker()
		{
			WorkerSupportsCancellation = true,
			WorkerReportsProgress = false
		};
		private bool BackgroundCancelRequested = false;

		public event EventHandler<StoppedEventArgs> Stopped
		{
			add => WaveOut.PlaybackStopped += value;
			remove => WaveOut.PlaybackStopped -= value;
		}
		public event TypedEventHandler<PlaybackState> PlaybackStateChanged;
		public event TypedEventHandler<string> MediaChanged;

		public PlaybackState PlaybackState => WaveOut.PlaybackState;
		public TimeSpan Position
		{
			get
			{
				try
				{
					if (WaveOut == null)
						return TimeSpan.Zero;
					return WaveOut.GetPositionTimeSpan();
				}
				catch (Exception)
				{
					return TimeSpan.Zero;
				}
			}
			set
			{
				if (Reader == null)
					return;
				var rate = Reader.Position / Position.TotalMilliseconds;
				Reader.Position = (long)(value.TotalMilliseconds * rate);
			}
		}
		public TimeSpan Length { get; private set; } = TimeSpan.Zero;
		public float Volume
		{
			get => WaveOut.Volume;
			set => WaveOut.Volume = value;
		}

		public AudioPlayer()
		{
			BackgroundWorker.DoWork += BackgroundWorker_DoWork;
			BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			OpenByBackground(e.Argument);
		}
		private void InitiateWaveOut(AudioFileReader reader)
		{
			WaveOut = new WaveOutEvent();
			WaveOut.PlaybackStopped += (_, e) =>  PlaybackStateChanged.Invoke(PlaybackState.Stopped);
			WaveOut.Init(reader);
		}
		private void OpenByBackground(object filePath)
		{
			Reader = new AudioFileReader(filePath.ToString());
			InitiateWaveOut(Reader);
			Length = Reader.TotalTime;
			Play();
			Stopped += delegate
			{
				WaveOut.Dispose();
				Reader.Dispose();
			};
			MediaChanged.Invoke(filePath.ToString());
		}
		public void Open(string filePath)
		{
			if (WaveOut != null && PlaybackState != PlaybackState.Stopped)
			{
				Stop();
			}
			if (!BackgroundWorker.IsBusy)
				BackgroundWorker.RunWorkerAsync(filePath);
		}

		public void Play()
		{
			WaveOut.Play();
			PlaybackStateChanged.Invoke(PlaybackState.Playing);
		}
		public void Pause()
		{
			WaveOut.Pause();
			PlaybackStateChanged.Invoke(PlaybackState.Paused);
		}
		public void Stop()
		{
			WaveOut.Stop();
			PlaybackStateChanged.Invoke(PlaybackState.Stopped);
			WaveOut.Dispose();
			Reader.Dispose();
		}
	}
}
