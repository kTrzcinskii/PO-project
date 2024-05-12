using FlightManager.Query;

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

    public bool MatchCondition(QueryCondition condition)
    {
        switch (condition.Property)
        {
            case "ID":
                return condition.Check(ID);
            case "OriginID":
                return condition.Check(OriginID);
            case "TargetID":
                return condition.Check(TargetID);
            case "TakeOffTime":
                return condition.Check(TakeOffTime);
            case "LandingTime":
                return condition.Check(LandingTime);
            case "Longitude":
                return condition.Check(Longitude!.Value);
            case "Latitude":
                return condition.Check(Latitude!.Value);
            case "AMSL":
                return condition.Check(AMSL!.Value);
            case "PlaneID":
                return condition.Check(PlaneID);
        }

        return false;
    }
}
