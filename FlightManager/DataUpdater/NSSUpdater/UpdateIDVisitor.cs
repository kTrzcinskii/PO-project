using FlightManager.Entity;
using FlightManager.Storage;
using NSS = NetworkSourceSimulator;

namespace FlightManager.DataUpdater.NSSUpdater;

internal class UpdateIDVisitor : IEntityVisitor
{
    public NSS.IDUpdateArgs Args { get; set; }
    private readonly EntityStorage storage = EntityStorage.GetStorage();
    public void VisitAirport(Airport airport)
    {
        storage.RemoveAirport(airport.ID);
        airport.ID = Args.NewObjectID;
        UpdateAirportID(Args.ObjectID, Args.NewObjectID);
        storage.Add(airport);
    }

    public void VisitCargo(Cargo cargo)
    {
        storage.RemoveCargo(cargo.ID);
        cargo.ID = Args.NewObjectID;
        UpdateLoadIDs(Args.ObjectID, Args.NewObjectID);
        storage.Add(cargo);
    }

    public void VisitCargoPlane(CargoPlane cargoPlane)
    {
        storage.RemoveCargoPlane(cargoPlane.ID);
        cargoPlane.ID = Args.NewObjectID;
        UpdatePlaneID(Args.ObjectID, Args.NewObjectID);
        storage.Add(cargoPlane);
    }

    public void VisitCrew(Crew crew)
    {
        storage.RemoveCrew(crew.ID);
        crew.ID = Args.NewObjectID;
        UpdateCrewIDs(Args.ObjectID, Args.NewObjectID);
        storage.Add(crew);
    }

    public void VisitFlight(Flight flight)
    {  
        storage.RemoveFlight(flight.ID);
        flight.ID = Args.NewObjectID;
        storage.Add(flight);
    }

    public void VisitPassenger(Passenger passenger)
    {
        storage.RemovePassenger(passenger.ID);
        passenger.ID = Args.NewObjectID;
        UpdateLoadIDs(Args.ObjectID, Args.NewObjectID);
        storage.Add(passenger);
    }

    public void VisitPassengerPlane(PassengerPlane plane)
    {
        storage.RemovePassengerPlane(plane.ID);
        plane.ID = Args.NewObjectID;
        UpdatePlaneID(Args.ObjectID, Args.NewObjectID);
        storage.Add(plane);
    }

    private void UpdateCrewIDs(ulong previousCrewId, ulong newCrewId)
    {
        var flights = storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            int index = Array.IndexOf(flight.CrewIDs, previousCrewId);
            if (index == -1)
                continue;
            flight.CrewIDs[index] = newCrewId;
            break;
        }
    }
    
    private void UpdateLoadIDs(ulong previousLoadId, ulong newLoadId)
    {
        var flights = storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            int index = Array.IndexOf(flight.LoadIDs, previousLoadId);
            if (index == -1)
                continue;
            flight.LoadIDs[index] = newLoadId;
            break;
        }
    }

    private void UpdatePlaneID(ulong previousPlaneId, ulong newPlaneId)
    {
        var flights = storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            if (flight.PlaneID == previousPlaneId)
            {
                flight.PlaneID = newPlaneId;
            }
        }
    }

    private void UpdateAirportID(ulong previousAirportId, ulong newAirportId)
    {
        var flights = storage.GetAllFlights();
        foreach (var (_, flight) in flights)
        {
            if (flight.OriginID == previousAirportId)
            {
                flight.OriginID = newAirportId;
            }
            if (flight.TargetID == previousAirportId)
            {
                flight.TargetID = newAirportId;
            }
        }
    }
}