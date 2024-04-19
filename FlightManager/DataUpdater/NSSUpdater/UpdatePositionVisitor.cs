using FlightManager.Entity;

namespace FlightManager.DataUpdater.NSSUpdater;
using NSS = NetworkSourceSimulator;

internal class UpdatePositionVisitor : IEntityVisitor
{
    public NSS.PositionUpdateArgs Args { get; set; }
    
    public void VisitAirport(Airport airport)
    {
        airport.Latitude = Args.Latitude;
        airport.Longitude = Args.Longitude;
        airport.AMSL = Args.AMSL;
    }

    public void VisitCargo(Cargo cargo)
    {
        throw new InvalidOperationException();
    }

    public void VisitCargoPlane(CargoPlane cargoPlane)
    {
        throw new InvalidOperationException();
    }

    public void VisitCrew(Crew crew)
    {
        throw new InvalidOperationException();
    }

    public void VisitFlight(Flight flight)
    {
        flight.Latitude = Args.Latitude;
        flight.Longitude = Args.Longitude;
        flight.AMSL = Args.AMSL;
    }

    public void VisitPassenger(Passenger passenger)
    {
        throw new InvalidOperationException();
    }

    public void VisitPassengerPlane(PassengerPlane plane)
    {
        throw new InvalidOperationException();
    }
}