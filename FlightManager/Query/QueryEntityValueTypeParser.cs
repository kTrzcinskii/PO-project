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
        throw new NotImplementedException();
    }
    
    private static IComparable ParseCargoPlane(string fieldName, string value)
    {
        throw new NotImplementedException();
    }
    
    private static IComparable ParseCrew(string fieldName, string value)
    {
        throw new NotImplementedException();
    }
    
    private static IComparable ParseFlight(string fieldName, string value)
    {
        throw new NotImplementedException();
    }
    
    private static IComparable ParsePassenger(string fieldName, string value)
    {
        throw new NotImplementedException();
    }
    
    private static IComparable ParsePassengerPlane(string fieldName, string value)
    {
        throw new NotImplementedException();
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
}