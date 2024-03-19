using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;

internal class CrewFactory : Factory
{
    private CrewArgumentsParser Parser { get; init; }

    public CrewFactory()
    {
        Parser = new CrewArgumentsParser();
    }

    public override Crew CreateInstance(string[] parameters)
    {
        var (ID, name, age, phone, email, practice, role) = Parser.ParseArgumets(parameters);
        return new Crew(ID, name, age, phone, email, practice, role);
    }

    public override Crew CreateInstance(byte[] parameters)
    {
        var (ID, name, age, phone, email, practice, role) = Parser.ParseArgumets(parameters);
        return new Crew(ID, name, age, phone, email, practice, role);
    }
}
