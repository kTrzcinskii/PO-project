namespace FlightManager.ProgramArguments;
internal struct FlightManagerArguments
{
    public readonly string inputPath;

    public FlightManagerArguments(string inputPath)
    {
        this.inputPath = inputPath;
    }
}