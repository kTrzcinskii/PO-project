using FlightManager.Entity;

namespace FlightManager.NewsSource;
internal class Television : INewsSource
{
    public string Name { get; init; }

    public Television(string name)
    {
        Name = name;
    }

    public string GetReport(Airport airport)
    {
        return $"An image of {airport.Name} airport";
    }

    public string GetReport(CargoPlane cargoPlane)
    {
        return $"An image of {cargoPlane.Serial} cargo plane";
    }

    public string GetReport(PassengerPlane passengerPlane)
    {
        return $"An image of {passengerPlane.Serial} passenger plane";
    }
}
