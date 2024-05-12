using FlightManager.NewsSource;
using FlightManager.Query;

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

    public bool MatchCondition(QueryCondition condition)
    {
        switch (condition.Property)
        {
            case "ID":
                return condition.Check(ID);
            case "Name":
                return condition.Check(Name);
            case "Code":
                return condition.Check(Code);
            case "Longitude":
                return condition.Check(Longitude);
            case "Latitude":
                return condition.Check(Latitude);
            case "AMSL":
                return condition.Check(AMSL);
            case "CountryISO":
                return condition.Check(CountryISO);
        }

        return false;
    }

    public string AcceptNewsSource(INewsSource newsSource)
    {
        return newsSource.GetReport(this);
    }
    
}
