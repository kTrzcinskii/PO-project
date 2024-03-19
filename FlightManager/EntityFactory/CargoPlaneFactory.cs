using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;

internal class CargoPlaneFactory : Factory
{
    private CargoPlaneArgumentsParser Parser { get; init; }

    public CargoPlaneFactory()
    {
        Parser = new CargoPlaneArgumentsParser();
    }

    public override CargoPlane CreateInstance(string[] parameters)
    {
        var (ID, serial, countryISO, model, maxLoad) = Parser.ParseArgumets(parameters);
        return new CargoPlane(ID, serial, countryISO, model, maxLoad);
    }

    public override CargoPlane CreateInstance(byte[] parameters)
    {
        var (ID, serial, countryISO, model, maxLoad) = Parser.ParseArgumets(parameters);
        return new CargoPlane(ID, serial, countryISO, model, maxLoad);
    }
}

