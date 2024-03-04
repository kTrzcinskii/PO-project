using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;

internal class CargoPlaneFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.CargoPlaneID;
    private CargoPlaneArgumentsParser Parser { get; init; }

    public CargoPlaneFactory()
    {
        Parser = new CargoPlaneArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, serial, countryISO, model, maxLoad) = Parser.ParseArgumets(parameters);
        return new CargoPlane(ID, serial, countryISO, model, maxLoad);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        var (ID, serial, countryISO, model, maxLoad) = Parser.ParseArgumets(parameters);
        return new CargoPlane(ID, serial, countryISO, model, maxLoad);
    }
}

