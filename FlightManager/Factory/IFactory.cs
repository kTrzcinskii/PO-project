using FlightManager.Entity;

namespace FlightManager.Factory;

internal interface IFactory
{
    public string EntityName { get; }
    public IEntity CreateInstance(string[] parameters);
}
