using System.Text.Json.Serialization;
using FlightManager.Query;

namespace FlightManager.Entity;

// TODO: should probably update setters in every class so that when value changes it also updates in dictionary used for 
// implementing GetFieldValue
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

    public IComparable GetFieldValue(string fieldName);
}
