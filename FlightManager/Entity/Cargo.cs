using FlightManager.Query;

namespace FlightManager.Entity;

internal class Cargo : IEntity, ILoad
{
    public static class FieldsNames
    {
        public const string ID = "ID";
        public const string Weight = "Weight";
        public const string Code = "Code";
        public const string Description = "Description";

        public static List<string> allFields = new List<string>() { ID, Weight, Code, Description};
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
    private float _weight { get; set; }
    public float Weight
    {
        get => _weight;
        set
        {
            _weight = value;
            _fields[FieldsNames.Weight] = value;
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
    private string _description { get; set; }
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            _fields[FieldsNames.Description] = value;
        }
    }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Cargo(ulong iD, float weight, string code, string description)
    {
        ID = iD;
        Weight = weight;
        Code = code;
        Description = description;
    }

    public void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitCargo(this);
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
        throw new NotImplementedException();
    }
}
