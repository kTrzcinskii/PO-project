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
        query = query.Trim();
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

    private static IQuery CreateDeleteQuery(string query)
    {
        string pattern = @"delete (\w+)(?: where .*)?$";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(query);

        if (!match.Success)
            throw new ArgumentException("invalid query");

        string classID = QueryParser.ClassNameToIdentifier(match.Groups[1].Value);
        ConditionChain? conditionChain = ExtractConditionChainFromQuery(query, classID);
        return new DeleteQuery(conditionChain, classID);
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

        return new DisplayQuery(conditionChain, fields, classID);
    }

    private static IQuery CreateUpdateQuery(string query)
    {
        string pattern = @"update (\w+) set \(.+\)(?: where .*)?$";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(query);

        if (!match.Success)
            throw new ArgumentException("invalid query");

        string classID = QueryParser.ClassNameToIdentifier(match.Groups[1].Value);
        int fieldValuesStart = query.IndexOf("set ") + "set ".Length;
        int fieldValuesLength = query.IndexOf(')') - fieldValuesStart + 1;
        var propertyValueHolders = QueryParser.ParseFieldValues(query.Substring(fieldValuesStart, fieldValuesLength));
        ConditionChain? conditionChain = ExtractConditionChainFromQuery(query, classID);

        return new UpdateQuery(conditionChain, classID, propertyValueHolders);
    }

    private static ConditionChain? ExtractConditionChainFromQuery(string query, string classID)
    {
        ConditionChain? conditionChain = null;
        if (query.Contains("where"))
            conditionChain = QueryParser.ParseConditions(query.Substring(query.IndexOf("where") + "where ".Length), classID);
        return conditionChain;
    }
}