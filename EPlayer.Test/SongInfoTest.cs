
using System;
using System.Linq;
using EPlayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPlayer.Test
{
	[TestClass]
	public class SongInfoTest
	{
		private const string SamplePath = "Sample.mp3";
		private const string CorruptSamplePath = "CorruptSample.mp3";

		[TestMethod]
		public void TestReadingSample()
		{
			var sample = new Song(SamplePath);

			Assert.IsFalse(sample.PossiblyCorrupt);
		}
		[TestMethod]
		public void TestReadingCorruptSample()
		{
			var corruptSample = new Song(CorruptSamplePath);

			Assert.IsTrue(corruptSample.PossiblyCorrupt);
		}

		[TestMethod]
		public void TestReadingArtist()
		{
			var sample = new Song(SamplePath);

			Assert.AreEqual("Gdaal", sample.Artist);
		}
		[TestMethod]
		public void TestReadingAlbumArtist()
		{
			var sample = new Song(SamplePath);

			Assert.AreEqual("Gdaal", sample.AlbumArtist);
		}
		[TestMethod]
		public void TestReadingAlbum()
		{
			var sample = new Song(SamplePath);

			Assert.AreEqual("Abr Haaye Noghrei Vol 1", sample.Album);
		}
		[TestMethod]
		public void TestReadingGenre()
		{
			var sample = new Song(SamplePath);

			Assert.AreEqual("Pop", sample.Genres.FirstOrDefault());
		}
		[TestMethod]
		public void TestReadingYear()
		{
			var sample = new Song(SamplePath);

			Assert.AreEqual((uint)2017, sample.Year);
		}
		[TestMethod]
		public void TestReadingTitle()
		{
			var sample = new Song(SamplePath);

			Assert.AreEqual("Hala (Ft Mahta)", sample.Title);
		}
		[TestMethod]
		public void TestDuration()
		{
			var sample = new Song(SamplePath);
			var expectedDuration = TimeSpan.FromSeconds(208);

			Assert.AreEqual(expectedDuration, sample.Duration);
		}
		[TestMethod]
		public void TestReadingFileTitle()
		{
			var sample = new Song(SamplePath);
			var corruptSample = new Song(CorruptSamplePath);

			Assert.AreEqual("Sample", sample.FileTitle);
			Assert.AreEqual("CorruptSample", corruptSample.FileTitle);
		}
		[TestMethod]
		public void TestAdditionDate()
		{
			var sample = new Song(SamplePath);
			var corruptSample = new Song(CorruptSamplePath);
			DateTime expectedAdditionDate = DateTime.Today;

			Assert.AreEqual(expectedAdditionDate, sample.AdditionDate);
			Assert.AreEqual(expectedAdditionDate, corruptSample.AdditionDate);
		}
	}
}
