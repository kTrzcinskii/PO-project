using FlightManager.Entity;
using FlightManager.EntityFactory;
using FlightManager.Storage;

namespace FlightManager.Query;

internal class AddQuery : IQuery
{
    private Dictionary<string, string> _fieldValues;
    private string _classIdentifier;
    private Dictionary<string, Factory> _factories;
    private AddToStorageVisitor _visitor;

    public AddQuery(Dictionary<string, string> fieldValues, string classIdentifier)
    {
        _fieldValues = fieldValues;
        _classIdentifier = classIdentifier;
        _factories = Factory.CreateFactoriesContainer();
        _visitor = new AddToStorageVisitor();
    }

    private string[] GetFtrParameters()
    {
        var response = new List<string>();
        var correctOrder = _parametersCorrectOrder[_classIdentifier];

        foreach (var field in correctOrder)
        {
            if (!_fieldValues.ContainsKey(field))
                throw new ArgumentException($"Missing field: {field}");
            response.Add(_fieldValues[field]);
        }

        if (_classIdentifier == EntitiesIdentifiers.FlightID)
        {
            response.Add("[]");
            response.Add("[]");
        }
        
        return response.ToArray();
    }
    
    public void Execute()
    {
        var parameters = GetFtrParameters();
        IEntity newEntity = _factories[_classIdentifier].CreateInstance(parameters);
        newEntity.AcceptVisitor(_visitor);
    }

    private static List<string> _correctFtrOrderAirport = new List<string>()
    {
        Airport.FieldsNames.ID, Airport.FieldsNames.Name, Airport.FieldsNames.Code, Airport.FieldsNames.Longitude,
        Airport.FieldsNames.Latitude, Airport.FieldsNames.AMSL, Airport.FieldsNames.CountryISO
    };

    private static List<string> _correctFtrOrderCargo = new List<string>()
    {
        Cargo.FieldsNames.ID, Cargo.FieldsNames.Weight, Cargo.FieldsNames.Code, Cargo.FieldsNames.Description
    };

    private static List<string> _correctFtrOrderCargoPlane = new List<string>()
    {
        Plane.FieldsNames.ID, Plane.FieldsNames.Serial, Plane.FieldsNames.CountryISO, Plane.FieldsNames.Model,
        CargoPlane.FieldsNames.MaxLoad
    };

    private static List<string> _correctFtrOrderCrew = new List<string>()
    {
        Person.FieldsNames.ID, Person.FieldsNames.Name, Person.FieldsNames.Age, Person.FieldsNames.Phone,
        Person.FieldsNames.Email, Crew.FieldsNames.Practice, Crew.FieldsNames.Role
    };

    private static List<string> _correctFtrOrderFlight = new List<string>()
    {
        Flight.FieldsNames.ID, Flight.FieldsNames.OriginID, Flight.FieldsNames.TargetID, Flight.FieldsNames.TakeOffTime,
        Flight.FieldsNames.LandingTime, Flight.FieldsNames.Longitude, Flight.FieldsNames.Latitude,
        Flight.FieldsNames.AMSL, Flight.FieldsNames.PlaneID
    };

    private static List<string> _correctFtrOrderPassenger = new List<string>()
    {
        Person.FieldsNames.ID, Person.FieldsNames.Name, Person.FieldsNames.Age, Person.FieldsNames.Phone,
        Person.FieldsNames.Email, Passenger.FieldsNames.Class, Passenger.FieldsNames.Miles
    };

    private static List<string> _correctFtrOrderPassengerPlane = new List<string>()
    {
        Plane.FieldsNames.ID, Plane.FieldsNames.Serial, Plane.FieldsNames.CountryISO, Plane.FieldsNames.Model,
        PassengerPlane.FieldsNames.FirstClassSize, PassengerPlane.FieldsNames.BusinessClassSize,
        PassengerPlane.FieldsNames.EconomyClassSize
    };
    
    private static Dictionary<string, List<string>> _parametersCorrectOrder =
        new Dictionary<string, List<string>>()
        {
            { EntitiesIdentifiers.AirportID, _correctFtrOrderAirport },
            { EntitiesIdentifiers.CargoID, _correctFtrOrderCargo },
            { EntitiesIdentifiers.CargoPlaneID, _correctFtrOrderCargoPlane },
            { EntitiesIdentifiers.CrewID, _correctFtrOrderCrew },
            { EntitiesIdentifiers.FlightID, _correctFtrOrderFlight },
            { EntitiesIdentifiers.PassengerID, _correctFtrOrderPassenger },
            { EntitiesIdentifiers.PassengerPlaneID, _correctFtrOrderPassengerPlane }
        };
}