using System.Windows;

namespace EPlayer.Extensions
{
	public static class EventExtension
	{
		/// <summary>
		/// Returns a new instance of RoutedEventArgs using the provided <paramref name="routedEvent"/>
		/// </summary>
		/// <param name="routedEvent">Target routed event</param>
		/// <returns>New instance of RoutedEventArgs</returns>
		public static RoutedEventArgs GetArgs(this RoutedEvent routedEvent) => new RoutedEventArgs(routedEvent);

		/// <summary>
		/// Returns new instance of RoutedPropertyChangedEventArgs of <typeparamref name="T"/> with values provided
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="routedEvent">Target RoutedEvent</param>
		/// <param name="oldValue">The old value of associated event</param>
		/// <param name="newValue">The new value</param>
		/// <returns>RoutedPropertyChangedEventArgs of <typeparamref name="T"/></returns>
		public static RoutedPropertyChangedEventArgs<T> WithArgs<T>(this RoutedEvent routedEvent, T oldValue, T newValue) => new RoutedPropertyChangedEventArgs<T>(oldValue, newValue, routedEvent);

		/// <summary>
		/// Invokes the <paramref name="handler"/> with parameter <paramref name="parameter"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="handler"></param>
		/// <param name="parameter"></param>
		/// <param name="sender"></param>
		public static void Invoke<T>(this TypedEventHandler<T> handler, T parameter, object sender = default) => handler?.Invoke(sender, parameter);

		public static void Raise(this FrameworkElement element, RoutedEvent ev)
		{
			element.RaiseEvent(new RoutedEventArgs(ev, element));
		}
	}
}
