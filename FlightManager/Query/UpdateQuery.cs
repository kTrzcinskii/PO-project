using FlightManager.Entity;

namespace FlightManager.Query;

internal class UpdateQuery<T> : FilterableQuery<T> where T : IEntity
{
    public UpdateQuery(ConditionChain? conditions, List<T> entities) : base(conditions, entities)
    {
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }

}