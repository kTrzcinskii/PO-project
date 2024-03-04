using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;

internal class PassengerPlaneFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.PassengerPlaneID;
    public PassengerPlaneArgumentsParser Parser { get; init; }

    public PassengerPlaneFactory()
    {
        Parser = new PassengerPlaneArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize) = Parser.ParseArgumets(parameters);
        return new PassengerPlane(ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        throw new NotImplementedException();
    }
}
