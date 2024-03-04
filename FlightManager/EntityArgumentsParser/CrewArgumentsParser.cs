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
        throw new NotImplementedException();
    }
}
