namespace EPlayer
{
	public class TypedEventArgs<T> : System.EventArgs
	{
		public T Parameter { get; set; }

		public TypedEventArgs() { }
		public TypedEventArgs(T para)
		{
			Parameter = para;
		}

		public static implicit operator TypedEventArgs<T>(T obj)
		{
			return new TypedEventArgs<T>(obj);
		}

		public static implicit operator T(TypedEventArgs<T> info)
		{
			return info.Parameter;
		}
	}
}