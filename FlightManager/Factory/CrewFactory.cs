using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;

internal class CrewFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.CrewID;
    private CrewArgumentsParser Parser { get; init; }

    public CrewFactory()
    {
        Parser = new CrewArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, name, age, phone, email, practice, role) = Parser.ParseArgumets(parameters);
        return new Crew(ID, name, age, phone, email, practice, role);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        var (ID, name, age, phone, email, practice, role) = Parser.ParseArgumets(parameters);
        return new Crew(ID, name, age, phone, email, practice, role);
    }
}
