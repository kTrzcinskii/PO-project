using FlightManager.Entity;

namespace FlightManager.Query;

internal abstract class FilterableQuery<T> : IQuery where T : IEntity
{
    protected ConditionChain? _conditions;
    protected List<T> _entities;

    protected FilterableQuery(ConditionChain? conditions, List<T> entities)
    {
        _conditions = conditions;
        _entities = entities;
    }

    protected List<T> FilterData()
    {
        if (_conditions == null)
            return _entities;
        var result = new List<T>();
        foreach (var entity in _entities)
            if (_conditions.Check(entity))
                result.Add(entity);
        return result;
    }
    
    public abstract void Execute();
}