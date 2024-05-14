using FlightManager.Entity;

namespace FlightManager.Query;

internal abstract class FilterableQuery<T> : IQuery where T : IEntity
{
    protected List<QueryCondition>? _andConditions;
    protected List<QueryCondition>? _orConditions;
    protected List<T> _entities;

    protected FilterableQuery(List<QueryCondition>? andConditions, List<QueryCondition>? orConditions, List<T> entities)
    {
        _andConditions = andConditions;
        _orConditions = orConditions;
        _entities = entities;
    }

    protected List<T> FilterData()
    {
        if (_andConditions == null && _orConditions == null)
            return _entities;
        var result = new List<T>();
        if (_andConditions != null)
        {
            foreach (var entity in _entities)
            {
                if (entity.MatchAllConditions(_andConditions))
                    result.Add(entity);
            }
        }

        if (_orConditions != null)
        {
            foreach (var entity in _entities)
            {
                if (entity.MatchAnyCondition(_orConditions))
                    result.Add(entity);
            }
        }
        return result;
    }
    
    public abstract void Execute();
}