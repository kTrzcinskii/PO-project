using FlightManager.Entity;

namespace FlightManager.Query;

internal class UpdateQuery : FilterableQuery
{
    public UpdateQuery(ConditionChain? conditions, string classIdentifier) : base(conditions, classIdentifier)
    {
    }
    
    public override void Execute()
    {
        throw new NotImplementedException();
    }

}