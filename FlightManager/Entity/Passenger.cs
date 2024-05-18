using FlightManager.Query;

namespace FlightManager.Entity;

internal class Passenger : Person, ILoad
{
    public static class FieldsNames
    {
        public const string Class = "Class";
        public const string Miles = "Miles";

        public static List<string> allFields = new List<string>() { Class, Miles };
    }
    
    public string Class { get; init; }
    public ulong Miles { get; init; }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();
    
    public Passenger(ulong iD, string name, ulong age, string phone, string email, string @class, ulong miles) : base(iD, name, age, phone, email)
    {
        Class = @class;
        _fields.Add(FieldsNames.Class, Class);
        Miles = miles;
        _fields.Add(FieldsNames.Miles, Miles);
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitPassenger(this);
    }

    public override bool MatchCondition(QueryCondition condition)
    {
        if (!_fields.ContainsKey(condition.Property))
            return base.MatchCondition(condition);
        return condition.Check(_fields[condition.Property]);
    }

    public override IComparable GetFieldValue(string fieldName)
    {
        if (!_fields.ContainsKey(fieldName))
            return base.GetFieldValue(fieldName);
        return _fields[fieldName];
    }
    
    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Person.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
