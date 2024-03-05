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
        const int serialLength = 10;
        const int countryISOLength = 3;

        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream, new System.Text.ASCIIEncoding());

        ulong ID = reader.ReadUInt64();
        string serial = new string(reader.ReadChars(serialLength));
        string countryISO = new string(reader.ReadChars(countryISOLength));
        ushort modelLength = reader.ReadUInt16();
        string model = new string(reader.ReadChars(modelLength));
        float maxLoad = reader.ReadSingle();

        return (ID, serial, countryISO, model, maxLoad);
    }
}
