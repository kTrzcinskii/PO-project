using FlightManager.Entity;

namespace FlightManager.Query;

internal class DeleteQuery<T> : FilterableQuery<T> where T : IEntity
{
    private DeletingVisitor visitor;
    
    public DeleteQuery(List<QueryCondition>? andConditions, List<QueryCondition>? orConditions, List<T> entities) : base(andConditions, orConditions, entities)
    {
        visitor = new DeletingVisitor();
    }

    private void DeleteAll(List<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.AcceptVisitor(visitor);
        }
    }
    
    public override void Execute()
    {
        var data = FilterData();
        DeleteAll(data);
    }

}