using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;

internal class AirportFactory : Factory
{
    private AirportArgumentsParser Parser { get; init; }

    public AirportFactory()
    {
        Parser = new AirportArgumentsParser();
    }

    public override Airport CreateInstance(string[] parameters)
    {
        var (ID, name, code, longitude, latitude, AMSL, countryISO) = Parser.ParseArgumets(parameters);
        return new Airport(ID, name, code, longitude, latitude, AMSL, countryISO);
    }

    public override Airport CreateInstance(byte[] parameters)
    {
        var (ID, name, code, longitude, latitude, AMSL, countryISO) = Parser.ParseArgumets(parameters);
        return new Airport(ID, name, code, longitude, latitude, AMSL, countryISO);
    }
}

