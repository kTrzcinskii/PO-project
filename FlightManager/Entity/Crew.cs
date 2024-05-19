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
        SetupUpdateFuncs();
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

    public override void UpdateFieldValue(string fieldName, IComparable value)
    {
        if (!_updateFuncs.ContainsKey(fieldName))
        {
            base.UpdateFieldValue(fieldName, value);
            return;
        }
        try
        {
            _updateFuncs[fieldName].Invoke(value);
        }
        catch (Exception)
        {
            throw new ArgumentException($"Couldnt assign {value} to {fieldName}");
        }
    }

    public override void UpdateID(IComparable value)
    {
        ulong newID = (ulong)value;
        _storage.RemoveCrew(ID);
        
        var flights = _storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            int index = Array.IndexOf(flight.CrewIDs, ID);
            if (index == -1)
                continue;
            flight.CrewIDs[index] = newID;
            break;
        }
        
        ID = newID;
        _storage.Add(this);
    }

    public void UpdatePractice(IComparable value)
    {
        ushort newPractice = (ushort)value;
        Practice = newPractice;
    }

    public void UpdateRole(IComparable value)
    {
        string newRole = (string)value;
        Role = newRole;
    }
    
    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.Practice, UpdatePractice);
        _updateFuncs.Add(FieldsNames.Role, UpdateRole);
    }
    
    public static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Person.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
