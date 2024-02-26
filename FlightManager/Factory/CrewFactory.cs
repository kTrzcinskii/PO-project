using FlightManager.Entity;

namespace FlightManager.Factory;

internal class CrewFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.CrewID;

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        string name = parameters[1];
        ulong age = Convert.ToUInt64(parameters[2]);
        string phone = parameters[3];
        string email = parameters[4];
        ushort practice = Convert.ToUInt16(parameters[5]);
        string role = parameters[6];
        return new Crew(ID, name, age, phone, email, practice, role);
    }
}
