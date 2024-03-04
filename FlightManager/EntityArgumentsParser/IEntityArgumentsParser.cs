namespace FlightManager.EntityArgumentsParser;
internal interface IEntityArgumentsParser<TArguments>
{
    public TArguments ParseArgumets(string[] data);
    public TArguments ParseArgumets(byte[] data);
}
