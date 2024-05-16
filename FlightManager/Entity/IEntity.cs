using System.Text.Json.Serialization;
using FlightManager.Query;

namespace FlightManager.Entity;

// TODO: when we do update we must update field value in dictionary of values in object
[JsonDerivedType(typeof(Airport), typeDiscriminator: EntitiesIdentifiers.AirportID)]
[JsonDerivedType(typeof(Cargo), typeDiscriminator: EntitiesIdentifiers.CargoID)]
[JsonDerivedType(typeof(CargoPlane), typeDiscriminator: EntitiesIdentifiers.CargoPlaneID)]
[JsonDerivedType(typeof(Crew), typeDiscriminator: EntitiesIdentifiers.CrewID)]
[JsonDerivedType(typeof(Flight), typeDiscriminator: EntitiesIdentifiers.FlightID)]
[JsonDerivedType(typeof(Passenger), typeDiscriminator: EntitiesIdentifiers.PassengerID)]
[JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: EntitiesIdentifiers.PassengerPlaneID)]
internal interface IEntity
{
    public ulong ID { get; set; }
    public void AcceptVisitor(IEntityVisitor visitor);

    public bool MatchCondition(QueryCondition condition);

    public static virtual List<string> GetAllFieldsNames()
    {
        // TODO: please make it bettter
        return new List<string>() { "ID" };
    }

    public IComparable GetFieldValue(string fieldName);
}
