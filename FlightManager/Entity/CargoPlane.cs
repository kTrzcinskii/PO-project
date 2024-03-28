using FlightManager.NewsSource;

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

    public void AcceptNewsSource(INewsSource newsSource)
    {
        newsSource.GetReport(this);
    }
}
