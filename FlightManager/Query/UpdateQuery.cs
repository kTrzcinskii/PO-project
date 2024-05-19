using FlightManager.Entity;
using FlightManager.Storage;

namespace FlightManager.Query;

internal class UpdateQuery : FilterableQuery
{
    private Dictionary<string, string> _fieldValues;
    public UpdateQuery(ConditionChain? conditions, string classIdentifier, Dictionary<string, string> fieldValues) : base(conditions, classIdentifier)
    {
        _fieldValues = fieldValues;
    }

    private void Update(List<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            foreach ((string field, string value) in _fieldValues)
            {
                if (field == "ID")
                {
                    if (EntityStorage.GetStorage()
                            .GetByID((ulong)QueryEntityValueTypeParser.Parse(_classIdentifier, field, value)) != null)
                        throw new ArgumentException($"{value} already used in db (ID must be unique)");
                }
                entity.UpdateFieldValue(field, QueryEntityValueTypeParser.Parse(_classIdentifier, field, value));
            }
        }
    }
    
    public override void Execute()
    {
        var data = FilterData();
        Update(data);
    }

}