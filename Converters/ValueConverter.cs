using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EPlayer.Converters
{
	public abstract class ValueConverter<T, U, TParam> : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return null;
			else if (value is T tValue)
			{
				if (targetType != typeof(U))
					throw new ArgumentException(nameof(targetType));
				else
					return Convert(tValue, TryGetParameter(parameter));
			}
			else
				throw new ArgumentException($"{nameof(value)} or {nameof(parameter)}");
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is U uValue)
			{
				if (targetType != typeof(T))
					throw new ArgumentException(nameof(targetType));
				else
					return ConvertBack(uValue, TryGetParameter(parameter));
			}
			else
				throw new ArgumentException($"{nameof(value)} or {nameof(parameter)}");
		}

		public TParam TryGetParameter(object parameter)
		{
			try
			{
				return (TParam)parameter;
			}
			catch (Exception)
			{
				return default;
			}
		}

		public abstract U Convert(T value, TParam parameter);
		public abstract T ConvertBack(U value, TParam parameter);
	}
}
