using FlightManager.DataParser;
using System.Globalization;

namespace FlightManager.EntityArgumentsParser;
internal class FlightArgumentsParser : IEntityArgumentsParser<(ulong, ulong, ulong, string, string, float, float, float, ulong, ulong[], ulong[])>
{
    public (ulong, ulong, ulong, string, string, float, float, float, ulong, ulong[], ulong[]) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        ulong originID = Convert.ToUInt64(data[1]);
        ulong targetID = Convert.ToUInt64(data[2]);
        string takeOffTime = data[3];
        string landingTime = data[4];
        float longitude = Convert.ToSingle(data[5], CultureInfo.InvariantCulture);
        float latitude = Convert.ToSingle(data[6], CultureInfo.InvariantCulture);
        float AMSL = Convert.ToSingle(data[7], CultureInfo.InvariantCulture);
        ulong planeID = Convert.ToUInt64(data[8]);
        ulong[] crewIDs = Array.ConvertAll(ParametersFormatter.ConvertToArray(data[9]), Convert.ToUInt64);
        ulong[] loadIDs = Array.ConvertAll(ParametersFormatter.ConvertToArray(data[10]), Convert.ToUInt64);
        return (ID, originID, targetID, takeOffTime, landingTime, longitude, latitude, AMSL, planeID, crewIDs, loadIDs);
    }

    public (ulong, ulong, ulong, string, string, float, float, float, ulong, ulong[], ulong[]) ParseArgumets(byte[] data)
    {
        throw new NotImplementedException();
    }
}
