using FlightManager.Entity;

namespace FlightManager.DataUpdater.NSSUpdater;
using NSS = NetworkSourceSimulator;

internal class UpdateContactInfoVisitor : IEntityVisitor 
{
    public NSS.ContactInfoUpdateArgs Args { get; set; }
    
    public void VisitAirport(Airport airport)
    {
        throw new InvalidOperationException();
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
        crew.Email = Args.EmailAddress;
        crew.Phone = Args.PhoneNumber;
    }

    public void VisitFlight(Flight flight)
    {
        throw new InvalidOperationException();
    }

    public void VisitPassenger(Passenger passenger)
    {
        passenger.Email = Args.EmailAddress;
        passenger.Phone = Args.PhoneNumber;
    }

    public void VisitPassengerPlane(PassengerPlane plane)
    {
        throw new InvalidOperationException();
    }
}