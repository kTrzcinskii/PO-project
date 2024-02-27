namespace FlightManager.ProgramArguments;
internal struct FlightManagerArguments
{
    public readonly string inputPath;
    public readonly string outputPath;

    public FlightManagerArguments(string inputPath, string outputPath)
    {
        this.inputPath = inputPath;
        this.outputPath = outputPath;
    }
}
