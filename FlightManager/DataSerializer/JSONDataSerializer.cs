using FlightManager.Entity;
using System.Text.Json;

namespace FlightManager.DataSerializer;

internal class JSONDataSerializer : IDataSerializer
{
    public string SerializeData(IEntity[] data)
    {
        string jsonData = JsonSerializer.Serialize(data);
        return jsonData;
    }
}
