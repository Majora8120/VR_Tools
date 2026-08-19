#pragma warning disable CA1416 // Validate platform compatibility
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using VR_Tools.Functions;

namespace VR_Tools.Views;

public partial class MainWindow : Window
{
    private const double AspectRatio = 5.0 / 4.0;
    private bool _updatingSize;
    
    public MainWindow()
    {
        InitializeComponent();
        Config.LoadConfigFile();
        UpdateStatus();

#if DEBUG
        TitleBar.Text = "VR Tools vDebug";
#else
        TitleBar.Text = "VR Tools v1.1.0";
#endif
    }
    
    private void OnWindowSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (_updatingSize)
            return;
        _updatingSize = true;
        try
        {
            if (e.WidthChanged)
                Height = Width / AspectRatio;
            else
                Width = Height * AspectRatio;
        }
        finally
        {
            _updatingSize = false;
        }
    }
    
    public void SetPriorityButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "OVRServer_x64":
                Priority.SetPriority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.High);
                break;
            case "OculusDash":
                Priority.SetPriority("OculusDash", "OculusDash.exe", ProcessPriorityClass.AboveNormal);
                break;
        }
    }
    public void EditRegistry(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        var subkey = @"SOFTWARE\Oculus";
        var value = "AswDisabled";
        switch (source!.Name)
        {
            case "ASWEnable":
                if (!Registry.DoesSubKeyExist(subkey))
                {
                    Registry.CreateSubKey(subkey);
                }
                if (Registry.DoesValueExist(subkey, value))
                {
                    Registry.DeleteValue(subkey, "AswDisabled");
                }
                break;
            case "ASWDisable":
                if (!Registry.DoesSubKeyExist(subkey))
                {
                    Registry.CreateSubKey(subkey);
                }
                if (!Registry.DoesValueExist(subkey, value))
                {
                    Registry.CreateValue(@"SOFTWARE\Oculus", "AswDisabled", 1, Microsoft.Win32.RegistryValueKind.DWord);
                }
                break;
        }
        UpdateStatus();
    }
    

    public async void SwitchDash(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "DashSteamVR":
                await Dash.SwapToSteamVR();
                break;
            case "DashOculus":
                Dash.SwapToOculusDash();
                break;
        }
        UpdateStatus();
    }
    public void ServiceButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "StartService":
                Service.StartService("OVRService");
                break;
            case "StopService":
                Service.StopService("OVRService");
                break;
            case "RestartService":
                Service.RestartService("OVRService");
                break;
        }
        UpdateStatus();
    }
    public void OpenProgram(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "OpenOculus":
                StartProcess.Program("OculusClient", Config.OculusFilePath + @"Support\oculus-client\OculusClient.exe");
                break;
            case "OpenOculusDebug":
                StartProcess.Program("OculusDebugTool", Config.OculusFilePath + @"Support\oculus-diagnostics\OculusDebugTool.exe");
                break;
            case "OpenOculusFolder":
                StartProcess.Explorer(Config.OculusFilePath);
                break;
            case "OpenAppFolder":
                StartProcess.Explorer(@".\");
                break;
        }
    }
    public void RegenConfig(object sender, RoutedEventArgs args)
    {
        Config.GenerateConfigFile();
        Config.LoadConfigFile();
    }
    public void ClearLog(object sender, RoutedEventArgs args)
    {
        Log.log.Clear();
    }
    public void RefreshStatus(object sender, RoutedEventArgs args)
    {
        UpdateStatus();
    }
    public void AboutWindow(object sender, RoutedEventArgs args)
    {
        var window = new AboutWindow();
        window.ShowDialog(this);
    }

    public void LogWindow(object sender, RoutedEventArgs args)
    {
        var window = new LogWindow();
        window.ShowDialog(this);
    }
    public void UpdateStatus()
    {
        string asw = "";
        if (Registry.DoesValueExist(@"SOFTWARE\Oculus", "AswDisabled") == true)
        { asw = "Disabled"; }
        else 
        { asw = "Enabled"; }

        string dash = Dash.GetCurrentDash();

        string service = Service.ServiceStatus("OVRService");

        OculusStatus.SetValue(Label.ContentProperty, $"ASW = {asw} | Dash = {dash} | Service = {service}");
    }
}