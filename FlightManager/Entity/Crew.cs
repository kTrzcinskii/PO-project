using FlightManager.Query;

namespace FlightManager.Entity;

internal class Crew : Person
{
    public ushort Practice { get; init; }
    public string Role { get; init; }

    public Crew(ulong iD, string name, ulong age, string phone, string email, ushort practice, string role) : base(iD, name, age, phone, email)
    {
        Practice = practice;
        Role = role;
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitCrew(this);
    }

    public override bool MatchCondition(QueryCondition condition)
    {
        switch (condition.Property)
        {
            case "Practice":
                return condition.Check(Practice);
            case "Role":
                return condition.Check(Role);
            default:
                return base.MatchCondition(condition);
        }
    }
}
