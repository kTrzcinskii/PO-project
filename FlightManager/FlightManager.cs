using FlightManager.DataLoader;
using FlightManager.DataSerializer;
using FlightManager.Entity;

namespace FlightManager;
internal class FlightManager
{
    private List<IEntity> Entities { get; init; }
    private IDataLoader DataLoader { get; set; }
    private IDataSerializer DataSerializer { get; set; }
    private readonly object entitiesLock = new object();
    private const string EXIT_COMMAND = "exit";
    private const string SNAPSHOT_COMMAND = "print";

    public FlightManager(IDataLoader dataLoader, IDataSerializer dataSerializer)
    {
        Entities = new List<IEntity>();
        DataLoader = dataLoader;
        DataSerializer = dataSerializer;
    }

    public void StartApp(string dataPath)
    {
        LoadEntities(dataPath);
        HandleApplicationInput();
    }

    private void HandleApplicationInput()
    {
        string command;
        while (true)
        {
            Console.Write("# ");
            command = Console.ReadLine() ?? "";
            switch (command)
            {
                case EXIT_COMMAND:
                    HandleExit();
                    break;
                case SNAPSHOT_COMMAND:
                    HandleSnapshot();
                    break;
            }
        }
    }

    private void HandleExit()
    {
        Environment.Exit(0);
    }

    private void HandleSnapshot()
    {
        string time = DateTime.Now.ToString("HH_mm_ss");
        string filePath = $"snapshot_{time}.json";
        lock (entitiesLock)
        {
            SaveEntities(filePath);
        }
    }

    private void LoadEntities(string dataPath)
    {
        DataLoader.Load(dataPath, Entities, entitiesLock);
    }

    private void SaveEntities(string outputPath)
    {
        var jsonData = DataSerializer.SerializeData([.. Entities]);
        File.WriteAllText(outputPath, jsonData);
    }
}
