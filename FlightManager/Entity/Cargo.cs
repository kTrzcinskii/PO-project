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
}
