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
    
    public float MaxLoad { get; init; }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public CargoPlane(ulong iD, string serial, string countryISO, string model, float maxLoad) : base(iD, serial, countryISO, model)
    {
        MaxLoad = maxLoad;
        _fields.Add(FieldsNames.MaxLoad, MaxLoad);
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
    
    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(FieldsNames.allFields);
        all.AddRange(Plane.GetAllFieldsNames());
        return all;
    }
}
