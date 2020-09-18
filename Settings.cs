using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using EPlayer.Models;

namespace EPlayer
{
	public static class Settings
	{
		public static double Volume 
		{
			get => double.Parse(Get());
			set => Set(value); 
		}
		public static string LastPath
		{ 
			get => Get(); 
			set => Set(value);
		}
		public static Size LastSize 
		{ 
			get => Size.Parse(Get()); 
			set => Set(value); 
		}
		public static PlayMode PlayMode
		{
			get => Enum.Parse<PlayMode>(Get());
			set => Set(value); 
		}
		public static IEnumerable<string> ScanFolders 
		{ 
			get => Get().Split("~~".ToCharArray()).AsEnumerable(); 
			set => Set(string.Join("~~", value)); 
		}
		public static int MediaDeviceIndex
		{
			get => int.Parse(Get());
			set => Set(value);
		}

		private static string Get([CallerMemberName] string name = "")
		{
			return ConfigurationManager.AppSettings.Get(name);
		}
		private static void Set<T>(T value, [CallerMemberName] string name = "")
		{
			ConfigurationManager.AppSettings.Set(name, value.ToString());
		}
	}
}
