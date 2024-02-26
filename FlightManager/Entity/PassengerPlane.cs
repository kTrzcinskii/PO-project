namespace FlightManager.Entity;

internal class PassengerPlane : Plane
{
    public override ulong ID { get; init; }
    public string Serial { get; init; }
    public string CountryISO { get; init; }
    public string Model { get; init; }
    public ushort FirstClassSize { get; init; }
    public ushort BusinessClassSize { get; init; }
    public ushort EconomyClassSize { get; init; }

    public PassengerPlane(ulong iD, string serial, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize)
    {
        ID = iD;
        Serial = serial;
        CountryISO = countryISO;
        Model = model;
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }

}
