namespace FlightManager.Entity;

internal class Passenger : Person, ILoad
{
    public override ulong ID { get; init; }
    public string Name { get; init; }
    public ulong Age { get; init; }
    public string Phone { get; init; }
    public string Email { get; init; }
    public string Class { get; init; }
    public ulong Miles { get; init; }

    public Passenger(ulong iD, string name, ulong age, string phone, string email, string @class, ulong miles)
    {
        ID = iD;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
        Class = @class;
        Miles = miles;
    }

}
