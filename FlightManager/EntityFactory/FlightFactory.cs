using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;

namespace FlightManager.EntityFactory;

internal class FlightFactory : Factory
{
    private FlightArgumentsParser Parser { get; init; }

    public FlightFactory()
    {
        Parser = new FlightArgumentsParser();
    }

    public override Flight CreateInstance(string[] parameters)
    {
        var (ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs) = Parser.ParseArgumets(parameters);
        return new Flight(ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs);
    }

    public override Flight CreateInstance(byte[] parameters)
    {
        var (ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs) = Parser.ParseArgumets(parameters);
        return new Flight(ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs);

    }
}
