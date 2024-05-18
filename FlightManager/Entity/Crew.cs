using FlightManager.Query;

namespace FlightManager.Entity;

internal class Crew : Person
{
    public static class FieldsNames
    {
        public const string Practice = "Practice";
        public const string Role = "Role";

        public static List<string> allFields = new List<string>() { Practice, Role };
    }
    
    public ushort Practice { get; init; }
    public string Role { get; init; }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Crew(ulong iD, string name, ulong age, string phone, string email, ushort practice, string role) : base(iD, name, age, phone, email)
    {
        Practice = practice;
        _fields.Add(FieldsNames.Practice, Practice);
        Role = role;
        _fields.Add(FieldsNames.Role, Role);
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitCrew(this);
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
    
    public static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Person.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
