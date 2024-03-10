namespace FlightManager.ProgramArguments;
internal static class ArgumentsParser
{
    public static FlightManagerArguments Parse(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("[USAGE]: ./FlighManager.exe {input_path}");
            Environment.Exit(1);
        }
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"[ERROR]: File \"{inputPath}\" doesn't exist");
            Environment.Exit(1);
        }

        return new FlightManagerArguments(inputPath);
    }
}
