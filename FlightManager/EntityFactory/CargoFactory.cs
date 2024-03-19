using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;
internal class CargoFactory : Factory
{
    private CargoArgumentsParser Parser { get; init; }

    public CargoFactory()
    {
        Parser = new CargoArgumentsParser();
    }

    public override Cargo CreateInstance(string[] parameters)
    {
        var (ID, weight, code, description) = Parser.ParseArgumets(parameters);
        return new Cargo(ID, weight, code, description);
    }

    public override Cargo CreateInstance(byte[] parameters)
    {
        var (ID, weight, code, description) = Parser.ParseArgumets(parameters);
        return new Cargo(ID, weight, code, description);
    }
}
