using FlightManager.Entity;

namespace FlightManager.Query;

internal class DisplayQuery<T> : FilterableQuery<T> where T : IEntity
{
    private List<string>? _fields;
    
    public DisplayQuery(List<QueryCondition>? conditions, List<T> entities, List<string>? fields) : base(conditions, entities)
    {
        _fields = fields;
    }
    
    private Dictionary<string, (List<object> rows, int requiredColumnWidth)> PrepareColumns(List<T> data)
    {
        if (_fields == null)
        {
            _fields = T.GetAllFieldsNames();
        }

        var dict = new Dictionary<string, (List<object> rows, int requiredColumnWidth)>();
        
        foreach (var field in _fields)
        {
            dict.Add(field, (new List<object>(), 0));
        }

        foreach (var entity in data)
        {
            foreach (var field in _fields)
            {
                object val = entity.GetFieldValue(field);
                dict[field].rows.Add(val);
                int requiredLength = val.ToString()!.Length;
                if (dict[field].requiredColumnWidth < requiredLength)
                {
                    var updated = (dict[field].rows, requiredLength);
                    dict[field] = updated;
                }
            }
        }

        return dict;
    }
    
    private void PrintData(Dictionary<string, (List<object> rows, int requiredColumnWidth)> columns)
    {
        Console.WriteLine("XD");
    }
    
    public override void Execute()
    {
        var data = FilterData();
        var columns = PrepareColumns(data);
        PrintData(columns);
    }

}