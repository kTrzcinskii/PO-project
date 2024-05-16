namespace FlightManager.Query;

public struct QueryCondition(string property, IComparable value, QueryConditionType type)
{
    public string Property = property;
    private readonly IComparable _value = value;
    private readonly QueryConditionType _type = type;

    public bool Check(IComparable property)
    {
        switch (_type)
        {
            case QueryConditionType.EQ:
                return property.CompareTo(_value) == 0;
            case QueryConditionType.GT:
                return property.CompareTo(_value) > 0;
            case QueryConditionType.LT:
                return property.CompareTo(_value) < 0;
            case QueryConditionType.GTE:
                return property.CompareTo(_value) >= 0;
            case QueryConditionType.LTE:
                return property.CompareTo(_value) <= 0;
        }
        return false;
    }
}