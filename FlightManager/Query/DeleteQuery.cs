using FlightManager.Entity;

namespace FlightManager.Query;

internal class DeleteQuery : FilterableQuery
{
    private DeletingVisitor visitor;
    
    public DeleteQuery(ConditionChain? conditions, string classIdentifier) : base(conditions, classIdentifier)
    {
        visitor = new DeletingVisitor();
    }

    private void DeleteAll(List<IEntity> entities)
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