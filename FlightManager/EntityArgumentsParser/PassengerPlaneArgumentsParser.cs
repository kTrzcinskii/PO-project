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
        const int serialLength = 10;
        const int countryISOLength = 3;

        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream);

        ulong ID = reader.ReadUInt64();
        string serial = BitConverter.ToString(reader.ReadBytes(serialLength));
        string countryISO = BitConverter.ToString(reader.ReadBytes(countryISOLength));
        ushort modelLength = reader.ReadUInt16();
        string model = BitConverter.ToString(reader.ReadBytes(modelLength));
        ushort firstClassSize = reader.ReadUInt16();
        ushort businessClassSize = reader.ReadUInt16();
        ushort economyClassSize = reader.ReadUInt16();
        return (ID, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
    }
}
}
