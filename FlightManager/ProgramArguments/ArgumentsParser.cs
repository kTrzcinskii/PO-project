namespace FlightManager.ProgramArguments;
internal class ArgumentsParser
{
    public static FlightManagerArguments Parse(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("[USAGE]: ./FlighManager.exe {input_path} {output_path}");
            Environment.Exit(1);
        }
        string inputPath = args[0];
        string outputPath = args[1];
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"[ERROR]: File \"{inputPath}\" doesn't exist");
            Environment.Exit(1);
        }

        return new FlightManagerArguments(inputPath, outputPath);
    }
}
