using FlightManager.Query;

namespace FlightManager.Entity;
internal abstract class Plane : IEntity
{
    public ulong ID { get; set; }
    public string Serial { get; init; }
    public string CountryISO { get; init; }
    public string Model { get; init; }

    public Plane(ulong iD, string serial, string countryISO, string model)
    {
        ID = iD;
        Serial = serial;
        CountryISO = countryISO;
        Model = model;
    }

    public abstract void AcceptVisitor(IEntityVisitor visitor);
    public virtual bool MatchCondition(QueryCondition condition)
    {
        switch (condition.Property)
        {
            case "ID":
                return condition.Check(ID);
            case "Serial":
                return condition.Check(Serial);
            case "CountryISO":
                return condition.Check(CountryISO);
            case "Model":
                return condition.Check(Model);
        }

        return false;
    }
}
