using FlightManager.Entity;
using FlightManager.Storage;

namespace FlightManager.Query;

internal class DeletingVisitor : IEntityVisitor
{
    private EntityStorage _storage;
    
    public DeletingVisitor()
    {
        _storage = EntityStorage.GetStorage();
    }
    
    public void VisitAirport(Airport airport)
    {
        _storage.RemoveAirport(airport.ID);
    }

    public void VisitCargo(Cargo cargo)
    {
        _storage.RemoveCargo(cargo.ID);
    }

    public void VisitCargoPlane(CargoPlane cargoPlane)
    {
        _storage.RemoveCargoPlane(cargoPlane.ID);
    }

    public void VisitCrew(Crew crew)
    {
        _storage.RemoveCrew(crew.ID);
    }

    public void VisitFlight(Flight flight)
    {
        _storage.RemoveFlight(flight.ID);
    }

    public void VisitPassenger(Passenger passenger)
    {
        _storage.RemovePassenger(passenger.ID);
    }

    public void VisitPassengerPlane(PassengerPlane plane)
    {
        _storage.RemovePassengerPlane(plane.ID);
    }
}