using System;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VR_Tools.Functions;

public static class Dash
{
    private static string FullFilePath = Config.OculusFilePath + @"Support\oculus-dash\dash\bin\OculusDash.exe";
    private static string FilePath = Config.OculusFilePath;
    private static string DashBackup = Config.OculusFilePath + @"Support\oculus-dash\dash\bin\OculusDash.exe.bak";
    //private static string KillerBackup = Config.OculusFilePath + @"Support\oculus-dash\dash\bin\OculusDash.exe.killer";

    public static async Task SwapToSteamVR()
    {
        string? currentDash = GetCurrentDash();

        if (IsDashRunning() == true)
        {
            Log.AddLine("Close Oculus Dash and/or SteamVR!!!", "ERROR");
            return;
        }
        if (currentDash == "SteamVR")
        {
            Log.AddLine("Oculus Killer already installed", "ERROR");
            return;
        }
        if (currentDash == null)
        {
            return;
        }

        try
        {
            File.Move(FullFilePath, DashBackup);
        }
        catch (Exception e)
        {
            Log.AddLine(e.ToString(), "ERROR");
            return;
        }

        Stream fileStream = await GetFileStream(Config.OculusKillerURL);
        if (fileStream != Stream.Null)
        {
            await SaveFileStream(fileStream);
            Log.AddLine("Oculus Killer installed", "INFO");
            return;
        }
        return;
    }
    public static void SwapToOculusDash()
    {
        string? currentDash = GetCurrentDash();

        if (IsDashRunning() == true)
        {
            Log.AddLine("Close OculusDash and SteamVR!!!", "ERROR");
            return;
        }
        if (currentDash == "OculusDash")
        {
            Log.AddLine("Oculus Dash already installed", "ERROR");
            return;
        }
        if (currentDash == null)
        {
            return;
        }

        try
        {
            File.Replace(DashBackup, FullFilePath, null);
        }
        catch (Exception e)
        {
            Log.AddLine(e.ToString(), "ERROR");
            return;
        }
        Log.AddLine("Oculus Dash restored", "INFO");
        return;
    }
    private static string? GetCurrentDash()
    {
        if (Directory.Exists(FilePath) == false)
        {
            Log.AddLine("Directory doesn't exist. Is Oculus Link installed?", "ERROR");
            return null;
        }
        if (File.Exists(FullFilePath) == false)
        {
            Log.AddLine("OculusDash.exe doesn't exist?", "ERROR");
            return null;
        }
        FileInfo fileInfo = new FileInfo(FullFilePath);
        if (fileInfo.Length < 5000000) // Oculus Killer is less than 1MB in size. Dash is ~32MB
        {
            return("SteamVR");
        }
        else
        {
            return ("OculusDash");
        }
    }
    private static bool IsDashRunning()
    {
        Process[] process = Process.GetProcessesByName("OculusDash");
        if (process.Length != 0)
        {
            return true;
        }
        process = Process.GetProcessesByName("vrserver");
        if (process.Length != 0)
        {
            return true;
        }
        return false;
    }
    private static async Task<Stream> GetFileStream(string url)
    {
        HttpClient client = new HttpClient();
        try
        {
            Stream fileStream = await client.GetStreamAsync(url);
            return fileStream;
        }
        catch(Exception e)
        {
            Log.AddLine(e.ToString(), "ERROR");
            return(Stream.Null);
        }
    }
    private static async Task SaveFileStream(Stream fileStream)
    {
        try
        {
            using FileStream outputFileStream = new FileStream(FullFilePath, FileMode.CreateNew);
            await fileStream.CopyToAsync(outputFileStream);
        }
        catch (Exception e)
        {
            Log.AddLine(e.ToString(), "ERROR");
            return;
        }
        return;
    }
}