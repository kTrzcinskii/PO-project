using System.Globalization;

namespace FlightManager.EntityArgumentsParser;
internal class AirportArgumentsParser : IEntityArgumentsParser<(ulong, string, string, float, float, float, string)>
{
    public (ulong, string, string, float, float, float, string) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        string name = data[1];
        string code = data[2];
        float longitude = Convert.ToSingle(data[3], CultureInfo.InvariantCulture);
        float latitude = Convert.ToSingle(data[4], CultureInfo.InvariantCulture);
        float AMSL = Convert.ToSingle(data[5], CultureInfo.InvariantCulture);
        string countryISO = data[6];
        return (ID, name, code, longitude, latitude, AMSL, countryISO);
    }

    public (ulong, string, string, float, float, float, string) ParseArgumets(byte[] data)
    {
        throw new NotImplementedException();
    }
}
