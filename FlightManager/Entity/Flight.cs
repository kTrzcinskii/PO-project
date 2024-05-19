using FlightManager.Query;

namespace FlightManager.Entity;

internal class Flight : IEntity
{
    public static class FieldsNames
    {
        public const string ID = "ID";
        public const string OriginID = "OriginID";
        public const string TargetID = "TargetID";
        public const string TakeOffTime = "TakeOffTime";
        public const string LandingTime = "LandingTime";
        public const string Longitude = "WorldPosition.Long";
        public const string Latitude = "WorldPosition.Lat";
        public const string AMSL = "AMSL";
        public const string PlaneID = "PlaneID";
        public const string WorldPosition = "WorldPosition";

        public static List<string> allFields = new List<string>() { ID, OriginID, TargetID, TakeOffTime, LandingTime, Longitude, Latitude, AMSL, PlaneID, WorldPosition };
    }
    
    private ulong _ID { get; set; }
    public ulong ID 
    {
        get => _ID;
        set
        {
            _ID = value;
            _fields[FieldsNames.ID] = value;
        }
    }
    private ulong _originID { get; set; }
    public ulong OriginID 
    {
        get => _originID;
        set
        {
            _originID = value;
            _fields[FieldsNames.OriginID] = value;
        }
    }
    private ulong _targetID { get; set; }
    public ulong TargetID 
    {
        get => _targetID;
        set
        {
            _targetID = value;
            _fields[FieldsNames.TargetID] = value;
        }
    }
    private DateTime _takeOffTime { get; set; }
    public DateTime TakeOffTime
    {
        get => _takeOffTime;
        set
        {
            _takeOffTime = value;
            _fields[FieldsNames.TakeOffTime] = value;
        }
    }
    private DateTime _landingTime { get; set; }
    public DateTime LandingTime
    {
        get => _landingTime;
        set
        {
            _landingTime = value;
            _fields[FieldsNames.LandingTime] = value;
        }
    }
    private float? _longitude { get; set; }
    public float? Longitude 
    {
        get => _longitude;
        set
        {
            _longitude = value;
            _worldPosition = new WorldPosition(value,_worldPosition.Lat);
            _fields[FieldsNames.Longitude] = value;
            _fields[FieldsNames.WorldPosition] = _worldPosition;
        }
    }
    private float? _latitude { get; set; }
    public float? Latitude
    {
        get => _latitude;
        set
        {
            _latitude = value;
            _worldPosition = new WorldPosition(_worldPosition.Long,value);
            _fields[FieldsNames.Latitude] = value;
            _fields[FieldsNames.WorldPosition] = _worldPosition;
        }
    }
    private float? _amsl { get; set; }
    public float? AMSL
    {
        get => _amsl;
        set
        {
            _amsl = value;
            _fields[FieldsNames.AMSL] = value;
        }
    }
    private ulong _planeID { get; set; }
    public ulong PlaneID
    {
        get => _planeID;
        set
        {
            _amsl = value;
            _fields[FieldsNames.PlaneID] = value;
        }
    }
    public ulong[] CrewIDs { get; init; }
    public ulong[] LoadIDs { get; init; }
    
    private WorldPosition _worldPosition { get; set; }

    public WorldPosition WorldPosition 
    {
        get => _worldPosition;
        set
        {
            _worldPosition = value;
            _longitude = value.Long;
            _latitude = value.Lat;
            _fields[FieldsNames.WorldPosition] = value;
            _fields[FieldsNames.Longitude] = _longitude;
            _fields[FieldsNames.Latitude] = _latitude;
        }
    }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

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
        WorldPosition = new WorldPosition(longitude, latitude);
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
