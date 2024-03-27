using FlightManager.Entity;
using FlightManager.EntityFactory;
using FlightManager.Storage;

namespace FlightManager.DataLoader;
internal class FTRFileDataLoader : IDataLoader
{

    private readonly Dictionary<string, Factory> factories;
    public FTRFileDataLoader()
    {
        factories = Factory.CreateFactoriesContainer();
    }

    public void Load(string dataPath)
    {
        var visitor = new AddToStorageVisitor();
        var fileContentLines = File.ReadAllLines(dataPath);

        foreach (string line in fileContentLines)
        {
            var parsedData = ParseEntry(line);
            (var entityName, var parameters) = parsedData;
            Factory factory = factories[entityName];
            IEntity newEntity = factory.CreateInstance(parameters);
            newEntity.AcceptVisitor(visitor);
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
