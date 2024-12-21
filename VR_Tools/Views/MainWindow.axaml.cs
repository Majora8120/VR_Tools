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
        DataGrid.ItemsSource = Log.log;
    }
    public void SetPriorityButton(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "OVRServer_x64":
                Priority.SetPriority("OVRServer_x64", "OVRServer_x64.exe", ProcessPriorityClass.RealTime);
                break;
            case "OVRRedir":
                Priority.SetPriority("OVRRedir", "OVRRedir.exe", ProcessPriorityClass.RealTime);
                break;
            case "OculusDash":
                Priority.SetPriority("OculusDash", "OculusDash.exe", ProcessPriorityClass.High);
                break;
            case "BS_Realtime":
                Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.RealTime);
                break;
            case "BS_High":
                Priority.SetPriority("Beat Saber", "Beat Saber.exe", ProcessPriorityClass.High);
                break;
        }
    }
    public void ASWEnable(object sender, RoutedEventArgs args)
    {
        Registry.EditRegistry(false);
    }
    public void ASWDisable(object sender, RoutedEventArgs args)
    {
        Registry.EditRegistry(true);
    }
    public void OpenProgram(object sender, RoutedEventArgs args)
    {
        var source = args.Source as Control;

        switch (source!.Name)
        {
            case "OpenOculus":
                StartProcess.Program("OculusClient", @"C:\Program Files\Oculus\Support\oculus-client\OculusClient.exe");
                break;
            case "OpenOculusDebug":
                StartProcess.Program("OculusDebugTool", @"C:\Program Files\Oculus\Support\oculus-diagnostics\OculusDebugTool.exe");
                break;
            case "OpenOculusFolder":
                StartProcess.Explorer(@"C:\Program Files\Oculus\Support");
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
    public void AboutWindow(object source, RoutedEventArgs args)
    {
        var window = new AboutWindow();
        window.ShowDialog(this);
    }
}