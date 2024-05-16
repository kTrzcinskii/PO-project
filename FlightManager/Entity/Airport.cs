using FlightManager.NewsSource;
using FlightManager.Query;

namespace FlightManager.Entity;

internal class Airport : IEntity, IReportable
{
    public static class FieldsNames
    {
        public const string ID = "ID";
        public const string Name = "Name";
        public const string Code = "Code";
        public const string Longitude = "Longitude";
        public const string Latitude = "Latitude";
        public const string AMSL = "AMSL";
        public const string CountryISO = "CountryISO";

        public static List<string> allFields = new List<string>() { ID, Name, Code, Longitude, Latitude, AMSL, CountryISO };
    }
    
    public ulong ID { get; set; }
    public string Name { get; init; }
    public string Code { get; init; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public string CountryISO { get; init; }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Airport(ulong iD, string name, string code, float longitude, float latitude, float aMSL, string countryISO)
    {
        ID = iD;
        _fields.Add(FieldsNames.ID, ID);
        Name = name;
        _fields.Add(FieldsNames.Name, Name);
        Code = code;
        _fields.Add(FieldsNames.Code, Code);
        Longitude = longitude;
        _fields.Add(FieldsNames.Longitude, Longitude);
        Latitude = latitude;
        _fields.Add(FieldsNames.Latitude, Latitude);
        AMSL = aMSL;
        _fields.Add(FieldsNames.AMSL, AMSL);
        CountryISO = countryISO;
        _fields.Add(FieldsNames.CountryISO, CountryISO);
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

    public string AcceptNewsSource(INewsSource newsSource)
    {
        return newsSource.GetReport(this);
    }
    
}
