using System.Collections.ObjectModel;

namespace VR_Tools
{
    public class Log
    {
        public static ObservableCollection<Log> log = [];
        private string Message { get; set; }
        private string Type { get; set; }
        private Log(string message, string type)
        {
            Message = message;
            Type = type;
        }
        public static void AddLine(string message, string type)
        {
            log.Insert(0, new Log(message, type));
        }
        public static void Info(string message)
        {
            log.Insert(0, new Log(message, "INFO"));
        }
        public static void Warning(string message)
        {
			log.Insert(0, new Log(message, "WARNING"));
		}
		public static void Error(string message)
		{
			log.Insert(0, new Log(message, "ERROR"));
		}
	}
}
