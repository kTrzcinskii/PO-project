using FlightManager.NewsSource;

namespace FlightManager.Entity;

internal class Airport : IEntity, IReportable
{
    public ulong ID { get; init; }
    public string Name { get; init; }
    public string Code { get; init; }
    public float Longitude { get; init; }
    public float Latitude { get; init; }
    public float AMSL { get; init; }
    public string CountryISO { get; init; }

    public Airport(ulong iD, string name, string code, float longitude, float latitude, float aMSL, string countryISO)
    {
        ID = iD;
        Name = name;
        Code = code;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = aMSL;
        CountryISO = countryISO;
    }

    public void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitAirport(this);
    }

    public void AcceptNewsSource(INewsSource newsSource)
    {
        newsSource.GetReport(this);
    }
}
