using FlightManager.Query;

namespace FlightManager.Entity;

internal class Flight : IEntity
{
    // For now don't include array types
    private static class FieldsNames
    {
        public const string ID = "ID";
        public const string OriginID = "OriginID";
        public const string TargetID = "TargetID";
        public const string TakeOffTime = "TakeOffTime";
        public const string LandingTime = "LandingTime";
        public const string Longitude = "WorldPostion.Long";
        public const string Latitude = "WorldPosition.Lat";
        public const string AMSL = "AMSL";
        public const string PlaneID = "PlaneID";
        public const string WordlPosition = "WorldPosition";

        public static List<string> allFields = new List<string>() { ID, OriginID, TargetID, TakeOffTime, LandingTime, Longitude, Latitude, AMSL, PlaneID, WordlPosition };
    }
    
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
    
    public WorldPosition WorldPosition { get; set; }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Flight(ulong iD, ulong originID, ulong targetID, DateTime takeOffTime, DateTime landingTime, float? longitude, float? latitude, float? aMSL, ulong planeID, ulong[] crewIDs, ulong[] loadIDs)
    {
        ID = iD;
        _fields.Add(FieldsNames.ID, ID);
        OriginID = originID;
        _fields.Add(FieldsNames.OriginID, OriginID);
        TargetID = targetID;
        _fields.Add(FieldsNames.TargetID, TargetID);
        TakeOffTime = takeOffTime;
        _fields.Add(FieldsNames.TakeOffTime, TakeOffTime);
        LandingTime = landingTime;
        _fields.Add(FieldsNames.LandingTime, LandingTime);
        Longitude = longitude;
        _fields.Add(FieldsNames.Longitude, Longitude ?? -1.0f);
        Latitude = latitude;
        _fields.Add(FieldsNames.Latitude, Latitude ?? -1.0f);
        AMSL = aMSL;
        _fields.Add(FieldsNames.AMSL, AMSL ?? -1.0f);
        PlaneID = planeID;
        _fields.Add(FieldsNames.PlaneID, PlaneID);
        CrewIDs = crewIDs;
        LoadIDs = loadIDs;
        WorldPosition = new WorldPosition(longitude ?? -1.0f, latitude ?? -1.0f);
        _fields.Add(FieldsNames.WordlPosition, WorldPosition);
    }

    public void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitFlight(this);
    }
    
    public bool MatchCondition(QueryCondition condition)
    {
        if (!_fields.ContainsKey(condition.Property))
            throw new ArgumentException("Invalid fieldName");
        return condition.Check(_fields[condition.Property]);
    }

    public static List<string> GetAllFieldsNames()
    {
        return FieldsNames.allFields;
    }

    public IComparable GetFieldValue(string fieldName)
    {
        if (!_fields.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _fields[fieldName];
    }
}
