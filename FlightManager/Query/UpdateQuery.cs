using FlightManager.Entity;

namespace FlightManager.Query;

internal class UpdateQuery<T> : FilterableQuery<T> where T : IEntity
{
    public UpdateQuery(List<QueryCondition>? andConditions, List<QueryCondition>? orConditions, List<T> entities) : base(andConditions, orConditions, entities)
    {
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }

}