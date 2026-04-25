using System;
using System.Xml;
using System.Xml.Linq;

namespace VR_Tools;

public static class Config
{
    // Default values
    private const string LatestConfigVersion = "1.0";
    private const string DefaultOculusFilePath = @"C:\Program Files\Oculus\";
    private const string DefaultOculusKillerURL = @"https://github.com/BnuuySolutions/OculusKiller/releases/latest/download/OculusDash.exe";
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
            GenerateConfigFile();
            return;
        }

        if (config.SelectSingleNode("xml/ConfigVersion")  != null && config.SelectSingleNode("xml/ConfigVersion")!.InnerText == "1.0")
        {
            if (config.SelectSingleNode("xml/OculusLinkInstallPath") != null && config.SelectSingleNode("xml/OculusKillerURL") != null)
            {
                OculusFilePath = config.SelectSingleNode("xml/OculusLinkInstallPath")!.InnerText;
                OculusKillerURL = config.SelectSingleNode("xml/OculusKillerURL")!.InnerText;
            }
            else
            {
                Log.AddLine("Config file error. Using default values", "ERROR");
                return;
            }
            Log.AddLine("Loaded config file", "INFO");
            return;
        }
        else
        {
            Log.AddLine("Config version error. Using default values", "ERROR");
            return;
        }
    }
    public static void GenerateConfigFile()
    {
        XDocument config = new XDocument(new XElement("xml", 
            new XElement("ConfigVersion", LatestConfigVersion),
            new XElement("OculusLinkInstallPath", DefaultOculusFilePath), 
            new XElement("OculusKillerURL", DefaultOculusKillerURL)));
        config.Save(@".\config.xml");
        Log.AddLine("Generated config file", "INFO");
        return;
    }
}