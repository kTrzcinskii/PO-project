using System.Globalization;

namespace FlightManager.EntityArgumentsParser;
internal class CargoArgumentsParser : IEntityArgumentsParser<(ulong, float, string, string)>
{
    public (ulong, float, string, string) ParseArgumets(string[] data)
    {
        ulong ID = Convert.ToUInt64(data[0]);
        float weight = Convert.ToSingle(data[1], CultureInfo.InvariantCulture);
        string code = data[2];
        string description = data[3];
        return (ID, weight, code, description);
    }

    public (ulong, float, string, string) ParseArgumets(byte[] data)
    {
        const int codeLenght = 6;

        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream);

        ulong ID = reader.ReadUInt64();
        float weight = reader.ReadSingle();
        string code = BitConverter.ToString(reader.ReadBytes(codeLenght));
        ushort descriptionLength = reader.ReadUInt16();
        string description = BitConverter.ToString(reader.ReadBytes(descriptionLength));

        return (ID, weight, code, description);
    }
}
