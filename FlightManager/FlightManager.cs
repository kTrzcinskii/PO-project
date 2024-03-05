using FlightManager.DataParser;
using FlightManager.DataSerializer;
using FlightManager.Entity;
using FlightManager.Factory;

namespace FlightManager;
internal class FlightManager
{
    private List<IEntity> Entities { get; init; }
    private Dictionary<string, IFactory> Factories { get; init; }

    public FlightManager()
    {
        Entities = new List<IEntity>();
        Factories = CreateFactoriesContainer();
    }

    public void LoadEntitiesFromTextFile(string dataPath, IDataParser<string, string[]> dataParser)
    {
        Entities.Clear();
        var fileContentLines = File.ReadAllLines(dataPath);

        foreach (string line in fileContentLines)
        {
            var parsedData = dataParser.ParseData(line);
            (var entityName, var parameters) = parsedData;
            IFactory factory = Factories[entityName];
            Entities.Add(factory.CreateInstance(parameters));
        }
    }

    public void SaveEntitiesToFile(string outputPath, IDataSerializer dataSerializer)
    {
        var jsonData = dataSerializer.SerializeData([.. Entities]);
        File.WriteAllText(outputPath, jsonData);
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
