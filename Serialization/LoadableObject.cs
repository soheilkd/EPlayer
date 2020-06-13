using System;

namespace EPlayer.Serialization
{
	[Serializable]
	public class LoadableObject<T>
	{
		public static T LoadFrom(string path)
		{
			if (Binary.TryDeserialize<T>(path, out T obj))
			{
				return obj;
			}
			else
			{
				return Activator.CreateInstance<T>();
			}
		}
		public void SaveTo(string path) => Binary.Serialize(this, path);
	}
}
