using System.Text.RegularExpressions;
using FlightManager.Entity;

namespace FlightManager.Query;

internal static class QueryParser
{
    private const string fieldSeparator = ", ";
    private const string conditionInnerSeparator = " ";
    private const string allFieldsQuery = "*";
    
    public static List<string>? ParseFields(string fieldsQueryPart)
    {
        fieldsQueryPart = fieldsQueryPart.Trim();
        if (fieldsQueryPart.Length == 0 || fieldsQueryPart == allFieldsQuery)
            return null;
        return fieldsQueryPart.Split(fieldSeparator).ToList();
    }

    public static ConditionChain? ParseConditions(string conditionsQueryPart, string classIdentifier)
    {
        conditionsQueryPart = conditionsQueryPart.Trim();
        if (conditionsQueryPart.Length == 0)
            return null;
        
        string pattern = @"\s*(and|or)\s*";
        var conditionsAndSeparators = Regex.Split(conditionsQueryPart, pattern).ToList();

        ConditionChain? chain = null;
        ConditionChain? current = null;
        // true if previous was separator 
        bool lastSeparator = false;
        ConditionChainSeparator separator = ConditionChainSeparator.AND;
        foreach (var p in conditionsAndSeparators)
        {
            if (chain == null)
            {
                chain = new ConditionChain(ParseSingleCondition(p, classIdentifier));
                current = chain;
                continue;
            }

            if (!lastSeparator)
            {
                separator = CreateConditionChainSeparator(p);
                lastSeparator = true;
                continue;
            }

            ConditionChain newChain = new ConditionChain(ParseSingleCondition(p, classIdentifier));
            current!.SetNext(newChain, separator);
            current = newChain;
            lastSeparator = false;
        }

        return chain;
    }

    private static QueryCondition ParseSingleCondition(string condition, string classIdentifier)
    {
        var parts = condition.Split(conditionInnerSeparator);
        if (parts.Length != 3)
            throw new ArgumentException("Invalid condition");
        string fieldName = parts[0];
        QueryConditionType type = CreateQueryConditionType(parts[1]);
        IComparable conditionValue = QueryEntityValueTypeParser.Parse(classIdentifier, fieldName, parts[2]);
        return new QueryCondition(fieldName, conditionValue, type);
    }

    private static QueryConditionType CreateQueryConditionType(string type)
    {
        if (type == "=")
            return QueryConditionType.EQ;
        if (type == "<")
            return QueryConditionType.LT;
        if (type == ">")
            return QueryConditionType.GT;
        if (type == "<=")
            return QueryConditionType.LTE;
        if (type == ">=")
            return QueryConditionType.GTE;
        throw new ArgumentException("Invalid QueryConditionType value");
    }

    private static ConditionChainSeparator CreateConditionChainSeparator(string separator)
    {
        if (separator == "and")
            return ConditionChainSeparator.AND;
        if (separator == "or")
            return ConditionChainSeparator.OR;
        throw new ArgumentException("Invalid ConditionChainSeparator value");
    }

    public static string ClassNameToIdentifier(string className)
    {
        if (!EntitiesIdentifiers.FullNameToIdentifier.ContainsKey(className))
            throw new ArgumentException("Invalid className");
        return EntitiesIdentifiers.FullNameToIdentifier[className];
    }
}