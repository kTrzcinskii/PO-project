using FlightManager.Entity;
using FlightManager.Factory;
using NSS = NetworkSourceSimulator;

namespace FlightManager.DataLoader;
internal class NetworkSourceSimulatorDataLoader : IDataLoader
{
    private static readonly int minOffsetInMs = 10;
    private static readonly int maxOffsetInMs = 50;
    private static readonly int entityCodeLength = 3;

    private Dictionary<string, IFactory>? factories;
    private NSS.NetworkSourceSimulator? server;
    private object? entLock;
    private IList<IEntity>? ents;

    public void Load(string dataPath, IList<IEntity> entities, object? entitiesLock = null)
    {
        factories = IFactory.CreateFactoriesContainer();
        entLock = entitiesLock;
        ents = entities;
        server = new NSS.NetworkSourceSimulator(dataPath, minOffsetInMs, maxOffsetInMs);
        server.OnNewDataReady += NewDataReadyHandler;
        Task.Run(server.Run);
    }

    private void NewDataReadyHandler(object sender, NSS.NewDataReadyArgs args)
    {
        var data = server!.GetMessageAt(args.MessageIndex).MessageBytes;
        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream, new System.Text.ASCIIEncoding());

        string entityName = MessageCodeToEntityIdentifier(new string(reader.ReadChars(entityCodeLength)));
        uint messageLength = reader.ReadUInt32();
        byte[] parameters = new byte[messageLength];
        Array.Copy(data, memStream.Position, parameters, 0, messageLength);

        IFactory factory = factories![entityName];
        IEntity newEntity = factory.CreateInstance(parameters);
        lock (entLock!)
        {
            ents!.Add(newEntity);
        }
    }

    private static string MessageCodeToEntityIdentifier(string code) => code switch
    {
        "NCR" => EntitiesIdentifiers.CrewID,
        "NPA" => EntitiesIdentifiers.PassengerID,
        "NCA" => EntitiesIdentifiers.CargoID,
        "NCP" => EntitiesIdentifiers.CargoPlaneID,
        "NPP" => EntitiesIdentifiers.PassengerPlaneID,
        "NAI" => EntitiesIdentifiers.AirportID,
        "NFL" => EntitiesIdentifiers.FlightID,
        _ => throw new ArgumentException("Invalid code")
    };
}
