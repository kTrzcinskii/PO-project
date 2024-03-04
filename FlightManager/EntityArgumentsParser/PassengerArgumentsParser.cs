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
        throw new NotImplementedException();
    }
}
