using FlightManager.DataLoader;
using FlightManager.DataSerializer;
using FlightManager.Entity;
using FlightManager.GUI;
using FlightTrackerGUI;
using System.Timers;

namespace FlightManager;
internal class FlightManager
{
    private List<IEntity> Entities { get; init; }
    private IDataLoader DataLoader { get; set; }
    private IDataSerializer DataSerializer { get; set; }
    private readonly object entitiesLock = new object();
    private const string EXIT_COMMAND = "exit";
    private const string SNAPSHOT_COMMAND = "print";
    private const int REFRESH_SCREEN_MS = 1000;

    public FlightManager(IDataLoader dataLoader, IDataSerializer dataSerializer)
    {
        Entities = new List<IEntity>();
        DataLoader = dataLoader;
        DataSerializer = dataSerializer;
    }

    public void StartApp(string dataPath)
    {
        LoadEntities(dataPath);
        RunGUI();
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

    private void RunGUI()
    {
        Task.Run(Runner.Run);
        StartUpdateScreen(REFRESH_SCREEN_MS);
    }

    private void StartUpdateScreen(int refreshTimeMs)
    {
        System.Timers.Timer timer = new();
        timer.Interval = refreshTimeMs;
        timer.Elapsed += OnUpdateScreen!;
        timer.Start();
    }

    private void OnUpdateScreen(object _, ElapsedEventArgs __)
    {
        var visitor = new UpdateGUIVisitor();
        lock (entitiesLock)
        {
            foreach (var entity in Entities)
            {
                entity.AcceptVisitor(visitor);
            }
        }
        var list = new List<FlightGUI>();
        foreach (var flight in visitor.Flights)
        {
            // Airport hasn't been loaded yet
            if (!visitor.Airports.ContainsKey(flight.OriginID) || !visitor.Airports.ContainsKey(flight.TargetID))
                continue;
            list.Add(new FlightGUIAdapter(flight, visitor.Airports[flight.OriginID], visitor.Airports[flight.TargetID]));
        }
        Runner.UpdateGUI(new FlightsGUIData(list));
    }
}
