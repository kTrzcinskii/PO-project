using FlightManager.Entity;

namespace FlightManager.Storage;
internal class AddToStorageVisitor : IEntityVisitor
{
    private EntityStorage storage;

    public AddToStorageVisitor()
    {
        storage = EntityStorage.GetStorage();
    }

    public void VisitAirport(Airport airport)
    {
        storage.Add(airport);
    }

    public void VisitCargo(Cargo cargo)
    {
        storage.Add(cargo);
    }

    public void VisitCargoPlane(CargoPlane cargoPlane)
    {
        storage.Add(cargoPlane);
    }

    public void VisitCrew(Crew crew)
    {
        storage.Add(crew);
    }

    public void VisitFlight(Flight flight)
    {
        storage.Add(flight);
    }

    public void VisitPassenger(Passenger passenger)
    {
        storage.Add(passenger);
    }

    public void VisitPassengerPlane(PassengerPlane passengerPlane)
    {
        storage.Add(passengerPlane);
    }
}
