using FlightManager.Entity;

namespace FlightManager.DataUpdater.NSSUpdater;
using NSS = NetworkSourceSimulator;

internal class UpdateIDVisitor : IEntityVisitor
{
    // TODO: updaet IDs in classes in which they are strored in arrays
    public NSS.IDUpdateArgs Args { get; set; }
    public void VisitAirport(Airport airport)
    {
        airport.ID = Args.NewObjectID;
    }

    public void VisitCargo(Cargo cargo)
    {
        cargo.ID = Args.NewObjectID;
    }

    public void VisitCargoPlane(CargoPlane cargoPlane)
    {
        cargoPlane.ID = Args.NewObjectID;
    }

    public void VisitCrew(Crew crew)
    {
        crew.ID = Args.NewObjectID;
    }

    public void VisitFlight(Flight flight)
    {
       flight.ID = Args.NewObjectID;
    }

    public void VisitPassenger(Passenger passenger)
    {
        passenger.ID = Args.NewObjectID;
    }

    public void VisitPassengerPlane(PassengerPlane plane)
    {
        plane.ID = Args.NewObjectID;
    }
}