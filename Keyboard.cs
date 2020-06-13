using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using EPlayer.Extensions;

namespace EPlayer
{
	public static class KeyboardHook
	{
		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
		private static LowLevelKeyboardProc _proc = HookCallback;
		private static IntPtr _hookID = SetHook(_proc);

		public static event TypedEventHandler<Key> KeyDown;
		public static event TypedEventHandler<Key> KeyUp;

		public static void Unhook()
		{
			UnhookWindowsHookEx(_hookID);
		}

		private static IntPtr SetHook(LowLevelKeyboardProc proc)
		{
			using var curProcess = Process.GetCurrentProcess();
			using var curModule = curProcess.MainModule;
			return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
				GetModuleHandle(curModule.ModuleName), 0);
		}

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				Key vk() => (Key)Marshal.ReadInt32(lParam); //It's method. So it won't be executed if it doesn't fit further cases
				if (wParam == (IntPtr)WM_KEYDOWN)
					KeyDown.Invoke(vk());
				else if (wParam == (IntPtr)WM_KEYUP)
					KeyDown.Invoke(vk());
			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}

		#region Dll Import
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
		#endregion
	}
}
