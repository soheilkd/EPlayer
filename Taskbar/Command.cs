using System;
using System.Windows.Input;
#pragma warning disable CS0067 //Suppres never used warning

namespace EPlayer.Taskbar
{
	public class Command : ICommand
	{
		public Command(EventHandler handler) : this()
		{
			Raised = handler;
		}

		public Command() : base() { }

		public event EventHandler CanExecuteChanged;
		public event EventHandler Raised;
		public bool CanExecute(object parameter) => true;

		public void Execute(object parameter) => Raised?.Invoke(this, null);
	}
}
