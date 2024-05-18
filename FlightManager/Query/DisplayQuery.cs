using System.Text;
using FlightManager.Entity;

namespace FlightManager.Query;

internal class DisplayQuery<T> : FilterableQuery<T> where T : IEntity
{
    private List<string>? _fields;
    
    public DisplayQuery(ConditionChain? conditions, List<T> entities, List<string>? fields) : base(conditions, entities)
    {
        _fields = fields;
    }
    
    private Dictionary<string, (List<object> rows, int requiredColumnWidth)> PrepareColumns(List<T> data)
    {
        if (_fields == null)
        {
            _fields = T.GetAllFieldsNames();
            SkipStructInsides();
        }

        var dict = new Dictionary<string, (List<object> rows, int requiredColumnWidth)>();
        
        foreach (var field in _fields)
        {
            dict.Add(field, (new List<object>(), field.Length));
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
    
    private void PrintData(Dictionary<string, (List<object> rows, int requiredColumnWidth)> columns, int rowCount)
    {
        // Display header and splitter
        var header = new StringBuilder();
        var splitter = new StringBuilder();
        foreach (var fieldName in _fields!)
        {
            int width = columns[fieldName].requiredColumnWidth;
            string format = "{0,-" + width + "}";
            header.Append(' ');
            splitter.Append('-');
            header.Append(String.Format(format, fieldName));
            splitter.Append('-', width);
            header.Append(" |");
            splitter.Append("-+");
        }
        header.Remove(header.Length - 1, 1);
        splitter.Remove(splitter.Length - 1, 1);
        Console.WriteLine(header);
        Console.WriteLine(splitter);
        // Display rows
        for (int i = 0; i < rowCount; i++)
        {
            var row = new StringBuilder();
            foreach (var fieldName in _fields!)
            {
                int width = columns[fieldName].requiredColumnWidth;
                var value = columns[fieldName].rows[i];
                string format = "{0," + width + "}";
                row.Append(' ');
                row.Append(String.Format(format, value));
                row.Append(" |");
            }
            row.Remove(row.Length - 1, 1);
            Console.WriteLine(row);
        }
        Console.WriteLine();
    }
    
    public override void Execute()
    {
        var data = FilterData();
        var columns = PrepareColumns(data);
        PrintData(columns, data.Count);
    }

    private void SkipStructInsides()
    {
        _fields?.RemoveAll(f => f.Contains('.'));
    }
}