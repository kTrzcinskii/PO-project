namespace FlightManager.Entity;

internal class Crew : Person
{
    public override ulong ID { get; init; }
    public string Name { get; init; }
    public ulong Age { get; init; }
    public string Phone { get; init; }
    public string Email { get; init; }
    public ushort Practice { get; init; }
    public string Role { get; init; }

    public Crew(ulong iD, string name, ulong age, string phone, string email, ushort practice, string role)
    {
        ID = iD;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
        Practice = practice;
        Role = role;
    }

}
