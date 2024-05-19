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
    
    private ushort _practice { get; set; }
    public ushort Practice
    {
        get => _practice;
        set
        {
            _practice = value;
            _fields[FieldsNames.Practice] = value;
        }
    }
    private string _role { get; set; }
    public string Role
    {
        get => _role;
        set
        {
            _role = value;
            _fields[FieldsNames.Role] = value;
        }
    }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public Crew(ulong iD, string name, ulong age, string phone, string email, ushort practice, string role) : base(iD, name, age, phone, email)
    {
        Practice = practice;
        Role = role;
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
