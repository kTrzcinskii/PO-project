using FlightManager.Entity;

namespace FlightManager.Query;

internal class ConditionChain
{
    private ConditionChain? _next = null;
    private QueryCondition _condition;
    private ConditionChainSeparator? _separator = null;
    
    public ConditionChain(QueryCondition condition)
    {
        _condition = condition;
    }
    
    public void SetNext(ConditionChain next, ConditionChainSeparator separator)
    {
        _next = next;
        _separator = separator;
    }

    public bool Check(IEntity entity)
    {
        bool match = entity.MatchCondition(_condition);
        switch (_separator)
        {
            case ConditionChainSeparator.AND:
                if (_next == null || !match)
                    return match;
                return _next.Check(entity);
            case ConditionChainSeparator.OR:
                if (_next == null || match)
                    return match;
                return _next.Check(entity);
            case null:
                return match;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}