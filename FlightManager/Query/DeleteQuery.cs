using FlightManager.Entity;

namespace FlightManager.Query;

internal class DeleteQuery<T> : FilterableQuery<T> where T : IEntity
{
    public DeleteQuery(List<QueryCondition>? conditions, List<T> entities) : base(conditions, entities)
    {
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }

}