using FlightManager.Entity;
using FlightManager.Factory;

namespace FlightManager.DataLoader;
internal class FTRFileDataLoader : IDataLoader
{
    public void Load(string dataPath, IList<IEntity> entities, object? entitiesLock = null)
    {
        var factories = IFactory.CreateFactoriesContainer();
        var fileContentLines = File.ReadAllLines(dataPath);

        foreach (string line in fileContentLines)
        {
            var parsedData = ParseEntry(line);
            (var entityName, var parameters) = parsedData;
            IFactory factory = factories[entityName];
            entities.Add(factory.CreateInstance(parameters));
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
