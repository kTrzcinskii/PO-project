using System.Globalization;

namespace FlightManager.EntityArgumentsParser;
internal class CargoPlaneArgumentsParser : IEntityArgumentsParser<(ulong, string, string, string, float)>
{
    public (ulong, string, string, string, float) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        string serial = data[1];
        string countryISO = data[2];
        string model = data[3];
        float maxLoad = Convert.ToSingle(data[4], CultureInfo.InvariantCulture);
        return (ID, serial, countryISO, model, maxLoad);
    }

    public (ulong, string, string, string, float) ParseArgumets(byte[] data)
    {
        throw new NotImplementedException();
    }
}
