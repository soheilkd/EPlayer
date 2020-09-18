using TagLib;

namespace EPlayer.Extensions
{
	public static class TagExtension
	{
		public static byte[] GetBytes(this IPicture picture)
		{
			var bytes = new byte[picture?.Data.Count ?? 0];
			picture.Data.CopyTo(bytes, 0);
			return bytes;
		}
	}
}
