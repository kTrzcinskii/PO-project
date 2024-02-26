using System.Text.Json.Serialization;

namespace FlightManager.Entity;

[JsonDerivedType(typeof(Airport), typeDiscriminator: nameof(Airport))]
[JsonDerivedType(typeof(Cargo), typeDiscriminator: nameof(Cargo))]
[JsonDerivedType(typeof(CargoPlane), typeDiscriminator: nameof(CargoPlane))]
[JsonDerivedType(typeof(Crew), typeDiscriminator: nameof(Crew))]
[JsonDerivedType(typeof(Flight), typeDiscriminator: nameof(Flight))]
[JsonDerivedType(typeof(Passenger), typeDiscriminator: nameof(Passenger))]
[JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: nameof(PassengerPlane))]
internal interface IEntity
{
    public ulong ID { get; init; }
}
