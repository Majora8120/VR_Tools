using System;
using System.Xml;

namespace VR_Tools;

public static class Config
{
    // Default values
    private const string DefaultOculusFilePath = @"C:\Program Files\Oculus\";
    private const string DefaultOculusKillerURL = @"https://github.com/BnuuySolutions/OculusKiller/releases/download/v1.3.0/OculusDash.exe";
    // Global values
    public static string OculusFilePath = DefaultOculusFilePath;
    public static string OculusKillerURL = DefaultOculusKillerURL;
    public static void LoadConfigFile()
    {
        XmlDocument config = new XmlDocument();
        try
        {
            config.Load(@"./config.xml");
        }
        catch(Exception e)
        {
            Log.AddLine(e.ToString(), "ERROR");
            return;
        }

        if (config.SelectSingleNode("XML/OculusLinkInstallPath") != null && config.SelectSingleNode("XML/OculusKillerURL") != null)
        {
            OculusFilePath = config.SelectSingleNode("XML/OculusLinkInstallPath")!.InnerText;
            OculusKillerURL = config.SelectSingleNode("XML/OculusKillerURL")!.InnerText;
        }
        else
        {
            Log.AddLine("Config file error. Using default values", "ERROR");
            return;
        }
        Log.AddLine("Loaded config file", "INFO");
        return;
    }
}