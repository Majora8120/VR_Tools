#pragma warning disable CA1416 // Validate platform compatibility
using System.ServiceProcess;
using System.Linq;

namespace VR_Tools.Functions;

public static class Service
{
    private const string ServiceName = "OVRService";
    public static void StartService()
    {
        string message = "null";
        string type = "ERROR";

        if (DoesServiceExist() == false)
        {
            Log.AddLine($"{ServiceName} doesn't exist", "ERROR");
            return;
        }
        ServiceController sc = new ServiceController(ServiceName);
        if (sc.Status == ServiceControllerStatus.Stopped | sc.Status == ServiceControllerStatus.StopPending)
        {
            sc.Start();
            (message, type) = ("Service Started", "INFO");
            Log.AddLine(message, type);
            return;
        }
        else if (sc.Status == ServiceControllerStatus.StartPending | sc.Status == ServiceControllerStatus.Running)
        {
            (message, type) = ("Service is already running", "ERROR");
            Log.AddLine(message, type);
            return;
        }
        return;
    }
    public static void StopService()
    {
        string message = "null";
        string type = "ERROR";

        if (DoesServiceExist() == false)
        {
            Log.AddLine($"{ServiceName} doesn't exist", "ERROR");
            return;
        }
        ServiceController sc = new ServiceController(ServiceName);
        if (sc.Status == ServiceControllerStatus.Running | sc.Status == ServiceControllerStatus.StartPending)
        {
            sc.Stop();
            (message, type) = ("Service Stopped", "INFO");
            Log.AddLine(message, type);
            return;
        }
        else if (sc.Status == ServiceControllerStatus.Stopped | sc.Status == ServiceControllerStatus.StopPending)
        {
            (message, type) = ("Service is already stopped", "ERROR");
            Log.AddLine(message, type);
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