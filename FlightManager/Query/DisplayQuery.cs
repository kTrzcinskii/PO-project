using FlightManager.Entity;

namespace FlightManager.Query;

internal class DisplayQuery<T> : FilterableQuery<T> where T : IEntity
{
    public DisplayQuery(List<QueryCondition>? conditions, List<T> entities) : base(conditions, entities)
    {
    }

    private void PrintData(List<T> data)
    {
        Console.WriteLine("XD");
    }
    
    public override void Execute()
    {
        var data = FilterData();
        PrintData(data);
    }

}