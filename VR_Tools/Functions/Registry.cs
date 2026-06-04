#pragma warning disable CA1416 // Validate platform compatibility
using System;
using Microsoft.Win32;

namespace VR_Tools.Functions;

public static class Registry
{
    // A lot of this is unused for now
    public static void CreateSubKey(string keyPath)
    {
        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) == null)
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(keyPath);
            }
            catch (Exception e)
            {
                Log.AddLine(e.ToString(), "ERROR");
                return;
            }
            Log.AddLine($"SubKey created {keyPath}", "INFO");
            return;
        }
        else
        {
            Log.AddLine("SubKey already exists", "ERROR");
            return;
        }
    }
    public static void DeleteSubKeyTree(string keyPath) // For debug use only
    {
        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) != null)
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(keyPath);
            }
            catch (Exception e)
            {
                Log.AddLine(e.ToString(), "ERROR");
                return;
            }
            Log.AddLine($"SubKey {keyPath} and children deleted", "INFO");
            return;
        }
        else
        {
            Log.AddLine("SubKey doesn't exist", "ERROR");
            return;
        }
    }
    public static bool DoesSubKeyExist(string keyPath)
    {
        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool DoesValueExist(string keyPath, string value)
    {
        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) != null)
        {
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath, false)!;
            if (key.GetValue(value) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public static void CreateValue(string keyPath, string name, object value, Microsoft.Win32.RegistryValueKind registryValueKind)
    {
        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) != null)
        {
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath, true)!;
            if (key.GetValue(name) == null)
            {
                key.SetValue(name, value, registryValueKind);
                key.Close();
                Log.AddLine("Registry value created", "INFO");
                return;
            }
            else
            {
                key.Close();
                Log.AddLine("Registry value already exists", "ERROR");
                return;
            }
        }
        else
        {
            Log.AddLine("SubKey doesn't exist", "ERROR");
            return;
        }
    }
    public static void DeleteValue(string keyPath, string name)
    {
        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) != null)
        {
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath, true)!;
            if (key.GetValue(name) != null)
            {
                key.DeleteValue(name);
                key.Close();
                Log.AddLine("Registry value deleted", "INFO");
                return;
            }
            else
            {
                key.Close();
                Log.AddLine("Registry value doesn't exist", "ERROR");
                return;
            }
        }
        else
        {
            Log.AddLine("SubKey doesn't exist", "ERROR");
            return;
        }
    }

    // Todo: move logging outside of these registry functions maybe
    public static String? GetStringValue(string keyPath, string name)
    {
		if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath) != null)
		{
			RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath, true)!;
			if (key.GetValueKind(name) == RegistryValueKind.String)
			{
                return key.GetValue(name)!.ToString();
			}
			else
			{
				key.Close();
				Log.AddLine("Registry value doesn't exist", "ERROR");
				return null;
			}
		}
		else
		{
			Log.AddLine("SubKey doesn't exist", "ERROR");
			return null;
		}
	}
}