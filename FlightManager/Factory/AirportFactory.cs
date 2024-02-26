using FlightManager.Entity;
using System.Globalization;

namespace FlightManager.Factory;

internal class AirportFactory : IFactory
{
    public string EntityName => "AI";

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        string name = parameters[1];
        string code = parameters[2];
        float longitude = Convert.ToSingle(parameters[3], CultureInfo.InvariantCulture);
        float latitude = Convert.ToSingle(parameters[4], CultureInfo.InvariantCulture);
        float AMSL = Convert.ToSingle(parameters[5], CultureInfo.InvariantCulture);
        string countryISO = parameters[6];
        return new Airport(ID, name, code, longitude, latitude, AMSL, countryISO);
    }
}

