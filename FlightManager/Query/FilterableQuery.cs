using FlightManager.Entity;
using FlightManager.Storage;

namespace FlightManager.Query;

internal abstract class FilterableQuery : IQuery
{
    protected ConditionChain? _conditions;
    protected List<IEntity> _entities;
    protected string _classIdentifier;

    protected FilterableQuery(ConditionChain? conditions, string classIdentifier)
    {
        _conditions = conditions;
        if (!_getFullEntitiesList.ContainsKey(classIdentifier))
            throw new ArgumentException("Invalid class identifier");
        _entities = _getFullEntitiesList[classIdentifier]();
        _classIdentifier = classIdentifier;
    }

    protected List<IEntity> FilterData()
    {
        if (_conditions == null)
            return _entities;
        var result = new List<IEntity>();
        foreach (var entity in _entities)
            if (_conditions.Check(entity))
                result.Add(entity);
        return result;
    }
    
    public abstract void Execute();

    private static Dictionary<string, Func<List<IEntity>>> _getFullEntitiesList =
        new Dictionary<string, Func<List<IEntity>>>()
        {
            { EntitiesIdentifiers.AirportID, GetAirports },
            { EntitiesIdentifiers.CargoID, GetCargos },
            { EntitiesIdentifiers.CargoPlaneID, GetCargoPlanes },
            { EntitiesIdentifiers.CrewID, GetCrews },
            { EntitiesIdentifiers.FlightID, GetFlights },
            { EntitiesIdentifiers.PassengerID, GetPassengers },
            { EntitiesIdentifiers.PassengerPlaneID, GetPassengerPlanes },
        };
    
    private static List<IEntity> GetAirports()
    {
        return EntityStorage.GetStorage().GetAllAirports().Values.Cast<IEntity>().ToList();
    }
    
    private static List<IEntity> GetCargos()
    {
        return EntityStorage.GetStorage().GetAllCargos().Values.Cast<IEntity>().ToList();
    }
    
    private static List<IEntity> GetCargoPlanes()
    {
        return EntityStorage.GetStorage().GetAllCargoPlanes().Values.Cast<IEntity>().ToList();
    }
    
    private static List<IEntity> GetCrews()
    {
        return EntityStorage.GetStorage().GetAllCrews().Values.Cast<IEntity>().ToList();
    }
    
    private static List<IEntity> GetFlights()
    {
        return EntityStorage.GetStorage().GetAllFlights().Values.Cast<IEntity>().ToList();
    }
    
    private static List<IEntity> GetPassengers()
    {
        return EntityStorage.GetStorage().GetAllPassengers().Values.Cast<IEntity>().ToList();
    }
    
    private static List<IEntity> GetPassengerPlanes()
    {
        return EntityStorage.GetStorage().GetAllPassengerPlanes().Values.Cast<IEntity>().ToList();
    }
}