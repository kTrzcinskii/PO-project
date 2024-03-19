using FlightManager.Entity;

namespace FlightManager.EntityFactory;

internal abstract class Factory
{
    public abstract IEntity CreateInstance(string[] parameters);
    public abstract IEntity CreateInstance(byte[] parameters);

}
