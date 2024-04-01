using FlightManager.Entity;

namespace FlightManager.NewsSource;
internal class Newspaper : INewsSource
{
    public string Name { get; init; }

    public Newspaper(string name)
    {
        Name = name;
    }

    public string GetReport(Airport airport)
    {
        return $"{Name} - A report from {airport.Name} airport, {airport.CountryISO}";
    }

    public string GetReport(CargoPlane cargoPlane)
    {
        return $"{Name} - An interview with the crew of {cargoPlane.Serial}";
    }

    public string GetReport(PassengerPlane passengerPlane)
    {
        return $"{Name} - Breaking News! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}";
    }
}
