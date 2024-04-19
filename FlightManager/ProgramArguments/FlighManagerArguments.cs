namespace FlightManager.ProgramArguments;
internal struct FlightManagerArguments
{
    public readonly string inputPath;
    public readonly string? updatePath;

    public FlightManagerArguments(string inputPath, string? updatePath = null)
    {
        this.inputPath = inputPath;
        this.updatePath = updatePath;
    }
}