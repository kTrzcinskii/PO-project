using FlightManager.Entity;

namespace FlightManager.NewsSource;
internal class Radio : INewsSource
{
    public string Name { get; init; }

    public Radio(string name)
    {
        Name = name;
    }

    public string GetReport(Airport airport)
    {
        return $"Reporting for {Name}, Ladies and gentelmen, we are at {airport.Name} airport.";
    }

    public string GetReport(CargoPlane cargoPlane)
    {
        return $"Reporting for {Name}, Ladies and gentelmen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
    }

    public string GetReport(PassengerPlane passengerPlane)
    {
        return $"Reporting for {Name}, Ladies and gentelmen, we've just witnessed {passengerPlane.Serial} take off.";
    }
}
