using FlightManager.Entity;
using FlightManager.EntityFactory;

namespace FlightManager.DataLoader;
internal class FTRFileDataLoader : IDataLoader
{

    private readonly Dictionary<string, Factory> factories;
    public FTRFileDataLoader()
    {
        factories = Factory.CreateFactoriesContainer();
    }

    public void Load(string dataPath, IList<IEntity> entities, object? entitiesLock = null)
    {
        var fileContentLines = File.ReadAllLines(dataPath);

        foreach (string line in fileContentLines)
        {
            var parsedData = ParseEntry(line);
            (var entityName, var parameters) = parsedData;
            Factory factory = factories[entityName];
            IEntity newEntity = factory.CreateInstance(parameters);
            entities.Add(newEntity);
        }
    }

    private (string, string[]) ParseEntry(string data)
    {
        const string divider = ",";
        var splittedLine = data.Split(divider);
        var enitityName = splittedLine[0];
        var parameters = splittedLine[1..];
        return (enitityName, parameters);
    }
}
