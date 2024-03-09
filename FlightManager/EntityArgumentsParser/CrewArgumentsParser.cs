namespace FlightManager.EntityArgumentsParser;
internal class CrewArgumentsParser : IEntityArgumentsParser<(ulong, string, ulong, string, string, ushort, string)>
{
    public (ulong, string, ulong, string, string, ushort, string) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        string name = data[1];
        ulong age = Convert.ToUInt64(data[2]);
        string phone = data[3];
        string email = data[4];
        ushort practice = Convert.ToUInt16(data[5]);
        string role = data[6];
        return (ID, name, age, phone, email, practice, role);
    }

    public (ulong, string, ulong, string, string, ushort, string) ParseArgumets(byte[] data)
    {
        const int phoneLength = 12;
        const int roleLength = 1;

        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream, new System.Text.ASCIIEncoding());

        ulong ID = reader.ReadUInt64();
        ushort nameLenght = reader.ReadUInt16();
        string name = ParametersFormatter.ReadStringFromBytes(reader, nameLenght);
        ushort age = reader.ReadUInt16();
        string phone = ParametersFormatter.ReadStringFromBytes(reader, phoneLength);
        ushort emailLenght = reader.ReadUInt16();
        string email = ParametersFormatter.ReadStringFromBytes(reader, emailLenght);
        ushort practice = reader.ReadUInt16();
        string role = ParametersFormatter.ReadStringFromBytes(reader, roleLength);

        return (ID, name, age, phone, email, practice, role);
    }
}
