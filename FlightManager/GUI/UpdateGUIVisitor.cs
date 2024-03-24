using FlightManager.Entity;

namespace FlightManager.GUI;
internal class UpdateGUIVisitor : IEntityVisitor
{
    public List<Flight> Flights { get; private set; }
    public Dictionary<ulong, Airport> Airports { get; private set; }

    public UpdateGUIVisitor()
    {
        Flights = new List<Flight>();
        Airports = new Dictionary<ulong, Airport>();
    }

    public void VisitAirport(Airport airport)
    {
        Airports.Add(airport.ID, airport);
    }

    public void VisitFlight(Flight flight)
    {
        Flights.Add(flight);
    }

    public void VisitCargo(Cargo cargo)
    { }

    public void VisitCargoPlane(CargoPlane cargoPlane)
    { }

    public void VisitCrew(Crew crew)
    { }

    public void VisitPassenger(Passenger passenger)
    { }

    public void VisitPassengerPlane(Plane plane)
    { }
}
