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
    
    protected ulong _ID { get; set; }
    public ulong ID
    {
        get => _ID;
        set
        {
            _ID = value;
            _fields[FieldsNames.ID] = value;
        }
    }
    protected string _serial { get; set; }
    public string Serial
    {
        get => _serial;
        set
        {
            _serial = value;
            _fields[FieldsNames.Serial] = value;
        }
    }
    protected string _countryISO { get; set; }
    public string CountryISO 
    {
        get => _countryISO;
        set
        {
            _countryISO = value;
            _fields[FieldsNames.CountryISO] = value;
        }
    }
    protected string _model { get; set; }
    public string Model
    {
        get => _model;
        set
        {
            _model = value;
            _fields[FieldsNames.Model] = value;
        }
    }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Plane(ulong iD, string serial, string countryISO, string model)
    {
        ID = iD;
        Serial = serial;
        CountryISO = countryISO;
        Model = model;
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

    public virtual void UpdateFieldValue(string fieldName, IComparable value)
    {
        throw new NotImplementedException();
    }
}
