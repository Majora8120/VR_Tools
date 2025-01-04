#pragma warning disable CA1416 // Validate platform compatibility
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using VR_Tools.Functions;

namespace VR_Tools.Views;

public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        Config.LoadConfigFile();
        DataGrid.ItemsSource = Log.log;

#if DEBUG
        TitleBar.Text = "VR Tools vDebug";
        MenuBar_Debug.IsVisible = true;
#else
        TitleBar.Text = "VR Tools v1.0.0";
#endif
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

        switch (source!.Name)
        {
            case "ASWEnable":
                Registry.DeleteValue(@"SOFTWARE\Oculus", "AswDisabled");
                break;
            case "ASWDisable":
                Registry.CreateValue(@"SOFTWARE\Oculus", "AswDisabled", 1, Microsoft.Win32.RegistryValueKind.DWord);
                break;
        }
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
    }
    public void ServiceButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "StartService":
                Service.StartService();
                break;
            case "StopService":
                Service.StopService();
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
    public void AboutWindow(object sender, RoutedEventArgs args)
    {
        var window = new AboutWindow();
        window.ShowDialog(this);
    }
}