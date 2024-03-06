using FlightManager.Entity;

namespace FlightManager.Factory;

internal interface IFactory
{
    public string EntityName { get; }
    public IEntity CreateInstance(string[] parameters);
    public IEntity CreateInstance(byte[] parameters);
    public static Dictionary<string, IFactory> CreateFactoriesContainer()
    {
        var airportFactory = new AirportFactory();
        var cargoFactory = new CargoFactory();
        var cargoPlaneFactory = new CargoPlaneFactory();
        var crewFactory = new CrewFactory();
        var flightFactory = new FlightFactory();
        var passengerFactory = new PassengerFactory();
        var passengerPlaneFactory = new PassengerPlaneFactory();
        Dictionary<string, IFactory> factories = new Dictionary<string, IFactory>() { { airportFactory.EntityName, airportFactory }, { cargoFactory.EntityName, cargoFactory }, { cargoPlaneFactory.EntityName, cargoPlaneFactory }, { crewFactory.EntityName, crewFactory }, { flightFactory.EntityName, flightFactory }, { passengerFactory.EntityName, passengerFactory }, { passengerPlaneFactory.EntityName, passengerPlaneFactory } };
        return factories;
    }
}
