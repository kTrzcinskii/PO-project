namespace FlightManager.Entity;

internal class CargoPlane : Plane
{
    public float MaxLoad { get; init; }

    public CargoPlane(ulong iD, string serial, string countryISO, string model, float maxLoad) : base(iD, serial, countryISO, model)
    {
        MaxLoad = maxLoad;
    }

}
