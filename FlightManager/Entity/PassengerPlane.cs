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
    
    public ushort FirstClassSize { get; init; }
    public ushort BusinessClassSize { get; init; }
    public ushort EconomyClassSize { get; init; }
    
    private Dictionary<string, IComparable> _fields = new Dictionary<string, IComparable>();

    public PassengerPlane(ulong iD, string serial, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) : base(iD, serial, countryISO, model)
    {
        FirstClassSize = firstClassSize;
        _fields.Add(FieldsNames.FirstClassSize, FirstClassSize);
        BusinessClassSize = businessClassSize;
        _fields.Add(FieldsNames.BusinessClassSize, BusinessClassSize);
        EconomyClassSize = economyClassSize;
        _fields.Add(FieldsNames.EconomyClassSize, EconomyClassSize);
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
    
    public new static List<string> GetAllFieldsNames()
    {
        var all = new List<string>();
        all.AddRange(FieldsNames.allFields);
        all.AddRange(Plane.GetAllFieldsNames());
        return all;
    }
}
