using System.Diagnostics;
using System.IO;

namespace VR_Tools.Functions;

public static class StartProcess
{
    public static void Program(string name, string path)
    {
        if (File.Exists(path) == false)
        {
            Log.AddLine($"Invalid Path: {path}", "ERROR");
            return;
        }

        Process[] process = Process.GetProcessesByName(name);
        if (process.Length != 0)
        {
            Log.AddLine($"Process is already running", "ERROR");
            return;
        }
        else
        {
            Process.Start(path);
            Log.AddLine($"Opened {name}", "INFO");
            return;
        }
    }
    public static void Explorer(string path)
    {
        if (Directory.Exists(path) == false)
        {
            Log.AddLine($"Invalid Path: {path}", "ERROR");
            return;
        }

        Process.Start("explorer.exe", path);
        Log.AddLine($"Opened {path}", "INFO");
        return;
    }
}