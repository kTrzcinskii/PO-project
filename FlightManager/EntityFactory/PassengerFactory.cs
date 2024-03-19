using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;

internal class PassengerFactory : Factory
{
    private PassengerArgumentsParser Parser { get; init; }

    public PassengerFactory()
    {
        Parser = new PassengerArgumentsParser();
    }

    public override Passenger CreateInstance(string[] parameters)
    {
        var (ID, name, age, phone, email, @class, miles) = Parser.ParseArgumets(parameters);
        return new Passenger(ID, name, age, phone, email, @class, miles);
    }

    public override Passenger CreateInstance(byte[] parameters)
    {
        var (ID, name, age, phone, email, @class, miles) = Parser.ParseArgumets(parameters);
        return new Passenger(ID, name, age, phone, email, @class, miles);
    }
}
