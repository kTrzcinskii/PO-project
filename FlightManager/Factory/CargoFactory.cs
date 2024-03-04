using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;
internal class CargoFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.CargoID;
    private CargoArgumentsParser Parser { get; init; }

    public CargoFactory()
    {
        Parser = new CargoArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, weight, code, description) = Parser.ParseArgumets(parameters);
        return new Cargo(ID, weight, code, description);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        var (ID, weight, code, description) = Parser.ParseArgumets(parameters);
        return new Cargo(ID, weight, code, description);
    }
}
