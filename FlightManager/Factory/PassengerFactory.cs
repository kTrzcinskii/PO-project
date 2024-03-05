using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;

internal class PassengerFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.PassengerID;
    private PassengerArgumentsParser Parser { get; init; }

    public PassengerFactory()
    {
        Parser = new PassengerArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, name, age, phone, email, @class, miles) = Parser.ParseArgumets(parameters);
        return new Passenger(ID, name, age, phone, email, @class, miles);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        var (ID, name, age, phone, email, @class, miles) = Parser.ParseArgumets(parameters);
        return new Passenger(ID, name, age, phone, email, @class, miles);
    }
}
