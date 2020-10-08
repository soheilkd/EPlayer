using System;
using System.IO;

namespace EPlayer.Serialization
{
	public static class PathHelper
	{
		private static string appdataPath;
		public static string AppdataPath
		{
			get
			{
				if (appdataPath == null)
				{
					appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					if (!appdataPath.EndsWith("\\")) appdataPath += "\\";
					appdataPath += "EPlayer\\";
					if (!Directory.Exists(appdataPath))
						Directory.CreateDirectory(appdataPath);
				}
				return appdataPath;
			}
		}
	}
}
