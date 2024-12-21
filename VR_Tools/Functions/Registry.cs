#pragma warning disable CA1416 // Validate platform compatibility
using Microsoft.Win32;
using System;

namespace VR_Tools.Functions;

public static class Registry
{
    public static void EditRegistry(bool disableASW)
    {
        string message = "null";
        string type = "ERROR";

        if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is null)
        {
            (message, type) = (@"HKEY_LOCAL_MACHINE\SOFTWARE\Oculus is null. Is Oculus Link installed?", "ERROR");
        }
        else if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus") is not null)
        {
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oculus", true) ?? throw new ArgumentNullException(nameof(key), "How TF is this null!");
            switch (disableASW)
            {
                case true:
                    if (key.GetValue("AswDisabled")?.ToString() is not "1")
                    {
                        key.SetValue(@"AswDisabled", 1, RegistryValueKind.DWord);

                        (message, type) = ("Registry value created", "INFO");
                    }
                    else
                    {
                        (message, type) = ("Registry value already exists", "ERROR");
                    }
                    break;
                case false:
                    if (key.GetValue("AswDisabled")?.ToString() is not null)
                    {
                        key.DeleteValue("AswDisabled");

                        (message, type) = ("Registry value deleted", "INFO");
                    }
                    else
                    {
                        (message, type) = ("Registry value doesn't exist", "ERROR");
                    }
                    break;
            }
            key.Close();
        }
        Log.AddLine(message, type);
        return;
    }
}