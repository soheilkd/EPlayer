using TagLib;

namespace EPlayer.Models
{
	public static class TagHelper
	{
		public static byte[] GetBytesFromTagPicture(IPicture picture)
		{
			var bytes = new byte[picture?.Data.Count ?? 0];
			picture.Data.CopyTo(bytes, 0);
			return bytes;
		}
	}
}
