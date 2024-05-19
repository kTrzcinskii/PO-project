using FlightManager.NewsSource;
using FlightManager.Query;

namespace FlightManager.Entity;

internal class CargoPlane : Plane, IReportable
{
    public static class FieldsNames
    {
        public const string MaxLoad = "MaxLoad";

        public static List<string> allFields = new List<string>() { MaxLoad };
    }
    
    private float _maxLoad { get; set; }
    public float MaxLoad
    {
        get => _maxLoad;
        set
        {
            _maxLoad = value;
            _fields[FieldsNames.MaxLoad] = value;
        }
    }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public CargoPlane(ulong iD, string serial, string countryISO, string model, float maxLoad) : base(iD, serial, countryISO, model)
    {
        MaxLoad = maxLoad;
        SetupUpdateFuncs();
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitCargoPlane(this);
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
        _storage.RemoveCargoPlane(newID);
        
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

    public void UpdateMaxLoad(IComparable value)
    {
        float newMaxLoad = (float)value;
        MaxLoad = newMaxLoad;
    }
    
    private Dictionary<string, Action<IComparable>> _updateFuncs = new Dictionary<string, Action<IComparable>>();

    private void SetupUpdateFuncs()
    {
        _updateFuncs.Add(FieldsNames.MaxLoad, UpdateMaxLoad);
    }

    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(Plane.GetAllFieldsNames());
        all.AddRange(FieldsNames.allFields);
        return all;
    }
}
