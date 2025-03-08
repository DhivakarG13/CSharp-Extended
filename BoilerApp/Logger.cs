using BoilerApp.Utilities;

namespace BoilerApp
{
    public delegate void StateChangedHandler(string eventDetails);
    public class Logger
    {
        public event StateChangedHandler OnStateChange;
        private string _fileName;

        public Logger(string fileName)
        {
            _fileName = fileName;
            OnStateChange += LogFile;
            OnStateChange += IOHandlerUtility.StateChangeNotifier;
        }
        public void LogFile(string eventDetails)
        {
            LogDetails logData = new LogDetails(eventDetails, DateTime.Now);
            if (!File.Exists(_fileName))
            {
                File.Create(_fileName);
            }
            using (FileStream fileStream = new FileStream(_fileName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine($"{logData.EventOccurred},{logData.EventOccurredTime}");
                }
            }
        }
        public void viewLog()
        {
            LogDetails logData = new LogDetails();
            if (!File.Exists(_fileName))
            {
                Console.WriteLine("No event occurred till now");
                return;
            }
            using (FileStream fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    var logDetails = File.ReadAllLines(_fileName)
                 .Select(line => line.Split(','))
                 .Select(parts => new LogDetails
                 {
                     EventOccurred = parts[0],
                     EventOccurredTime = DateTime.Parse(parts[1]),
                 })
                 .ToList();

                    foreach (var log in logDetails)
                    {
                        Console.WriteLine($"Event: {log.EventOccurred}, Time: {log.EventOccurredTime}");
                    }
                }
            }
        }
        public void EventOccurred(string eventDetails)
        {
            OnStateChange.Invoke(eventDetails);
        }
    }
    file class LogDetails
    {
        public string EventOccurred { get; set; }
        public DateTime EventOccurredTime { get; set; }
        public LogDetails()
        {
            EventOccurred = string.Empty;
            EventOccurredTime = DateTime.Now;
        }
        public LogDetails(string eventOccurred, DateTime eventOccurredTime)
        {
            EventOccurred = eventOccurred;
            EventOccurredTime = eventOccurredTime;
        }
    }
}
