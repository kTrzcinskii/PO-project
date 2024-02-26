using FlightManager.Entity;
using System.Globalization;

namespace FlightManager.Factory;

internal class CargoPlaneFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.CargoPlaneID;

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        string serial = parameters[1];
        string countryISO = parameters[2];
        string model = parameters[3];
        float maxLoad = Convert.ToSingle(parameters[4], CultureInfo.InvariantCulture);
        return new CargoPlane(ID, serial, countryISO, model, maxLoad);
    }
}

