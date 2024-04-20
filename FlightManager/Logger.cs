namespace FlightManager;

internal class Logger
{
    private static Logger? instance = null;
    private static string directoryPath = "logs";
    
    public static Logger GetLogger()
    {
        return instance ??= new Logger();
    }

    public void StartLogger()
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        string fileName = GetLogFileName();
        AssertFileExists(fileName);
        LogStartMessage();
    }
    
    private Logger()
    { }

    private string GetLogFileName()
    {
        string file =  DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        return Path.Join(directoryPath, file);
    }

    private void LogMessage(string message)
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        string finalMessage = $"{message} ({time})\n";
        string fileName = GetLogFileName();
        // If our app will be working at midnight, we must create new file 
        if (!AssertFileExists(fileName))
            LogStartMessage();
        File.AppendAllText(fileName, finalMessage);
    }
    
    private void LogStartMessage()
    {
        const string message = $"[INFO] Logging started";
        LogMessage(message);
    }

    public void LogUpdateMessage(string updateMessage)
    {
        string message = $"[UPDATE] {updateMessage}";
        LogMessage(message);
    }

    public void LogErrorMessage(string errorMessage)
    {
        string message = $"[ERROR] {errorMessage}";
        LogMessage(message);
    }

    private bool AssertFileExists(string fileName)
    {
        if (File.Exists(fileName)) 
            return true;
        var fs = File.Create(fileName);
        fs.Dispose();
        return false;
    }
}