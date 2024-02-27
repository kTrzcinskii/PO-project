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

}
