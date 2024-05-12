using FlightManager.NewsSource;
using FlightManager.Query;

namespace FlightManager.Entity;

internal class PassengerPlane : Plane, IReportable
{
    public ushort FirstClassSize { get; init; }
    public ushort BusinessClassSize { get; init; }
    public ushort EconomyClassSize { get; init; }

    public PassengerPlane(ulong iD, string serial, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) : base(iD, serial, countryISO, model)
    {
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
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
        switch (condition.Property)
        {
            case "FirstClassSize":
                return condition.Check(FirstClassSize);
            case "BusinessClassSize":
                return condition.Check(BusinessClassSize);
            case "EconomyClassSize":
                return condition.Check(EconomyClassSize);
            default:
                return base.MatchCondition(condition);
        }
    }
}
