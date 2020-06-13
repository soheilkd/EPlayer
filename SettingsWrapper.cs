using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

using EPlayer.Models;

namespace EPlayer
{
	public static class SettingsWrapper
	{
		public static double Volume { get => double.Parse(Get()); set => Set(value); }
		public static string LastPath { get => Get(); set => Set(value); }
		public static Size LastSize { get => Size.Parse(Get()); set => Set(value); }
		public static PlayMode PlayMode { get => (PlayMode)int.Parse(Get()); set => Set(value); }
		public static IEnumerable<string> ScanningPaths { get => Get().Split("~~".ToCharArray()).AsEnumerable(); set => Set(string.Join("~~", value)); }

		private static string Get([CallerMemberName] string name = "") => ConfigurationManager.AppSettings.Get(name);
		private static void Set<T>(T value, [CallerMemberName] string name = "") => ConfigurationManager.AppSettings.Set(name, value.ToString());
	}
}
