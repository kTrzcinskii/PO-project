using FlightManager.DataParser;
using FlightManager.DataSerializer;
using FlightManager.Entity;
using FlightManager.Factory;

namespace FlightManager;
internal class FlightManager
{
    private IDataParser DataParser { get; set; }
    private IDataSerializer DataSerializer { get; set; }
    private List<IEntity> Entities { get; init; }
    private Dictionary<string, IFactory> Factories { get; init; }

    public FlightManager(IDataParser dataParser, IDataSerializer dataSerializer)
    {
        DataParser = dataParser;
        DataSerializer = dataSerializer;
        Entities = new List<IEntity>();
        Factories = CreateFactoriesContainer();
    }

    public void LoadEntities(string dataPath)
    {
        Entities.Clear();
        var parsedData = DataParser.ParseData(dataPath);
        foreach (var entityData in parsedData)
        {
            (var entityName, var parameters) = entityData;
            IFactory factory = Factories[entityName];
            Entities.Add(factory.CreateInstance(parameters));
        }
    }

    public void SerializeEntities(string outputPath)
    {
        DataSerializer.SerializeData([.. Entities], outputPath);
    }

    public void ChangeDataParser(IDataParser newDataParser)
    {
        DataParser = newDataParser;
    }

    public void ChangeDataSerializer(IDataSerializer newDataSerializer)
    {
        DataSerializer = newDataSerializer;
    }

    private static Dictionary<string, IFactory> CreateFactoriesContainer()
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
