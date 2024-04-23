namespace FlightManager.Entity;

internal class Flight : IEntity
{
    public ulong ID { get; set; }
    public ulong OriginID { get; set; }
    public ulong TargetID { get; set; }
    public DateTime TakeOffTime { get; init; }
    public DateTime LandingTime { get; init; }
    public float? Longitude { get; set; }
    public float? Latitude { get; set; }
    public float? AMSL { get; set; }
    public ulong PlaneID { get; set; }
    public ulong[] CrewIDs { get; init; }
    public ulong[] LoadIDs { get; init; }

    public Flight(ulong iD, ulong originID, ulong targetID, DateTime takeOffTime, DateTime landingTime, float? longitude, float? latitude, float? aMSL, ulong planeID, ulong[] crewIDs, ulong[] loadIDs)
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

    public void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitFlight(this);
    }
}
