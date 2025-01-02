#pragma warning disable CA1416 // Validate platform compatibility
using System.ServiceProcess;
using System.Linq;

namespace VR_Tools.Functions;

public static class Service
{
    private const string ServiceName = "OVRService";
    public static void StartService()
    {
        if (DoesServiceExist() == false)
        {
            Log.AddLine($"{ServiceName} doesn't exist", "ERROR");
            return;
        }
        ServiceController sc = new ServiceController(ServiceName);
        if (sc.Status == ServiceControllerStatus.Stopped | sc.Status == ServiceControllerStatus.StopPending)
        {
            sc.Start();
            Log.AddLine("Service Started", "INFO");
            return;
        }
        else if (sc.Status == ServiceControllerStatus.StartPending | sc.Status == ServiceControllerStatus.Running)
        {
            Log.AddLine("Service is already running", "ERROR");
            return;
        }
        return;
    }
    public static void StopService()
    {
        if (DoesServiceExist() == false)
        {
            Log.AddLine($"{ServiceName} doesn't exist", "ERROR");
            return;
        }
        ServiceController sc = new ServiceController(ServiceName);
        if (sc.Status == ServiceControllerStatus.Running | sc.Status == ServiceControllerStatus.StartPending)
        {
            sc.Stop();
            Log.AddLine("Service Stopped", "INFO");
            return;
        }
        else if (sc.Status == ServiceControllerStatus.Stopped | sc.Status == ServiceControllerStatus.StopPending)
        {
            Log.AddLine("Service is already stopped", "ERROR");
            return;
        }
        return;
    }
    public static bool DoesServiceExist()
    {
        ServiceController[] services = ServiceController.GetServices();
        var sc = services.FirstOrDefault(s => s.ServiceName == ServiceName);
        if (sc != null) 
        { 
            return(true); 
        }
        else 
        { 
            return(false); 
        }
    }
}