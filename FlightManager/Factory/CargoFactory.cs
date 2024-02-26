using FlightManager.Entity;
using System.Globalization;

namespace FlightManager.Factory;
internal class CargoFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.CargoID;

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        float weight = Convert.ToSingle(parameters[1], CultureInfo.InvariantCulture);
        string code = parameters[2];
        string description = parameters[3];
        return new Cargo(ID, weight, code, description);
    }
}
