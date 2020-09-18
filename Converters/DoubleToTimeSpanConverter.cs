using System;

namespace EPlayer.Converters
{
	class DoubleToTimeSpanConverter : ValueConverter<double, TimeSpan, object>
	{
		public override TimeSpan Convert(double value, object parameter) => TimeSpan.FromSeconds(value);
		public override double ConvertBack(TimeSpan value, object parameter) => value.TotalSeconds;
	}
}
