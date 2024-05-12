using FlightManager.Query;

namespace FlightManager.Entity;

internal class Cargo : IEntity, ILoad
{
    public ulong ID { get; set; }
    public float Weight { get; init; }
    public string Code { get; init; }
    public string Description { get; init; }

    public Cargo(ulong iD, float weight, string code, string description)
    {
        ID = iD;
        Weight = weight;
        Code = code;
        Description = description;
    }

    public void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitCargo(this);
    }

    public bool MatchCondition(QueryCondition condition)
    {
        switch (condition.Property)
        {
            case "ID":
                return condition.Check(ID);
            case "Weight":
                return condition.Check(Weight);
            case "Code":
                return condition.Check(Code);
            case "Description":
                return condition.Check(Description);
        }

        return false;
    }
}
