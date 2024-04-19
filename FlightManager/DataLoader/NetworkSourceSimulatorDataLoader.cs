using FlightManager.Entity;
using FlightManager.EntityArgumentsParser;
using FlightManager.EntityFactory;
using FlightManager.Storage;
using NSS = NetworkSourceSimulator;

namespace FlightManager.DataLoader;
internal class NetworkSourceSimulatorDataLoader : IDataLoader
{
    private static readonly int minOffsetInMs = 1;
    private static readonly int maxOffsetInMs = 2;
    private static readonly int entityCodeLength = 3;

    private readonly Dictionary<string, Factory> factories;
    private NSS.NetworkSourceSimulator? server;
    private AddToStorageVisitor addToStorageVisitor = new AddToStorageVisitor();

    public NetworkSourceSimulatorDataLoader()
    {
        factories = Factory.CreateFactoriesContainer();
    }

    public void Load(string dataPath)
    {
        server = new NSS.NetworkSourceSimulator(dataPath, minOffsetInMs, maxOffsetInMs);
        server.OnNewDataReady += NewDataReadyHandler;
        Task.Run( () =>
        {
            server.Run();
            DataLoaded?.Invoke(this, EventArgs.Empty);
        });
    }

    public event IDataLoader.OnDataLoaded? DataLoaded;

    private void NewDataReadyHandler(object _, NSS.NewDataReadyArgs args)
    {
        var data = server!.GetMessageAt(args.MessageIndex).MessageBytes;
        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream, new System.Text.ASCIIEncoding());

        string entityName = EntitiesIdentifiers.NewEntityIdentifier[ParametersFormatter.ReadStringFromBytes(reader, entityCodeLength)];
        uint messageLength = reader.ReadUInt32();
        byte[] parameters = new byte[messageLength];
        Array.Copy(data, memStream.Position, parameters, 0, messageLength);

        Factory factory = factories[entityName];
        IEntity newEntity = factory.CreateInstance(parameters);
        newEntity.AcceptVisitor(addToStorageVisitor);
    }
}
