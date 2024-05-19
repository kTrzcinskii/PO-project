using FlightManager.Query;
using FlightManager.Storage;

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

    private EntityStorage _storage;
    public Cargo(ulong iD, float weight, string code, string description)
    {
        ID = iD;
        Weight = weight;
        Code = code;
        Description = description;
        SetupUpdateFuncs();
        _storage = EntityStorage.GetStorage();
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
        if (!_updateFuncs.ContainsKey(fieldName))
            throw new ArgumentException($"Invalid fieldName {fieldName}");
        try
        {
            _updateFuncs[fieldName].Invoke(value);
        }
        catch (Exception)
        {
            throw new ArgumentException($"Couldnt assign {value} to {fieldName}");
        }
    }

    public void UpdateID(IComparable value)
    {
        ulong newID = (ulong)value;
        _storage.RemoveCargo(ID);
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

    public void UpdateWeight(IComparable value)
    {
        float newWeight = (float)value;
        Weight = newWeight;
    }

    public void UpdateCode(IComparable value)
    {
        string newCode = (string)value;
        Code = newCode;
    }

    public void UpdateDescription(IComparable value)
    {
        string newDescription = (string)value;
        Description = newDescription;
    }
    
    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.ID, UpdateID);
        _updateFuncs.Add(FieldsNames.Weight, UpdateWeight);
        _updateFuncs.Add(FieldsNames.Code, UpdateCode);
        _updateFuncs.Add(FieldsNames.Description, UpdateDescription);
    }
}
