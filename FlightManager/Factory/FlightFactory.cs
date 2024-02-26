using FlightManager.DataParser;
using FlightManager.Entity;
using System.Globalization;

namespace FlightManager.Factory;

internal class FlightFactory : IFactory
{
    public string EntityName => EntitiesIdentifiers.FlightID;

    public IEntity CreateInstance(string[] parameters)
    {
        ulong ID = Convert.ToUInt64(parameters[0]);
        ulong originID = Convert.ToUInt64(parameters[1]);
        ulong targetID = Convert.ToUInt64(parameters[2]);
        string takeOffTime = parameters[3];
        string landingTime = parameters[4];
        float longitude = Convert.ToSingle(parameters[5], CultureInfo.InvariantCulture);
        float latitude = Convert.ToSingle(parameters[6], CultureInfo.InvariantCulture);
        float AMSL = Convert.ToSingle(parameters[7], CultureInfo.InvariantCulture);
        ulong planeID = Convert.ToUInt64(parameters[8]);
        ulong[] crewIDs = Array.ConvertAll(ParametersFormatter.ConvertToArray(parameters[9]), Convert.ToUInt64);
        ulong[] loadIDs = Array.ConvertAll(ParametersFormatter.ConvertToArray(parameters[10]), Convert.ToUInt64);
        return new Flight(ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs);
    }
}
