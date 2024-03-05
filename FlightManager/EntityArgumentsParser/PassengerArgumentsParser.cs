namespace FlightManager.EntityArgumentsParser;
internal class PassengerArgumentsParser : IEntityArgumentsParser<(ulong, string, ulong, string, string, string, ulong)>
{
    public (ulong, string, ulong, string, string, string, ulong) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        string name = data[1];
        ulong age = Convert.ToUInt64(data[2]);
        string phone = data[3];
        string email = data[4];
        string @class = data[5];
        ulong miles = Convert.ToUInt64(data[6]);
        return (ID, name, age, phone, email, @class, miles);
    }

    public (ulong, string, ulong, string, string, string, ulong) ParseArgumets(byte[] data)
    {
        const int phoneLength = 12;
        const int classLength = 1;

        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream);

        ulong ID = reader.ReadUInt64();
        ushort nameLength = reader.ReadUInt16();
        string name = BitConverter.ToString(reader.ReadBytes(nameLength));
        ushort age = reader.ReadUInt16();
        string phone = BitConverter.ToString(reader.ReadBytes(phoneLength));
        ushort emailLength = reader.ReadUInt16();
        string email = BitConverter.ToString(reader.ReadBytes(emailLength));
        string @class = BitConverter.ToString(reader.ReadBytes(classLength));
        ulong miles = reader.ReadUInt64();
        return (ID, name, age, phone, email, @class, miles);
    }
}
