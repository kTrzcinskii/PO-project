namespace FlightManager.DataParser;

internal class FTRDataParser : IDataParser<string, string[]>
{
    private static readonly string divider = ",";

    public (string, string[]) ParseData(string data)
    {
        var splittedLine = data.Split(divider);
        var enitityName = splittedLine[0];
        var parameters = splittedLine[1..];
        return (enitityName, parameters);
    }
}
