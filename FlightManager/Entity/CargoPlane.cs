using FlightManager.NewsSource;
using FlightManager.Query;

namespace FlightManager.Entity;

internal class CargoPlane : Plane, IReportable
{
    public float MaxLoad { get; init; }

    public CargoPlane(ulong iD, string serial, string countryISO, string model, float maxLoad) : base(iD, serial, countryISO, model)
    {
        MaxLoad = maxLoad;
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
        switch (condition.Property)
        {
            case "MaxLoad":
                return condition.Check(MaxLoad);
            default:
                return base.MatchCondition(condition);
        }
    }
}
