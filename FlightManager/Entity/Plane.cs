using FlightManager.Query;
using FlightManager.Storage;

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

    protected EntityStorage _storage;

    public Plane(ulong iD, string serial, string countryISO, string model)
    {
        ID = iD;
        Serial = serial;
        CountryISO = countryISO;
        Model = model;
        _storage = EntityStorage.GetStorage();
        SetupUpdateFuncs();
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
        if (!_updateFuncs.ContainsKey(fieldName))
        {
            throw new ArgumentException($"Invalid fieldName {fieldName}");
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

    public abstract void UpdateID(IComparable value);

    public void UpdateSerial(IComparable value)
    {
        string newSerial = (string)value;
        Serial = newSerial;
    }

    public void UpdateCountryISO(IComparable value)
    {
        string newCountryISO = (string)value;
        CountryISO = newCountryISO;
    }

    public void UpdateModel(IComparable value)
    {
        string newModel = (string)value;
        Model = newModel;
    }
    
    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.ID, UpdateID);
        _updateFuncs.Add(FieldsNames.Serial, UpdateSerial);
        _updateFuncs.Add(FieldsNames.CountryISO, UpdateCountryISO);
        _updateFuncs.Add(FieldsNames.Model, UpdateModel);
    }
}
