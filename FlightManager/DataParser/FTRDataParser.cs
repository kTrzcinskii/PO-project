namespace FlightManager.DataParser;

internal class FTRDataParser : IDataParser
{
    private static readonly string divider = ",";

    public IList<(string, string[])> ParseData(string dataPath)
    {
        var arguments = new List<(string, string[])>();
        var fileContentLines = File.ReadAllLines(dataPath);
        foreach (var line in fileContentLines)
        {
            var splittedLine = line.Split(divider);
            var enitityName = splittedLine[0];
            var parameters = splittedLine[1..];
            arguments.Add((enitityName, parameters));
        }
        return arguments;
    }
}
