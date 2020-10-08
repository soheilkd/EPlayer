using System;
using System.Windows.Shell;
using EPlayer.Controls;
using EPlayer.Extensions;
using EPlayer.Imaging;
using EPlayer.Media;
using NAudio.Wave;

namespace EPlayer.Taskbar
{
	public class Controller
	{
		private readonly TaskbarItemInfo TaskbarInfo;

		#region Events
		public event EventHandler PlayRequested;
		public event EventHandler PauseRequested;
		public event EventHandler PreviousRequested;
		public event EventHandler NextRequested;
		#endregion

		#region Thumbs
		private readonly ThumbButtonInfo _PlayThumb = new ThumbButtonInfo()
		{
			Description = "Play",
			ImageSource = ControlRenderer.RenderIcon(SegoeIcon.Play)
		};
		private readonly ThumbButtonInfo _PauseThumb = new ThumbButtonInfo()
		{
			Description = "Pause",
			ImageSource = ControlRenderer.RenderIcon(SegoeIcon.Pause)
		};
		private readonly ThumbButtonInfo _PreviousThumb = new ThumbButtonInfo()
		{
			Description = "Previous",
			ImageSource = ControlRenderer.RenderIcon(SegoeIcon.Previous)
		};
		private readonly ThumbButtonInfo _NextThumb = new ThumbButtonInfo()
		{
			Description = "Next",
			ImageSource = ControlRenderer.RenderIcon(SegoeIcon.Next)
		};
		#endregion

		public Controller(TaskbarItemInfo info)
		{
			_PlayThumb.Command = new Command(PlayRequested);
			_PauseThumb.Command = new Command(PauseRequested);
			_PreviousThumb.Command = new Command(PreviousRequested);
			_NextThumb.Command = new Command(NextRequested);
			info.ThumbButtonInfos.Add(_PreviousThumb);
			info.ThumbButtonInfos.Add(_PlayThumb);
			info.ThumbButtonInfos.Add(_NextThumb);
			TaskbarInfo = info;
		}

		public void SetPlayingStateOnThumb(bool isPlaying)
		{
			//TODO:FIX BUG
		//	TaskbarInfo.ThumbButtonInfos[1] = isPlaying ? _PauseThumb : _PlayThumb;
		}

		public void BindMusicPlayer(MusicPlayer player)
		{
			PlayRequested += (_, __) => player.Play();
			PauseRequested += (_, __) => player.Pause();
			NextRequested += (_, __) => player.Next();
			PreviousRequested += (_, __) => player.Previous();
			player.PlaybackStateChanged += (_, e) => SetPlayingStateOnThumb(e.Parameter == PlaybackState.Playing);
		}
	}
}
