using System.Linq;
using System.Runtime;
using System.Windows;
using EPlayer.Library;
using EPlayer.Media;
using EPlayer.Models;
using SingleInstanceCore;

namespace EPlayer
{
	public partial class App : Application, ISingleInstance
	{
		public static readonly string Path = @"C:\Program Files\soheilkd\EPlayer\";
		public static event TypedEventHandler<string[]> InstanceInvoked;

		public static MusicPlayer MusicPlayer { get; } = new MusicPlayer();
		public static MusicLibrary MusicLibrary { get; } = new MusicLibrary();

		public App()
		{
			ProfileOptimization.SetProfileRoot(Path);
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
