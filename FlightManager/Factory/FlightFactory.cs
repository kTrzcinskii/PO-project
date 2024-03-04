using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.Factory;

internal class FlightFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.FlightID;
    private FlightArgumentsParser Parser { get; init; }

    public FlightFactory()
    {
        Parser = new FlightArgumentsParser();
    }

    public IEntity CreateInstance(string[] parameters)
    {
        var (ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs) = Parser.ParseArgumets(parameters);
        return new Flight(ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs);
    }

    public IEntity CreateInstance(byte[] parameters)
    {
        throw new NotImplementedException();
    }
}
