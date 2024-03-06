using FlightManager.Entity;

namespace FlightManager.DataLoader;
internal interface IDataLoader
{
    public void Load(string dataPath, IList<IEntity> entities, object? entitiesLock = null);
}
