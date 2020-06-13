using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace EPlayer.Controls
{
	//TODO CLEEEAAAN
	//TODO REFAAAACTOR
	internal static class CustomControlHelper
	{

		public static DependencyProperty RegisterProperty<T>(string name, string onChangeCallbackName, T defaultValue = default)
		{
			Type elementType = GetTypeOfCaller();
			PropertyMetadata metadata = GetMetadata(elementType, onChangeCallbackName, defaultValue);
			return DependencyProperty.Register(name, typeof(T), elementType, metadata);
		}

		public static DependencyProperty RegisterProperty<T>(string name, T defaultValue = default)
		{
			Type elementType = GetTypeOfCaller();
			var metadata = new PropertyMetadata(defaultValue);
			return DependencyProperty.Register(name, typeof(T), elementType, metadata);
		}

		public static RoutedEvent RegisterEvent(string name)
		{
			Type elementType = GetTypeOfCaller();
			return EventManager.RegisterRoutedEvent(name, RoutingStrategy.Direct, typeof(RoutedEventHandler), elementType);
		}


		public static RoutedEvent RegisterChangedEvent<T>(string name)
		{
			Type elementType = GetTypeOfCaller();
			return EventManager.RegisterRoutedEvent(name, RoutingStrategy.Direct, typeof(RoutedPropertyChangedEventHandler<T>), elementType);
		}

		private static PropertyMetadata GetMetadata<T>(Type elementType, string onChangeCallbackName, T defaultValue)
		{
			PropertyChangedCallback callback = GetPropertyChangedCallback<T>(elementType, onChangeCallbackName);
			return new PropertyMetadata(defaultValue, callback);
		}

		private static PropertyChangedCallback GetPropertyChangedCallback<T>(Type elementType, string onChangeCallbackName) => (obj, args) =>
																																					  {
																																						  if (args.NewValue is T newValue)
																																						  {
																																							  InvokeMethodIn(obj, elementType, onChangeCallbackName, newValue);
																																						  }
																																					  };

		private static void InvokeMethodIn(object obj, Type objectType, string methodName, params object[] parameters)
		{
			//Flags is to get private/internal/protected methods as well as public ones
			BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
			MethodInfo method = objectType.GetMethod(methodName, flags);
			method?.Invoke(obj, parameters);
		}

		public static void OverrideDefault(this DependencyProperty property, object newDefaultValue, bool inherits = true)
		{
			Type elementType = GetTypeOfCaller();
			property.OverrideMetadata(
				elementType,
				new FrameworkPropertyMetadata(newDefaultValue) { Inherits = inherits });
		}
		private static Type GetTypeOfCaller()
		{
			//Need to skip to frames: One is calling this method. Second is the method calling this
			//By doing so, frame gets the right stack of caller. Which would then give the right DeclaringType
			var frame = new StackFrame(2);
			MethodBase method = frame.GetMethod();
			return method.DeclaringType;
		}
	}
}
