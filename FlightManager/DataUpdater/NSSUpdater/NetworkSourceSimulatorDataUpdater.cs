using FlightManager.Entity;
using FlightManager.Storage;
using NSS = NetworkSourceSimulator;

namespace FlightManager.DataUpdater.NSSUpdater;

internal class NetworkSourceSimulatorDataUpdater : IDataUpdater
{
    private static readonly int minOffsetInMs = 1;
    private static readonly int maxOffsetInMs = 5;
    private readonly UpdatePositionVisitor positionVisitor = new UpdatePositionVisitor();
    private readonly UpdateContactInfoVisitor contactInfoVisitor = new UpdateContactInfoVisitor();
    private readonly UpdateIDVisitor idVisitor = new UpdateIDVisitor();
    
    public void StartUpdateLoop(string sourcePath)
    {
        var server = new NSS.NetworkSourceSimulator(sourcePath, minOffsetInMs, maxOffsetInMs);
        server.OnPositionUpdate += HandlePositionUpdate;
        server.OnContactInfoUpdate += HandleContactInfoUpdate;
        server.OnIDUpdate += HandleIDUpdate;
        Task.Run(server.Run);
    }
    
    private void HandlePositionUpdate(object _, NSS.PositionUpdateArgs args)
    {
        var storage = EntityStorage.GetStorage();
        var entity = storage.GetByID(args.ObjectID);
        if (entity == null)
        {
            // handle error in logs
            return;
        }
        positionVisitor.Args = args;
        try
        {
            entity.AcceptVisitor(positionVisitor);
        }
        catch (InvalidOperationException e)
        {
            // handle error in logs
        }
    }

    private void HandleContactInfoUpdate(object _, NSS.ContactInfoUpdateArgs args)
    {
        var storage = EntityStorage.GetStorage();
        var entity = storage.GetByID(args.ObjectID);
        if (entity == null)
        {
            // handle error in logs
            return;
        }

        contactInfoVisitor.Args = args;
        try
        {
            entity.AcceptVisitor(contactInfoVisitor);
        }
        catch (InvalidOperationException e)
        {
            // handle error in logs
        }
    }
    
    private void HandleIDUpdate(object _, NSS.IDUpdateArgs args)
    {
        var storage = EntityStorage.GetStorage();
        var entity = storage.GetByID(args.ObjectID);
        if (entity == null)
        {
            // handle error in logs
            return;
        }

        idVisitor.Args = args;
        try
        {
            entity.AcceptVisitor(idVisitor);
        }
        catch (InvalidOperationException e)
        {
            // handle error in logs
        }
    }
}