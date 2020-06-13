using System.Linq;
using System.Runtime;
using System.Windows;
using EPlayer.Media;
using EPlayer.Models;
using SingleInstanceCore;

namespace EPlayer
{
	public partial class App : Application, ISingleInstance
	{
		public static readonly string Path = @"C:\Program Files\soheilkd\EPlayer\";

		public static event TypedEventHandler<string[]> NewInstanceRequested;
		public static MusicPlayer MusicPlayer { get; } = new MusicPlayer();
		public static ImageCache Metadata { get; set; } = new ImageCache();

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
			SingleInstance<App>.Cleanup();
			KeyboardHook.Unhook();
			Library.Save();
		}

		public void OnInstanceInvoked(string[] args) => NewInstanceRequested?.Invoke(this, args.ToArray());
	}
}
