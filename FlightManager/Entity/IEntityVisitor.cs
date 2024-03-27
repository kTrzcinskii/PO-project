namespace FlightManager.Entity;
internal interface IEntityVisitor
{
    public void VisitAirport(Airport airport);
    public void VisitCargo(Cargo cargo);
    public void VisitCargoPlane(CargoPlane cargoPlane);
    public void VisitCrew(Crew crew);
    public void VisitFlight(Flight flight);
    public void VisitPassenger(Passenger passenger);
    public void VisitPassengerPlane(PassengerPlane plane);
}
