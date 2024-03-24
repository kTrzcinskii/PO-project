using FlightManager.Entity;

namespace FlightManager.EntityFactory;

internal abstract class Factory
{
    public abstract IEntity CreateInstance(string[] parameters);
    public abstract IEntity CreateInstance(byte[] parameters);
    public static Dictionary<string, Factory> CreateFactoriesContainer()
    {
        var factories = new Dictionary<string, Factory> { { EntitiesIdentifiers.AirportID, new AirportFactory() }, { EntitiesIdentifiers.CargoID, new CargoFactory() }, { EntitiesIdentifiers.CargoPlaneID, new CargoPlaneFactory() },
        { EntitiesIdentifiers.CrewID, new CrewFactory() }, { EntitiesIdentifiers.FlightID, new FlightFactory() }, { EntitiesIdentifiers.PassengerID, new PassengerFactory() }, { EntitiesIdentifiers.PassengerPlaneID, new PassengerPlaneFactory() }    };
        return factories;
    }

}
