namespace FlightManager.EntityArgumentsParser;

internal static class ParametersFormatter
{
    public static string[] ConvertToArray(string argument, string splitOn = ";")
    {
        // Array is declared as "[value1;value2;value3,...]"
        if (argument == "[]")
            return Array.Empty<string>();
        return argument[1..^1].Split(splitOn);
    }

    public static string ReadStringFromBytes(BinaryReader reader, int length, bool removeNulls = true)
    {
        var s = new string(reader.ReadChars(length));
        if (removeNulls)
            return s.Replace("\0", string.Empty);
        return s;
    }
}
