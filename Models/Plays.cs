using System;
using System.Collections.Generic;
using System.Linq;

namespace EPlayer.Models
{
	public class Plays : List<DateTime>
	{
		public static readonly Plays Empty = new Plays();
		public Plays(IEnumerable<DateTime> dates) : base(dates) { }
		private Plays() : base() { }
		public void AddCount() => Add(DateTime.Now);

		public static Plays Create() => new Plays(new[] { DateTime.Now });
	}

	public static class PlaysExtensions
	{
		public static IEnumerable<DateTime> Since(this IEnumerable<DateTime> dates, DateTime date) => dates.Where(each => each > date);

		public static IEnumerable<DateTime> Till(this IEnumerable<DateTime> dates, DateTime date) => dates.Where(each => each < date);
	}
}