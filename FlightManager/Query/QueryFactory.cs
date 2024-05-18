using System.Text.RegularExpressions;
using FlightManager.Entity;
using FlightManager.Storage;

namespace FlightManager.Query;
internal class QueryFactory
{
    private class CreateHelpers
    {
        public Func<ConditionChain?, List<string>?, IQuery> DisplayCreator;
        public Func<ConditionChain?, IQuery> DeleteCreator;
        
        public CreateHelpers(Func<ConditionChain?, List<string>?, IQuery> displayCreator, Func<ConditionChain?, IQuery> deleteCreator)
        {
            // TODO: there will go creators for other query types
            DisplayCreator = displayCreator;
            DeleteCreator = deleteCreator;
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
            { EntitiesIdentifiers.AirportID, new CreateHelpers(CreateAirportDisplayQuery, CreateAirportDeleteQuery) },
            { EntitiesIdentifiers.CargoID, new CreateHelpers(CreateCargoDisplayQuery, CreateCargoDeleteQuery) },
            { EntitiesIdentifiers.CargoPlaneID, new CreateHelpers(CreateCargoPlaneDisplayQuery, CreateCargoPlaneDeleteQuery) },
            { EntitiesIdentifiers.CrewID, new CreateHelpers(CreateCrewDisplayQuery, CreateCrewDeleteQuery) },
            { EntitiesIdentifiers.FlightID, new CreateHelpers(CreateFlightDisplayQuery, CreateFlightDeleteQuery) },
            { EntitiesIdentifiers.PassengerID, new CreateHelpers(CreatePassengerDisplayQuery, CreatePassengerDeleteQuery) },
            { EntitiesIdentifiers.PassengerPlaneID, new CreateHelpers(CreatePassengerPlaneDisplayQuery, CreatePassengerPlaneDeleteQuery) },
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
        string pattern = @"add (\w+) new \(.+\)$";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(query);

        if (!match.Success)
            throw new ArgumentException("invalid query");

        string classID = QueryParser.ClassNameToIdentifier(match.Groups[1].Value);
        var propertyValueHolders = QueryParser.ParseFieldValues(query.Substring(query.IndexOf("new ") + "new ".Length));

        return new AddQuery(propertyValueHolders, classID);
    }

    private static  IQuery CreateDeleteQuery(string query)
    {
        string pattern = @"delete (\w+)(?: where .*)?$";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(query);

        if (!match.Success)
            throw new ArgumentException("invalid query");

        string classID = QueryParser.ClassNameToIdentifier(match.Groups[1].Value);
        ConditionChain? conditionChain = ExtractConditionChainFromQuery(query, classID);
        return QueryCreateHelpers[classID].DeleteCreator.Invoke(conditionChain);
    }

    private static IQuery CreateAirportDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<Airport>(conditionChain, EntityStorage.GetStorage().GetAllAirports().Values.ToList());
    }

    private static IQuery CreateCargoDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<Cargo>(conditionChain, EntityStorage.GetStorage().GetAllCargos().Values.ToList());
    }
    
    private static IQuery CreateCargoPlaneDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<CargoPlane>(conditionChain, EntityStorage.GetStorage().GetAllCargoPlanes().Values.ToList());
    }
    
    private static IQuery CreateCrewDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<Crew>(conditionChain, EntityStorage.GetStorage().GetAllCrews().Values.ToList());
    }
    
    private static IQuery CreateFlightDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<Flight>(conditionChain, EntityStorage.GetStorage().GetAllFlights().Values.ToList());
    }
    
    private static IQuery CreatePassengerDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<Passenger>(conditionChain, EntityStorage.GetStorage().GetAllPassengers().Values.ToList());
    }
    
    private static IQuery CreatePassengerPlaneDeleteQuery(ConditionChain? conditionChain)
    {
        return new DeleteQuery<PassengerPlane>(conditionChain, EntityStorage.GetStorage().GetAllPassengerPlanes().Values.ToList());
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

        ConditionChain? conditionChain = ExtractConditionChainFromQuery(query, classID);

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

    private static ConditionChain? ExtractConditionChainFromQuery(string query, string classID)
    {
        ConditionChain? conditionChain = null;
        if (query.Contains("where"))
            conditionChain = QueryParser.ParseConditions(query.Substring(query.IndexOf("where") + "where ".Length), classID);
        return conditionChain;
    }
}