using System.Text.RegularExpressions;
using FlightManager.Entity;
using FlightManager.Storage;

namespace FlightManager.Query;
internal class QueryFactory
{
    private class CreateHelpers
    {
        public Func<ConditionChain?, List<string>?, IQuery> DisplayCreator;

        public CreateHelpers(Func<ConditionChain?, List<string>?, IQuery> displayCreator)
        {
            // TODO: there will go creators for other query types
            DisplayCreator = displayCreator;
        }
    }
    
    private delegate IQuery CreateQueryDelegate(string query);

    private static readonly Dictionary<string, CreateQueryDelegate> QueryCreateFunctions =
        new Dictionary<string, CreateQueryDelegate>()
        {
            { "display", CreateDisplayQuery },
            { "delete", CreateDeleteQuery },
            { "add", CreateAddQuery },
            { "update", CreateUpdateQuery }
        };

    private static readonly Dictionary<string, CreateHelpers> QueryCreateHelpers =
        new Dictionary<string, CreateHelpers>()
        {
            { EntitiesIdentifiers.AirportID, new CreateHelpers(CreateAirportDisplayQuery) },
            { EntitiesIdentifiers.CargoID, new CreateHelpers(CreateCargoDisplayQuery) },
            { EntitiesIdentifiers.CargoPlaneID, new CreateHelpers(CreateCargoPlaneDisplayQuery) },
            { EntitiesIdentifiers.CrewID, new CreateHelpers(CreateCrewDisplayQuery) },
            { EntitiesIdentifiers.FlightID, new CreateHelpers(CreateFlightDisplayQuery) },
            { EntitiesIdentifiers.PassengerID, new CreateHelpers(CreatePassengerDisplayQuery) },
            { EntitiesIdentifiers.PassengerPlaneID, new CreateHelpers(CreatePassengerPlaneDisplayQuery) },
        };
    
    public IQuery CreateQuery(string query)
    {
        string queryType = query.Split(" ")[0];
        if (!QueryCreateFunctions.TryGetValue(queryType, out CreateQueryDelegate? factoryMethod))
            throw new ArgumentException("Ivalid query type");
        return factoryMethod.Invoke(query);
    }

    private static IQuery CreateAddQuery(string query)
    {
        return new AddQuery();
    }

    private static  IQuery CreateDeleteQuery(string query)
    {
        throw new NotImplementedException();
    }

    private static IQuery CreateDisplayQuery(string query)
    {
        string pattern = @"display .*? from (\w+)(?: where .*)?$";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(query);

        if (!match.Success)
            throw new ArgumentException("invalid query");

        string classID = QueryParser.ClassNameToIdentifier(match.Groups[1].Value);

        var fields = QueryParser.ParseFields(query.Substring(query.IndexOf(' '), query.IndexOf("from") - query.IndexOf(' ')));

        ConditionChain? conditionChain = null;
        if (query.Contains("where"))
            conditionChain = QueryParser.ParseConditions(query.Substring(query.IndexOf("where") + "where ".Length), classID);

        return QueryCreateHelpers[classID].DisplayCreator.Invoke(conditionChain, fields);
    }

    private static IQuery CreateAirportDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<Airport>(conditionChain, EntityStorage.GetStorage().GetAllAirports().Values.ToList(), fields);
    }

    private static IQuery CreateCargoDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<Cargo>(conditionChain, EntityStorage.GetStorage().GetAllCargos().Values.ToList(), fields);
    }
    
    private static IQuery CreateCargoPlaneDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<CargoPlane>(conditionChain, EntityStorage.GetStorage().GetAllCargoPlanes().Values.ToList(), fields);
    }
    
    private static IQuery CreateCrewDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<Crew>(conditionChain, EntityStorage.GetStorage().GetAllCrews().Values.ToList(), fields);
    }
    
    private static IQuery CreateFlightDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<Flight>(conditionChain, EntityStorage.GetStorage().GetAllFlights().Values.ToList(), fields);
    }
    
    private static IQuery CreatePassengerDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<Passenger>(conditionChain, EntityStorage.GetStorage().GetAllPassengers().Values.ToList(), fields);
    }
    
    private static IQuery CreatePassengerPlaneDisplayQuery(ConditionChain? conditionChain, List<string>? fields)
    {
        return new DisplayQuery<PassengerPlane>(conditionChain, EntityStorage.GetStorage().GetAllPassengerPlanes().Values.ToList(), fields);
    }

    private static IQuery CreateUpdateQuery(string query)
    {
        throw new NotImplementedException();
    }
}