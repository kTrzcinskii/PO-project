using FlightManager.Entity;

namespace FlightManager.Query;

internal abstract class FilterableQuery<T> : IQuery where T : IEntity
{
    protected List<QueryCondition>? _conditions;
    protected List<T> _entities;

    protected FilterableQuery(List<QueryCondition>? conditions, List<T> entities)
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
        {
            if (entity.MatchConditions(_conditions))
                result.Add(entity);
        }

        return result;
    }
    
    public abstract void Execute();
}