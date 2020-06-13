using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;
using System.Windows.Threading;
using EPlayer.Media;
using EPlayer.Models;

namespace EPlayer.Windows
{
	//TODO: Clean
	public partial class MainWindow : Window
	{
		public static event TypedEventHandler<Song> NewSongAdded;
		public static Dispatcher PublicDispatcher;
		public MainWindow()
		{
			InitializeComponent();

			KeyboardHook.KeyUp += KeyboardHook_KeyUp;
			App.NewInstanceRequested += App_NewInstanceRequested;
			App.MusicPlayer.MediaChanged += (_, e) => Title = $"Elephant Player | {e.Parameter.Title}";

			Drop += MainWindow_Drop;
			PublicDispatcher = Dispatcher;
			NewSongAdded += (_, e) => App.MusicPlayer.Play(e);
		}

		private void KeyboardHook_KeyUp(object sender, TypedEventArgs<Key> e)
		{
			//Key shortcuts whether window is active or main key is down
			if (IsActive || e == Key.LeftShift)
			{
				//if (e == Key.Left) Player.SlidePosition(FlowDirection.RightToLeft);
				//if (e == Key.Right) App.MusicPlayer.SlidePosition(FlowDirection.LeftToRight);
			}
			//Key shortcuts always invokable
			//if (key == Key.MediaPlayPause) App.MusicPlayer.Pause(); //TODO: Implement
			if (e == Key.MediaNextTrack) App.MusicPlayer.Next();
			if (e == Key.MediaPreviousTrack) App.MusicPlayer.Previous();
		}
		private void App_NewInstanceRequested(object sender, TypedEventArgs<string[]> e) => Library.ReadNewData(e.Parameter);

		private void MainWindow_Drop(object sender, DragEventArgs e)
		{
			var data = e.Data.GetData(DataFormats.FileDrop) as string[];
			Library.ReadNewData(data);
		}

		public static void InvokeNewSongAdded(Song song) => NewSongAdded?.Invoke(null, song);

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TaskbarItemInfo = new TaskbarItemInfo();
			Task.Run(() =>
			Library.Load());
			//LoadContents();
			StateChanged += MainWindowStateChangeRaised;

		}

		#region Minimize, maximize, restore and close buttons
		private void CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
		private void Executed_Minimize(object sender, ExecutedRoutedEventArgs e) => SystemCommands.MinimizeWindow(this);
		private void Executed_Maximize(object sender, ExecutedRoutedEventArgs e) => SystemCommands.MaximizeWindow(this);
		private void Executed_Restore(object sender, ExecutedRoutedEventArgs e) => SystemCommands.RestoreWindow(this);
		private void Executed_Close(object sender, ExecutedRoutedEventArgs e) => SystemCommands.CloseWindow(this);

		private void MainWindowStateChangeRaised(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				MainWindowBorder.BorderThickness = new Thickness(8);
				RestoreButton.Visibility = Visibility.Visible;
				MaximizeButton.Visibility = Visibility.Collapsed;
			}
			else
			{
				MainWindowBorder.BorderThickness = new Thickness(0);
				RestoreButton.Visibility = Visibility.Collapsed;
				MaximizeButton.Visibility = Visibility.Visible;
			}
		}
		#endregion
	}
}