using FlightManager.Entity;

namespace FlightManager.DataSerializer;

internal interface IDataSerializer
{
    public string SerializeData(IEntity[] data);
}
