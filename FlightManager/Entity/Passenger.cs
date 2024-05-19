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
        SetupUpdateFuncs();
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
        _storage.RemovePassenger(ID);
        
        var flights = _storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            int index = Array.IndexOf(flight.LoadIDs, ID);
            if (index == -1)
                continue;
            flight.LoadIDs[index] = newID;
            break;
        }
        
        ID = newID;
        _storage.Add(this);
    }

    public void UpdateClass(IComparable value)
    {
        string newClass = (string)value;
        Class = newClass;
    }

    public void UpdateMiles(IComparable value)
    {
        ulong newMiles = (ulong)value;
        Miles = newMiles;
    }
    
    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.Class, UpdateClass);
        _updateFuncs.Add(FieldsNames.Miles, UpdateMiles);
    }

    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Person.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
