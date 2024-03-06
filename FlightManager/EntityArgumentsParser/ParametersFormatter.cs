namespace FlightManager.EntityArgumentsParser;

internal static class ParametersFormatter
{
    public static string[] ConvertToArray(string argument, string splitOn = ";")
    {
        // Array is declared as "[value1;value2;value3,...]"
        return argument[1..^1].Split(splitOn);
    }
}
