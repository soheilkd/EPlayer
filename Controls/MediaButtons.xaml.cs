using System.Windows;
using System.Windows.Controls;
using EPlayer.Extensions;
using static EPlayer.Controls.CustomControlHelper;

namespace EPlayer.Controls
{
	public partial class MediaButtons : UserControl
	{
		public static readonly RoutedEvent NextEvent = RegisterEvent(nameof(Next));
		public static readonly RoutedEvent PreviousEvent = RegisterEvent(nameof(Previous));
		public static readonly RoutedEvent PlayEvent = RegisterEvent(nameof(Play));
		public static readonly RoutedEvent PauseEvent = RegisterEvent(nameof(Pause));
		public event RoutedEventHandler Next
		{
			add => AddHandler(NextEvent, value); 
			remove => RemoveHandler(NextEvent, value);
		}
		public event RoutedEventHandler Previous
		{
			add => AddHandler(PreviousEvent, value);
			remove => RemoveHandler(PreviousEvent, value);
		}
		public event RoutedEventHandler Play
		{
			add => AddHandler(PlayEvent, value);
			remove => RemoveHandler(PlayEvent, value);
		}
		public event RoutedEventHandler Pause
		{
			add => AddHandler(PauseEvent, value);
			remove => RemoveHandler(PauseEvent, value);
		}

		public bool IsPlaying
		{
			get => PlayPauseButton.Icon == SegoeIcon.Pause;
			set => PlayPauseButton.Icon = value ? SegoeIcon.Pause : SegoeIcon.Play;
		}
		public MediaButtons()
		{
			InitializeComponent();
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			this.Raise(NextEvent);
		}

		private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
		{
			IsPlaying = !IsPlaying;
			this.Raise(IsPlaying ? PlayEvent : PauseEvent);
		}

		private void PreviousButton_Click(object sender, RoutedEventArgs e)
		{
			this.Raise(PreviousEvent);
		}
	}
}
