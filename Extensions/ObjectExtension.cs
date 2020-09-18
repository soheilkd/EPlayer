using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPlayer.Extensions
{
	public static class ObjectExtension
	{
		public static string FormatToString(this TimeSpan span)
		{
			return string.Format("{0:mm\\:ss}", span);
		}

		public static T GetResult<T>(this Task<T> task)
		{
			task.Start();
			task.Wait();
			return task.Result;
		}
	}
}
