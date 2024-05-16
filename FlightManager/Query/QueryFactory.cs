using System.Text.RegularExpressions;
using FlightManager.Entity;
using FlightManager.Storage;

namespace FlightManager.Query;
internal class QueryFactory
{
    private delegate IQuery CreateQueryDelegate(string query);

    private static readonly Dictionary<string, CreateQueryDelegate> QueryCreateFunctions =
        new Dictionary<string, CreateQueryDelegate>()
        {
            { "display", CreateDisplayQuery },
            { "delete", CreateDeleteQuery },
            { "add", CreateAddQuery },
            { "update", CreateUpdateQuery }
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

        var condtitionChain = QueryParser.ParseConditions(query.Substring(query.IndexOf("where") + "where ".Length), classID);
        
        // TODO: dont harcode type but base it on classID
        return new DisplayQuery<Airport>(condtitionChain, EntityStorage.GetStorage().GetAllAirports().Values.ToList(), fields);
    }

    private static IQuery CreateUpdateQuery(string query)
    {
        throw new NotImplementedException();
    }
}