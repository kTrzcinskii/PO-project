using FlightManager.NewsSource;
using FlightManager.Query;
using FlightManager.Storage;

namespace FlightManager.Entity;

internal class Airport : IEntity, IReportable
{
    public static class FieldsNames
    {
        public const string ID = "ID";
        public const string Name = "Name";
        public const string Code = "Code";
        public const string Longitude = "WorldPosition.Long";
        public const string Latitude = "WorldPosition.Lat";
        public const string AMSL = "AMSL";
        public const string CountryISO = "CountryISO";
        public const string WorldPosition = "WorldPosition";

        public static List<string> allFields = new List<string>() { ID, Name, Code, Latitude, Longitude, AMSL, CountryISO, WorldPosition };
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
    private string _name { get; set; }
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            _fields[FieldsNames.Name] = value;
        }
    }
    private string _code { get; set; }
    public string Code 
    {
        get => _code;
        set
        {
            _code = value;
            _fields[FieldsNames.Code] = value;
        }
    }
    private float _longitude { get; set; }
    public float Longitude 
    {
        get => _longitude;
        set
        {
            _longitude = value;
            _worldPosition = new WorldPosition(value,_longitude);
            _fields[FieldsNames.Longitude] = value;
            _fields[FieldsNames.WorldPosition] = _worldPosition;
        }
    }
    private float _latitude { get; set; }
    public float Latitude
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
    private float _amsl { get; set; }
    public float AMSL
    {
        get => _amsl;
        set
        {
            _amsl = value;
            _fields[FieldsNames.AMSL] = value;
        }
    }
    private string _countryISO { get; set; }
    public string CountryISO 
    {
        get => _countryISO;
        set
        {
            _countryISO = value;
            _fields[FieldsNames.CountryISO] = value;
        }
    }
    
    private WorldPosition _worldPosition { get; set; }

    public WorldPosition WorldPosition 
    {
        get => _worldPosition;
        set
        {
            _worldPosition = value;
            _longitude = value.Long!.Value;
            _latitude = value.Lat!.Value;
            _fields[FieldsNames.WorldPosition] = value;
            _fields[FieldsNames.Longitude] = _longitude;
            _fields[FieldsNames.Latitude] = _latitude;
        }
    }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    private EntityStorage _storage;
    
    public Airport(ulong iD, string name, string code, float longitude, float latitude, float aMSL, string countryISO)
    {
        ID = iD;
        Name = name;
        Code = code;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = aMSL;
        CountryISO = countryISO;
        WorldPosition = new WorldPosition(longitude, latitude);
        _storage = EntityStorage.GetStorage();
        SetupUpdateFuncs();
    }

    public void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitAirport(this);
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

    public void UpdateFieldValue(string fieldName, IComparable value)
    {
        if (!_updateFuncs.ContainsKey(fieldName))
            throw new ArgumentException($"Invalid fieldName {fieldName}");
        try
        {
            _updateFuncs[fieldName].Invoke(value);
        }
        catch (Exception)
        {
            throw new ArgumentException($"Couldnt assign {value} to {fieldName}");
        }
    }

    public void UpdateID(IComparable value)
    {
        ulong newID = (ulong)value;
        _storage.RemoveAirport(ID);
        var flights = _storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            if (flight.OriginID == ID)
            {
                flight.OriginID = newID;
            }
            if (flight.TargetID == ID)
            {
                flight.TargetID = newID;
            }
        }
        ID = newID;
        _storage.Add(this);
    }

    public void UpdateName(IComparable value)
    {
        string newName = (string)value;
        Name = newName;
    }

    public void UpdateCode(IComparable value)
    {
        string newCode = (string)value;
        Code = newCode;
    }

    public void UpdateLongitude(IComparable value)
    {
        float newLong = (float)value;
        Longitude = newLong;
    }

    public void UpdateLatitude(IComparable value)
    {
        float newLatitude = (float)value;
        Latitude = newLatitude;
    }

    public void UpdateAMSL(IComparable value)
    {
        float newAMSL = (float)value;
        AMSL = newAMSL;
    }

    public void UpdateCountryISO(IComparable value)
    {
        string newCountryISO = (string)value;
        CountryISO = newCountryISO;
    }

    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.ID, UpdateID);
        _updateFuncs.Add(FieldsNames.Name, UpdateName);
        _updateFuncs.Add(FieldsNames.Code, UpdateCode);
        _updateFuncs.Add(FieldsNames.Longitude, UpdateLongitude);
        _updateFuncs.Add(FieldsNames.Latitude, UpdateLatitude);
        _updateFuncs.Add(FieldsNames.AMSL, UpdateAMSL);
        _updateFuncs.Add(FieldsNames.CountryISO, UpdateCountryISO);
    }
    
    public string AcceptNewsSource(INewsSource newsSource)
    {
        return newsSource.GetReport(this);
    }
    
}
