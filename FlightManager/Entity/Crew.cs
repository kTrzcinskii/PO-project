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

}
