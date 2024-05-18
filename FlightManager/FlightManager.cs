using FlightManager.DataLoader;
using FlightManager.DataSerializer;
using FlightManager.GUI;
using FlightManager.NewsSource;
using FlightManager.Storage;
using FlightTrackerGUI;
using System.Timers;
using FlightManager.DataUpdater;
using FlightManager.Query;

namespace FlightManager;
internal class FlightManager
{
    private IDataLoader DataLoader { get; set; }
    private IDataSerializer DataSerializer { get; set; }
    private IDataUpdater? DataUpdater { get; set; }
    private EntityStorage Storage { get; set; }
    private QueryFactory QueryCreator { get; init; }
    private const string EXIT_COMMAND = "exit";
    private const string SNAPSHOT_COMMAND = "print";
    private const string REPORT_COMMAND = "report";
    public const int REFRESH_SCREEN_MS = 1000;

    public FlightManager(IDataLoader dataLoader, IDataSerializer dataSerializer, IDataUpdater? dataUpdater = null, string? dataUpdateSource = null)
    {
        DataLoader = dataLoader;
        DataSerializer = dataSerializer;
        if (dataUpdater != null && dataUpdateSource != null)
        {
            DataUpdater = dataUpdater;
            DataLoader.DataLoaded += (object _, EventArgs __) => dataUpdater.StartUpdateLoop(dataUpdateSource);
        }
        Storage = EntityStorage.GetStorage();
        CreateNewsSources();
        QueryCreator = new QueryFactory();
    }

    public void StartApp(string dataPath)
    {
        Logger logger = Logger.GetLogger();
        logger.StartLogger();
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
                case REPORT_COMMAND:
                    HandleReport();
                    break;
                default:
                    HandleQuery(command);
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
        SaveEntities(filePath);
    }

    private void LoadEntities(string dataPath)
    {
        DataLoader.Load(dataPath);
    }

    private void SaveEntities(string outputPath)
    {
        var jsonData = DataSerializer.SerializeData([.. Storage.GetAll()]);
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
        var list = new List<FlightGUI>();
        foreach (var (_, flight) in Storage.GetAllFlights())
        {
            // Airport hasn't been loaded yet
            var originAirport = Storage.GetAirport(flight.OriginID);
            if (originAirport == null)
                continue;
            var targetAirport = Storage.GetAirport(flight.TargetID);
            if (targetAirport == null)
                continue;

            list.Add(new FlightGUIAdapter(flight, originAirport, targetAirport));
        }
        Runner.UpdateGUI(new FlightsGUIData(list));
    }

    private void CreateNewsSources()
    {
        Storage.Add(new Television("Telewizja Abelowa"));
        Storage.Add(new Television("Kanał TV-tensor"));
        Storage.Add(new Radio("Radio Kwantyfikator"));
        Storage.Add(new Radio("Radio Shmem"));
        Storage.Add(new Newspaper("Gazeta Kategoryczna"));
        Storage.Add(new Newspaper("Dziennik Politechniczny"));
    }

    private void HandleReport()
    {
        var generator = new NewsGenerator(Storage.GetNewsSources(), Storage.GetReportables());
        string? report;
        while ((report = generator.GenerateNextNews()) != null)
        {
            Console.WriteLine(report);
        }
    }

    private void HandleQuery(string queryCommand)
    {
        try
        {
            IQuery query = QueryCreator.CreateQuery(queryCommand);
            query.Execute();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid syntax: {ex.Message}\n");
        }
    }
}
