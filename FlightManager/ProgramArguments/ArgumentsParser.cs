namespace FlightManager.ProgramArguments;
internal static class ArgumentsParser
{
    public static FlightManagerArguments Parse(string[] args)
    {
        if (args.Length != 1 && args.Length != 2)
        {
            Console.WriteLine("[USAGE]: ./FlighManager.exe {input_path} {update_path}(optional)");
            Environment.Exit(1);
        }
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"[ERROR]: File \"{inputPath}\" doesn't exist");
            Environment.Exit(1);
        }

        string? updatePath = null;
        if (args.Length == 2)
        {
            updatePath = args[1];
            if (!File.Exists(updatePath))
            {
                Console.WriteLine($"[ERROR]: File \"{updatePath}\" doesn't exist");
                Environment.Exit(1);
            }
        }

        return new FlightManagerArguments(inputPath, updatePath);
    }
}
