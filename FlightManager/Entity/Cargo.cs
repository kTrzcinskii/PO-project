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
    
    public ulong ID { get; set; }
    public float Weight { get; init; }
    public string Code { get; init; }
    public string Description { get; init; }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Cargo(ulong iD, float weight, string code, string description)
    {
        ID = iD;
        _fields.Add(FieldsNames.ID, ID);
        Weight = weight;
        _fields.Add(FieldsNames.Weight, Weight);
        Code = code;
        _fields.Add(FieldsNames.Code, Code);
        Description = description;
        _fields.Add(FieldsNames.Description, Description);
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
}
