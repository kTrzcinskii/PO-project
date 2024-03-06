using System.Globalization;

namespace FlightManager.EntityArgumentsParser;
internal class FlightArgumentsParser : IEntityArgumentsParser<(ulong, ulong, ulong, string, string, float?, float?, float?, ulong, ulong[], ulong[])>
{
    public (ulong, ulong, ulong, string, string, float?, float?, float?, ulong, ulong[], ulong[]) ParseArgumets(string[] data)
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

    public (ulong, ulong, ulong, string, string, float?, float?, float?, ulong, ulong[], ulong[]) ParseArgumets(byte[] data)
    {
        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream, new System.Text.ASCIIEncoding());

        ulong ID = reader.ReadUInt64();
        ulong originID = reader.ReadUInt64();
        ulong targetID = reader.ReadUInt64();
        string takeOffTime = DateTimeOffset.FromUnixTimeMilliseconds((long)reader.ReadUInt64()).ToString("HH:mm");
        string landingTime = DateTimeOffset.FromUnixTimeMilliseconds((long)reader.ReadUInt64()).ToString("HH:mm");
        ulong planeID = reader.ReadUInt64();
        ushort crewCount = reader.ReadUInt16();
        ulong[] crewIDs = new ulong[crewCount];
        for (int i = 0; i < crewCount; i++)
        {
            crewIDs[i] = reader.ReadUInt64();
        }
        ushort loadCount = reader.ReadUInt16();
        ulong[] loadIDs = new ulong[loadCount];
        for (int i = 0; i < loadCount; i++)
        {
            loadIDs[i] = reader.ReadUInt64();
        }
        return (ID, originID, targetID, takeOffTime, landingTime, null, null, null, planeID, crewIDs, loadIDs);
    }
}
