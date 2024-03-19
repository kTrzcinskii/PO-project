using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;
using FlightManager.EntityFactory;
using NSS = NetworkSourceSimulator;

namespace FlightManager.DataLoader;
internal class NetworkSourceSimulatorDataLoader : IDataLoader
{
    private static readonly int minOffsetInMs = 10;
    private static readonly int maxOffsetInMs = 50;
    private static readonly int entityCodeLength = 3;

    private readonly Dictionary<string, Factory> factories;
    private object? entLock;
    private IList<IEntity>? ents;
    private NSS.NetworkSourceSimulator? server;

    public NetworkSourceSimulatorDataLoader()
    {
        factories = CreateFactoriesContainer();
    }

    public void Load(string dataPath, IList<IEntity> entities, object? entitiesLock = null)
    {
        entLock = entitiesLock;
        ents = entities;
        server = new NSS.NetworkSourceSimulator(dataPath, minOffsetInMs, maxOffsetInMs);
        server.OnNewDataReady += NewDataReadyHandler;
        Task.Run(server.Run);
    }

    private void NewDataReadyHandler(object _, NSS.NewDataReadyArgs args)
    {
        var data = server!.GetMessageAt(args.MessageIndex).MessageBytes;
        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream, new System.Text.ASCIIEncoding());

        string entityName = ParametersFormatter.ReadStringFromBytes(reader, entityCodeLength);
        uint messageLength = reader.ReadUInt32();
        byte[] parameters = new byte[messageLength];
        Array.Copy(data, memStream.Position, parameters, 0, messageLength);

        Factory factory = factories[entityName];
        IEntity newEntity = factory.CreateInstance(parameters);
        lock (entLock!)
        {
            ents!.Add(newEntity);
        }
    }

    private static Dictionary<string, Factory> CreateFactoriesContainer()
    {
        var factories = new Dictionary<string, Factory> { { EntitiesIdentifiers.NewAirportID, new AirportFactory() }, { EntitiesIdentifiers.NewCargoID, new CargoFactory() }, { EntitiesIdentifiers.NewCargoPlaneID, new CargoPlaneFactory() },
        { EntitiesIdentifiers.NewCrewID, new CrewFactory() }, { EntitiesIdentifiers.NewFlightID, new FlightFactory() }, { EntitiesIdentifiers.NewPassengerID, new PassengerFactory() }, { EntitiesIdentifiers.NewPassengerPlaneID, new PassengerPlaneFactory() }    };
        return factories;
    }
}
