using FlightManager.Entity;

namespace FlightManager.Factory;

internal class PassengerFactory : IFactory
{
    public string EntityName => "P";

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        string name = parameters[1];
        ulong age = Convert.ToUInt64(parameters[2]);
        string phone = parameters[3];
        string email = parameters[4];
        string @class = parameters[5];
        ulong miles = Convert.ToUInt64(parameters[6]);
        return new Passenger(ID, name, age, phone, email, @class, miles);
    }
}
