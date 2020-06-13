using System;
using System.Collections.Generic;

using EPlayer.Serialization;

namespace EPlayer.Models
{
	[Serializable]
	public class ImageCache : LoadableObject<ImageCache>
	{
		public Dictionary<string, byte[]> ArtistImages { get; private set; } = new Dictionary<string, byte[]>();
		public Dictionary<string, byte[]> AlbumImages { get; private set; } = new Dictionary<string, byte[]>();
	}
}
