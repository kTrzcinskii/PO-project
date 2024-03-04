namespace FlightManager.EntityArgumentsParser;
internal class PassengerPlaneArgumentsParser : IEntityArgumentsParser<(ulong, string, string, string, ushort, ushort, ushort)>
{
    public (ulong, string, string, string, ushort, ushort, ushort) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        string serial = data[1];
        string countryISO = data[2];
        string model = data[3];
        ushort firstClassSize = Convert.ToUInt16(data[4]);
        ushort businessClassSize = Convert.ToUInt16(data[5]);
        ushort economyClassSize = Convert.ToUInt16(data[6]);
        return (ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
    }

    public (ulong, string, string, string, ushort, ushort, ushort) ParseArgumets(byte[] data)
    {
        throw new NotImplementedException();
    }
}
