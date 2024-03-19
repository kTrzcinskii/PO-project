using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;

internal class PassengerPlaneFactory : Factory
{
    public PassengerPlaneArgumentsParser Parser { get; init; }

    public PassengerPlaneFactory()
    {
        Parser = new PassengerPlaneArgumentsParser();
    }

    public override PassengerPlane CreateInstance(string[] parameters)
    {
        var (ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize) = Parser.ParseArgumets(parameters);
        return new PassengerPlane(ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
    }

    public override PassengerPlane CreateInstance(byte[] parameters)
    {
        var (ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize) = Parser.ParseArgumets(parameters);
        return new PassengerPlane(ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
    }
}
