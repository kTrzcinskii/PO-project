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
    
    private string _class { get; set; }
    public string Class
    {
        get => _class;
        set
        {
            _class = value;
            _fields[FieldsNames.Class] = value;
        }
    }
    private ulong _miles { get; set; }
    public ulong Miles
    {
        get => _miles;
        set
        {
            _miles = value;
            _fields[FieldsNames.Miles] = value;
        }
    }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();
    
    public Passenger(ulong iD, string name, ulong age, string phone, string email, string @class, ulong miles) : base(iD, name, age, phone, email)
    {
        Class = @class;
        Miles = miles;
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

    public override void UpdateFieldValue(string fieldName, IComparable value)
    {
        base.UpdateFieldValue(fieldName, value);
    }

    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Person.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
