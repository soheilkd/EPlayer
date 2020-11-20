using System.Linq;
using System.Runtime;
using System.Windows;
using EPlayer.Media;
using EPlayer.Models;
using EPlayer.MusicLibrary;
using EPlayer.Serialization;
using SingleInstanceCore;

namespace EPlayer
{
	public partial class App : Application, ISingleInstance
	{
		public static event TypedEventHandler<string[]> InstanceInvoked;

		public static MusicPlayer MusicPlayer { get; } = new MusicPlayer();
		public static Library MusicLibrary { get; } = new Library();

		public App()
		{
			ProfileOptimization.SetProfileRoot(PathHelper.AppdataPath);
			ProfileOptimization.StartProfile("RuntimeProfile.jit");
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			if (!SingleInstance<App>.InitializeAsFirstInstance("soheilkd_EPlayerIPC"))
				Current.Shutdown();
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			MusicPlayer.Stop();
			SingleInstance<App>.Cleanup();
			KeyboardHook.Unhook();
			MusicLibrary.Save();
		}

		public void OnInstanceInvoked(string[] args)
		{
			var playSong = new TypedEventHandler<Song>((_, e) => MusicPlayer.Play(e));
			MusicLibrary.NewSongAdded += playSong;
			MusicLibrary.ReadNewData(args);
			MusicLibrary.NewSongAdded -= playSong;

			InstanceInvoked?.Invoke(this, args);
		}
	}
}
