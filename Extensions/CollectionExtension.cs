using System.Collections;

namespace EPlayer.Extensions
{
	public static class CollectionExtension
	{
		public static T FirstOrDefault<T>(this IList list) where T : class
		{
			if (list == null || list.Count == 0)
			{
				return default;
			}
			else
			{
				return list[0] as T;
			}
		}
	}
}
