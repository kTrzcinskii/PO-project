namespace FlightManager.Entity;

internal class Flight : IEntity
{
    public ulong ID { get; init; }
    public ulong OriginID { get; init; }
    public ulong TargetID { get; init; }
    public string TakeOffTime { get; init; }
    public string LandingTime { get; init; }
    public float Longitude { get; init; }
    public float Latitude { get; init; }
    public float AMSL { get; init; }
    public ulong PlaneID { get; init; }
    public ulong[] CrewIDs { get; init; }
    public ulong[] LoadIDs { get; init; }
    public Flight(ulong iD, ulong originID, ulong targetID, string takeOffTime, string landingTime, float longitude, float latitude, float aMSL, ulong planeID, ulong[] crewIDs, ulong[] loadIDs)
    {
        ID = iD;
        OriginID = originID;
        TargetID = targetID;
        TakeOffTime = takeOffTime;
        LandingTime = landingTime;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = aMSL;
        PlaneID = planeID;
        CrewIDs = crewIDs;
        LoadIDs = loadIDs;
    }

}
