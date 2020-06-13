using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EPlayer.Serialization
{
	public static class Binary
	{
		private static readonly BinaryFormatter Formatter = new BinaryFormatter();

		public static void Serialize(object obj, string path)
		{
			using var fileStream = new FileStream(path, FileMode.Create);
			Formatter.Serialize(fileStream, obj);
		}
		public static void Serialize(object obj, Stream stream)
		{
			using (stream)
			{
				Formatter.Serialize(stream, obj);
			}
		}

		public static bool TryDeserialize<T>(string path, out T result)
		{
			try
			{
				using var fileStream = new FileStream(path, FileMode.Open);
				result = Deserialize<T>(fileStream);
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}
		public static bool TryDeserialize<T>(Stream stream, out T result)
		{
			try
			{
				using (stream)
				{
					result = Deserialize<T>(stream);
				}

				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		public static T Deserialize<T>(string path)
		{
			if (!File.Exists(path))
			{
				Serialize(Activator.CreateInstance<T>(), path);
			}

			using var stream = new FileStream(path, FileMode.Open);
			return (T)Formatter.Deserialize(stream);
		}
		public static T Deserialize<T>(Stream stream)
		{
			using (stream)
			{
				return (T)Formatter.Deserialize(stream);
			}
		}

		public static Lazy<T> LazyDeserialize<T>(string path)
		{
			if (!File.Exists(path))
			{
				Serialize(Activator.CreateInstance<T>(), path);
			}

			using var fileStream = new FileStream(path, FileMode.Open);
			return new Lazy<T>(() => Deserialize<T>(fileStream));
		}
		public static Lazy<T> LazyDeserialize<T>(Stream stream) => new Lazy<T>(delegate
																			 {
																				 using (stream)
																				 {
																					 return Deserialize<T>(stream);
																				 }
																			 });
	}
}
