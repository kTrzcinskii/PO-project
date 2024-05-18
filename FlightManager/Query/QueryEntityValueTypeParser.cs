using FlightManager.Entity;
using ArgumentException = System.ArgumentException;

namespace FlightManager.Query;

internal static class QueryEntityValueTypeParser
{
    private static Dictionary<string, Func<string, string, IComparable>> _funcs =
        new Dictionary<string, Func<string, string, IComparable>>()
        {
            { EntitiesIdentifiers.AirportID, ParseAirport },
            { EntitiesIdentifiers.CargoID, ParseCargo },
            { EntitiesIdentifiers.CargoPlaneID, ParseCargoPlane },
            { EntitiesIdentifiers.CrewID, ParseCrew },
            { EntitiesIdentifiers.FlightID, ParseFlight },
            { EntitiesIdentifiers.PassengerID, ParsePassenger },
            { EntitiesIdentifiers.PassengerPlaneID, ParsePassengerPlane }
        };

    private static Dictionary<string, Func<string, IComparable>> _planeFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { Plane.FieldsNames.ID, ToUlong },
            { Plane.FieldsNames.Serial, ToString },
            { Plane.FieldsNames.CountryISO, ToString },
            { Plane.FieldsNames.Model, ToString }
        };

    private static Dictionary<string, Func<string, IComparable>> _personFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { Person.FieldsNames.ID, ToUlong },
            { Person.FieldsNames.Name, ToString },
            { Person.FieldsNames.Age, ToUlong },
            { Person.FieldsNames.Phone, ToString },
            { Person.FieldsNames.Email, ToString }
        };
    
    private static Dictionary<string, Func<string, IComparable>> _airportFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { Airport.FieldsNames.ID, ToUlong },
            { Airport.FieldsNames.Name, ToString },
            { Airport.FieldsNames.Code, ToString },
            { Airport.FieldsNames.Longitude, ToFloat }, 
            { Airport.FieldsNames.Latitude, ToFloat },
            { Airport.FieldsNames.AMSL, ToFloat },
            { Airport.FieldsNames.CountryISO, ToString }
        };

    private static Dictionary<string, Func<string, IComparable>> _cargoFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { Cargo.FieldsNames.ID, ToUlong },
            { Cargo.FieldsNames.Weight, ToFloat },
            { Cargo.FieldsNames.Code, ToString },
            { Cargo.FieldsNames.Description, ToString }
        };

    private static Dictionary<string, Func<string, IComparable>> _cargoPlaneFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { CargoPlane.FieldsNames.MaxLoad, ToFloat }
        };

    private static Dictionary<string, Func<string, IComparable>> _crewFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { Crew.FieldsNames.Practice, ToUshort },
            { Crew.FieldsNames.Role, ToString },
        };
    
    private static Dictionary<string, Func<string, IComparable>> _flightFuncs = new Dictionary<string, Func<string, IComparable>>()
    {
        {Flight.FieldsNames.ID, ToUlong},
        {Flight.FieldsNames.OriginID, ToUlong},
        {Flight.FieldsNames.TargetID, ToUlong},
        {Flight.FieldsNames.TakeOffTime, ToDateTime},
        {Flight.FieldsNames.LandingTime, ToDateTime},
        {Flight.FieldsNames.Longitude, ToFloat},
        {Flight.FieldsNames.Latitude, ToFloat},
        {Flight.FieldsNames.AMSL, ToFloat},
        {Flight.FieldsNames.PlaneID, ToUlong}
    };

    private static Dictionary<string, Func<string, IComparable>> _passengerFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { Passenger.FieldsNames.Class, ToString },
            { Passenger.FieldsNames.Miles, ToUlong },
        };

    private static Dictionary<string, Func<string, IComparable>> _passengerPlaneFuncs =
        new Dictionary<string, Func<string, IComparable>>()
        {
            { PassengerPlane.FieldsNames.BusinessClassSize, ToUshort },
            { PassengerPlane.FieldsNames.FirstClassSize, ToUshort },
            { PassengerPlane.FieldsNames.EconomyClassSize, ToUshort }
        };
    
    public static IComparable Parse(string classIdentifier, string fieldName, string value)
    {
        if (!_funcs.ContainsKey(classIdentifier))
            throw new ArgumentException("Invalid classIdentifier");
        return _funcs[classIdentifier].Invoke(fieldName, value);
    }

    private static IComparable ParseAirport(string fieldName, string value)
    {
        if (!_airportFuncs.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _airportFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ParseCargo(string fieldName, string value)
    {
        if (!_cargoFuncs.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _cargoFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ParseCargoPlane(string fieldName, string value)
    {
        if (!_cargoPlaneFuncs.ContainsKey(fieldName))
            return ParsePlane(fieldName, value);
        return _cargoPlaneFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ParseCrew(string fieldName, string value)
    {
        if (!_crewFuncs.ContainsKey(fieldName))
            return ParsePerson(fieldName, value);
        return _crewFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ParseFlight(string fieldName, string value)
    {
        if (!_flightFuncs.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _flightFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ParsePassenger(string fieldName, string value)
    {
        if (!_passengerFuncs.ContainsKey(fieldName))
            return ParsePerson(fieldName, value);
        return _passengerFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ParsePassengerPlane(string fieldName, string value)
    {
        if (!_passengerPlaneFuncs.ContainsKey(fieldName))
            return ParsePlane(fieldName, value);
        return _passengerPlaneFuncs[fieldName].Invoke(value);
    }

    private static IComparable ParsePlane(string fieldName, string value)
    {
        if (!_planeFuncs.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _planeFuncs[fieldName].Invoke(value);
    }

    private static IComparable ParsePerson(string fieldName, string value)
    {
        if (!_personFuncs.ContainsKey(fieldName))
            throw new ArgumentException("Invalid fieldName");
        return _personFuncs[fieldName].Invoke(value);
    }
    
    private static IComparable ToString(string value)
    {
        return value;
    }

    private static IComparable ToUlong(string value)
    {
        try
        {
            return ulong.Parse(value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Invalid value");
        }
    }
    
    private static IComparable ToFloat(string value)
    {
        try
        {
            return float.Parse(value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Invalid value");
        }
    }
    
    private static IComparable ToDouble(string value)
    {
        try
        {
            return double.Parse(value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Invalid value");
        }
    }
    
    private static IComparable ToUshort(string value)
    {
        try
        {
            return ushort.Parse(value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Invalid value");
        }
    }

    private static IComparable ToDateTime(string value)
    {
        try
        {
            return DateTime.Parse(value);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Invalid value");
        }
    }
}