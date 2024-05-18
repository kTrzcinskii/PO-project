using FlightManager.Query;

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
    
    public ulong ID { get; set; }
    public string Name { get; init; }
    public ulong Age { get; init; }
    public string Phone { get; set; }
    public string Email { get; set; }

    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();
    
    public Person(ulong iD, string name, ulong age, string phone, string email)
    {
        ID = iD;
        _fields.Add(FieldsNames.ID, ID);
        Name = name;
        _fields.Add(FieldsNames.Name, Name);
        Age = age;
        _fields.Add(FieldsNames.Age, Age);
        Phone = phone;
        _fields.Add(FieldsNames.Phone, Phone);
        Email = email;
        _fields.Add(FieldsNames.Email, Email);
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
}
