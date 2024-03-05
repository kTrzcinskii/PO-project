namespace FlightManager.DataParser;

internal interface IDataParser<Tin, Tout>
{
    // First item of tuple must always be from `EntitiesIdentifiers`
    public (string, Tout) ParseData(Tin data);
}
