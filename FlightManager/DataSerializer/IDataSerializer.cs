using FlightManager.Entity;

namespace FlightManager.DataSerializer;

internal interface IDataSerializer
{
    public void SerializeData(IEntity[] data, string outputPath);
}
