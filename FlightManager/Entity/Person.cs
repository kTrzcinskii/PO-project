namespace FlightManager.Entity;
internal abstract class Person : IEntity
{
    public ulong ID { get; init; }
    public string Name { get; init; }
    public ulong Age { get; init; }
    public string Phone { get; init; }
    public string Email { get; init; }

    public Person(ulong iD, string name, ulong age, string phone, string email)
    {
        ID = iD;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
    }
}
