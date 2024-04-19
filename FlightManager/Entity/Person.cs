namespace FlightManager.Entity;
internal abstract class Person : IEntity
{
    public ulong ID { get; set; }
    public string Name { get; init; }
    public ulong Age { get; init; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public Person(ulong iD, string name, ulong age, string phone, string email)
    {
        ID = iD;
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
    }

    public abstract void AcceptVisitor(IEntityVisitor visitor);
}
