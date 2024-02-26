using FlightManager.Entity;
using System.Text.Json;

namespace FlightManager.DataSerializer;

internal class JSONDataSerializer : IDataSerializer
{
    public void SerializeData(IEntity[] data, string outputPath)
    {
        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(outputPath, jsonData);
    }
}
