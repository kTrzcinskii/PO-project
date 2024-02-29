namespace FlightManager.DataParser;

internal class FTRDataParser : IDataParser
{
    private static readonly string divider = ",";

    public IList<(string, string[])> ParseData(string[] data)
    {
        var arguments = new List<(string, string[])>();
        foreach (var line in data)
        {
            var splittedLine = line.Split(divider);
            var enitityName = splittedLine[0];
            var parameters = splittedLine[1..];
            arguments.Add((enitityName, parameters));
        }
        return arguments;
    }
}
