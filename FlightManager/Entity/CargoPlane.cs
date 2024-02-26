namespace FlightManager.Entity;

internal class CargoPlane : Plane
{
    public override ulong ID { get; init; }
    public string Serial { get; init; }
    public string CountryISO { get; init; }
    public string Model { get; init; }
    public float MaxLoad { get; init; }

    public CargoPlane(ulong iD, string serial, string countryISO, string model, float maxLoad)
    {
        ID = iD;
        Serial = serial;
        CountryISO = countryISO;
        Model = model;
        MaxLoad = maxLoad;
    }

}
