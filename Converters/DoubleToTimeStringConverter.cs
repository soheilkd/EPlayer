using System;
using System.Diagnostics;
using EPlayer.Extensions;

namespace EPlayer.Converters
{
	public class DoubleToTimeStringConverter : ValueConverter<double, string, object>
	{
		public override string Convert(double value, object parameter)
		{
			Debug.WriteLine("BAAAM");
			return TimeSpan.FromSeconds(value).FormatToString();
		}
		public override double ConvertBack(string value, object parameter)
		{
			Debug.WriteLine("BOOOOM");
			return double.Parse(value);
		}
	}
}
