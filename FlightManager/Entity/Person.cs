namespace FlightManager.Entity;
internal abstract class Person : IEntity
{
    public abstract ulong ID { get; init; }
}
