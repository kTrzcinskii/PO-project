namespace FlightManager.Entity;
internal static class EntitiesIdentifiers
{
    public const string AirportID = "AI";
    public const string CargoID = "CA";
    public const string CargoPlaneID = "CP";
    public const string CrewID = "C";
    public const string FlightID = "FL";
    public const string PassengerID = "P";
    public const string PassengerPlaneID = "PP";

    public const string NewAirportID = "NAI";
    public const string NewCargoID = "NCA";
    public const string NewCargoPlaneID = "NCP";
    public const string NewCrewID = "NCR";
    public const string NewFlightID = "NFL";
    public const string NewPassengerID = "NPA";
    public const string NewPassengerPlaneID = "NPP";

    public static readonly Dictionary<string, string> NewEntityIdentifier = new Dictionary<string, string>() { { NewAirportID, AirportID }, { NewCargoID, CargoID }, { NewCargoPlaneID, CargoPlaneID }, { NewCrewID, CrewID }, { NewFlightID, FlightID }, { NewPassengerID, PassengerID }, { NewPassengerPlaneID, PassengerPlaneID } };

    public static readonly Dictionary<string, string> FullNameToIdentifier = new Dictionary<string, string>()
    {
        { "Airport", AirportID },
        { "Cargo", CargoID },
        { "CargoPlane", CargoPlaneID },
        { "Crew", CrewID },
        { "Flight", FlightID },
        { "Passenger", PassengerID },
        { "PassengerPlane", PassengerPlaneID }
    };
}
