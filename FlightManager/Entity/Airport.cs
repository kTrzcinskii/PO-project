using FlightManager.NewsSource;

namespace FlightManager.Entity;

internal class Airport : IEntity, IReportable
{
    public ulong ID { get; set; }
    public string Name { get; init; }
    public string Code { get; init; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
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

    public string AcceptNewsSource(INewsSource newsSource)
    {
        return newsSource.GetReport(this);
    }
}
