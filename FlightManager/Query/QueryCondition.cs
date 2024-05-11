namespace FlightManager.Query;

public struct QueryCondition(string property, object value, QueryConditionType type)
{
    public string Property = property;
    public object Value = value;
    public QueryConditionType Type = type;
}