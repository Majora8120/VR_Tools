#pragma warning disable CA1416 // Validate platform compatibility
using System;
using Microsoft.Win32;

namespace VR_Tools
{
	internal class ASW
	{
		private const string _keyPath = @"SOFTWARE\Oculus";
		private const string _valueName = "AswDisabled";

		public static void Enable()
		{
			try
			{
				RegistryKey? key = Registry.LocalMachine.OpenSubKey(_keyPath);
				key ??= Registry.LocalMachine.CreateSubKey(_keyPath);
				key.SetValue(_valueName, 1, RegistryValueKind.DWord);
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
			}
		}

		public static void Disable()
		{
			try
			{
				RegistryKey? key = Registry.LocalMachine.OpenSubKey(_keyPath);
				key ??= Registry.LocalMachine.CreateSubKey(_keyPath);
				key.SetValue(_valueName, 0, RegistryValueKind.DWord);
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
			}
		}

		public static bool IsDisabled()
		{
			try
			{
				RegistryKey? key = Registry.LocalMachine.OpenSubKey(_keyPath);
				key ??= Registry.LocalMachine.CreateSubKey(_keyPath);
				return (bool?)key.GetValue(_valueName) ?? false;
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				return false;
			}
		}
	}
}
