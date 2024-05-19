using FlightManager.Query;
using FlightManager.Storage;

namespace FlightManager.Entity;
internal abstract class Person : IEntity
{
    public static class FieldsNames
    {
        public const string ID = "ID";
        public const string Name = "Name";
        public const string Age = "Age";
        public const string Phone = "Phone";
        public const string Email = "Email";

        public static List<string> allFields = new List<string>() { ID, Name, Age, Phone, Email};
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
    protected string _name { get; set; }
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            _fields[FieldsNames.Name] = value;
        }
    }
    private ulong _age { get; set; }
    public ulong Age
    {
        get => _age;
        set
        {
            _age = value;
            _fields[FieldsNames.Age] = value;
        }
    }
    private string _phone { get; set; }
    public string Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            _fields[FieldsNames.Phone] = value;
        }
    }
    private string _email { get; set; }
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            _fields[FieldsNames.Email] = value;
        }
    }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    protected EntityStorage _storage;
    
    public Person(ulong iD, string name, ulong age, string phone, string email)
    {
        ID = iD;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
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

    public void UpdateName(IComparable value)
    {
        string newName = (string)value;
        Name = newName;
    }

    public void UpdateAge(IComparable value)
    {
        ulong newAge = (ulong)value;
        Age = newAge;
    }

    public void UpdatePhone(IComparable value)
    {
        string newPhone = (string)value;
        Phone = newPhone;
    }

    public void UpdateEmail(IComparable value)
    {
        string newEmail = (string)value;
        Email = newEmail;
    }
    
    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.ID, UpdateID);
        _updateFuncs.Add(FieldsNames.Name, UpdateName);
        _updateFuncs.Add(FieldsNames.Age, UpdateAge);
        _updateFuncs.Add(FieldsNames.Phone, UpdatePhone);
        _updateFuncs.Add(FieldsNames.Email, UpdateEmail);
    }
}
