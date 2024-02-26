using FlightManager.Entity;

namespace FlightManager.Factory;

internal class PassengerPlaneFactory : IFactory
{
    public string EntityName => "PP";

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        string serial = parameters[1];
        string countryISO = parameters[2];
        string model = parameters[3];
        ushort firstClassSize = Convert.ToUInt16(parameters[4]);
        ushort businessClassSize = Convert.ToUInt16(parameters[5]);
        ushort economyClassSize = Convert.ToUInt16(parameters[6]);
        return new PassengerPlane(ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
    }
}
