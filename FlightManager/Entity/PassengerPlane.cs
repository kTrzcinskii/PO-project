using FlightManager.NewsSource;
using FlightManager.Query;

namespace FlightManager.Entity;

internal class PassengerPlane : Plane, IReportable
{
    public static class FieldsNames
    {
        public const string FirstClassSize = "FirstClassSize";
        public const string BusinessClassSize = "BusinessClassSize";
        public const string EconomyClassSize = "EconomyClassSize";

        public static List<string> allFields = new List<string>() { FirstClassSize, BusinessClassSize, EconomyClassSize };
    }
    
    private ushort _firstClassSize { get; set; }
    public ushort FirstClassSize
    {
        get => _firstClassSize;
        set
        {
            _firstClassSize = value;
            _fields[FieldsNames.FirstClassSize] = value;
        }
    }
    private ushort _businessClassSize { get; set; }
    public ushort BusinessClassSize
    {
        get => _businessClassSize;
        set
        {
            _businessClassSize = value;
            _fields[FieldsNames.BusinessClassSize] = value;
        }
    }
    private ushort _economyClassSize { get; set; }
    public ushort EconomyClassSize
    {
        get => _economyClassSize;
        set
        {
            _economyClassSize = value;
            _fields[FieldsNames.EconomyClassSize] = value;
        }
    }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public PassengerPlane(ulong iD, string serial, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) : base(iD, serial, countryISO, model)
    {
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
        SetupUpdateFuncs();
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitPassengerPlane(this);
    }

    public string AcceptNewsSource(INewsSource newsSource)
    {
        return newsSource.GetReport(this);
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
        _storage.RemovePassengerPlane(newID);
        
        var flights = _storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            if (flight.PlaneID == ID)
            {
                flight.PlaneID = newID;
            }
        }
        
        ID = newID;
        _storage.Add(this);
    }

    public void UpdateFirstClassSize(IComparable value)
    {
        ushort newFirstClassSize = (ushort)value;
        FirstClassSize = newFirstClassSize;
    }
    
    public void UpdateBusinessClassSize(IComparable value)
    {
        ushort newBusinessClassSize = (ushort)value;
        BusinessClassSize = newBusinessClassSize;
    }
    
    public void UpdateEconomyClassSize(IComparable value)
    {
        ushort newEconomyClassSize = (ushort)value;
        EconomyClassSize = newEconomyClassSize;
    }

    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.FirstClassSize, UpdateFirstClassSize);
        _updateFuncs.Add(FieldsNames.BusinessClassSize, UpdateBusinessClassSize);
        _updateFuncs.Add(FieldsNames.EconomyClassSize, UpdateEconomyClassSize);
    }

    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Plane.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
