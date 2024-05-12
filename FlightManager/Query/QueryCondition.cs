namespace FlightManager.Query;

public struct QueryCondition(string property, object value, QueryConditionType type)
{
    public string Property = property;
    private readonly object _value = value;
    private readonly QueryConditionType _type = type;

    public bool Check<T>(IComparable<T> property)
    {
        T compValue = (T)_value;
        switch (_type)
        {
            case QueryConditionType.EQ:
                return property.CompareTo(compValue) == 0;
            case QueryConditionType.GT:
                return property.CompareTo(compValue) > 0;
            case QueryConditionType.LT:
                return property.CompareTo(compValue) < 0;
            case QueryConditionType.GTE:
                return property.CompareTo(compValue) >= 0;
            case QueryConditionType.LTE:
                return property.CompareTo(compValue) <= 0;
        }
        return false;
    }
}