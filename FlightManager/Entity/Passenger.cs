using FlightManager.Query;

namespace FlightManager.Entity;

internal class Passenger : Person, ILoad
{
    public string Class { get; init; }
    public ulong Miles { get; init; }

    public Passenger(ulong iD, string name, ulong age, string phone, string email, string @class, ulong miles) : base(iD, name, age, phone, email)
    {
        Class = @class;
        Miles = miles;
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitPassenger(this);
    }

    public override bool MatchCondition(QueryCondition condition)
    {
        switch (condition.Property)
        {
            case "Class":
                return condition.Check(Class);
            case "Miles":
                return condition.Check(Miles);
            default:
                return base.MatchCondition(condition);
        }
    }
}
