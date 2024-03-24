﻿namespace FlightManager.Entity;

internal class PassengerPlane : Plane
{
    public ushort FirstClassSize { get; init; }
    public ushort BusinessClassSize { get; init; }
    public ushort EconomyClassSize { get; init; }

    public PassengerPlane(ulong iD, string serial, string countryISO, string model, ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) : base(iD, serial, countryISO, model)
    {
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }

    public override void AcceptVisitor(IEntityVisitor visitor)
    {
        visitor.VisitPassengerPlane(this);
    }
}
