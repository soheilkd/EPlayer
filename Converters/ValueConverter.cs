using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EPlayer.Converters
{
	public abstract class ValueConverter<T, U, TParam> : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is T tValue && parameter is TParam param)
			{
				if (targetType != typeof(U))
					throw new ArgumentException(nameof(targetType));
				else
					return Convert(tValue, param);
			}
			else
				throw new ArgumentException($"{nameof(value)} or {nameof(parameter)}");
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is U uValue && parameter is TParam param)
			{
				if (targetType != typeof(T))
					throw new ArgumentException(nameof(targetType));
				else
					return ConvertBack(uValue, param);
			}
			else
				throw new ArgumentException($"{nameof(value)} or {nameof(parameter)}");
		}

		public abstract U Convert(T value, TParam parameter);
		public abstract T ConvertBack(U value, TParam parameter);
	}
}
