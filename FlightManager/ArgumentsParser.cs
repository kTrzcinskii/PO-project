namespace FlightManager;
internal class ArgumentsParser
{
    public string InputPath { get; }
    public string OutputPath { get; }
    public ArgumentsParser(string[] args)
    {
        InputPath = args[0];
        OutputPath = args[1];
    }
}
