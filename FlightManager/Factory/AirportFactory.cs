using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;

internal class AirportFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.AirportID;
    private AirportArgumentsParser Parser { get; init; }

    public AirportFactory()
    {
        Parser = new AirportArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, name, code, longitude, latitude, AMSL, countryISO) = Parser.ParseArgumets(parameters);
        return new Airport(ID, name, code, longitude, latitude, AMSL, countryISO);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        var (ID, name, code, longitude, latitude, AMSL, countryISO) = Parser.ParseArgumets(parameters);
        return new Airport(ID, name, code, longitude, latitude, AMSL, countryISO);
    }
}

