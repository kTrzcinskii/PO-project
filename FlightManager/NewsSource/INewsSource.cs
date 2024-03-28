using FlightManager.Entity;

namespace FlightManager.NewsSource;
internal interface INewsSource
{
    public string Name { get; init; }
    public string GetReport(Airport airport);
    public string GetReport(CargoPlane cargoPlane);
    public string GetReport(PassengerPlane passengerPlane);
}
