using System;
using System.Windows.Shell;

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
			Description = "Play"
			//ImageSource = IconProvider.GetBitmap(IconType.Play)
		};
		private readonly ThumbButtonInfo _PauseThumb = new ThumbButtonInfo()
		{
			Description = "Pause"
			//ImageSource = IconProvider.GetBitmap(IconType.Pause)
		};
		private readonly ThumbButtonInfo _PreviousThumb = new ThumbButtonInfo()
		{
			Description = "Previous"
			//ImageSource = IconProvider.GetBitmap(IconType.Previous)
		};
		private readonly ThumbButtonInfo _NextThumb = new ThumbButtonInfo()
		{
			Description = "Next"
			//ImageSource = IconProvider.GetBitmap(IconType.Next)
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

		public void SetPlayingStateOnThumb(bool isPlaying) => TaskbarInfo.ThumbButtonInfos[1] = isPlaying ? _PauseThumb : _PlayThumb;
	}
}
