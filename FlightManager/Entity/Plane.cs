using FlightManager.Query;

namespace FlightManager.Entity;
internal abstract class Plane : IEntity
{
    public static class FieldsNames
    {
        public const string ID = "ID";
        public const string Serial = "Serial";
        public const string CountryISO = "CountryISO";
        public const string Model = "Model";

        public static List<string> allFields = new List<string>() { ID, Serial, CountryISO, Model };
    }
    
    public ulong ID { get; set; }
    public string Serial { get; init; }
    public string CountryISO { get; init; }
    public string Model { get; init; }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Plane(ulong iD, string serial, string countryISO, string model)
    {
        ID = iD;
        _fields.Add(FieldsNames.ID, ID);
        Serial = serial;
        _fields.Add(FieldsNames.Serial, Serial);
        CountryISO = countryISO;
        _fields.Add(FieldsNames.CountryISO, CountryISO);
        Model = model;
        _fields.Add(FieldsNames.Model, Model);
    }

    public abstract void AcceptVisitor(IEntityVisitor visitor);
    public virtual bool MatchCondition(QueryCondition condition)
    {
        if (!_fields.ContainsKey(condition.Property))
            throw new ArgumentException("Invalid fieldName");
        return condition.Check(_fields[condition.Property]);
    }

    public static List<string> GetAllFieldsNames()
    {
        return FieldsNames.allFields;
    }

    public virtual IComparable GetFieldValue(string fieldName)
    {
        if (!_fields.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _fields[fieldName];
    }
}
