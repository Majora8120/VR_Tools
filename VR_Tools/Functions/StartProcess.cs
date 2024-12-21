using System.Diagnostics;
using System.IO;

namespace VR_Tools.Functions;

public static class StartProcess
{
    public static void Program(string name, string path)
    {
        string message = "null";
        string type = "ERROR";

        if (File.Exists(path) == false)
        {
            (message, type) = ($"Invalid Path: {path}", "ERROR");
            Log.AddLine(message, type);
            return;
        }

        Process[] process = Process.GetProcessesByName(name);
        if (process.Length != 0)
        {
            (message, type) = ($"Process is already running", "ERROR");
            Log.AddLine(message, type);
            return;
        }
        else
        {
            Process.Start(path);
            (message, type) = ($"Opened {name}", "INFO");
            Log.AddLine(message, type);
            return;
        }
    }
    public static void Explorer(string path)
    {
        string message = "null";
        string type = "ERROR";

        if (Directory.Exists(path) == false)
        {
            (message, type) = ($"Invalid Path: {path}", "ERROR");
            Log.AddLine(message, type);
            return;
        }

        Process.Start("explorer.exe", path);
        (message, type) = ($"Opened {path}", "INFO");
        Log.AddLine(message, type);
        return;
    }
}