namespace FlightManager.Query;

// TODO:
// 1) The command which operates on some already existing data (so i think everything except add) should have
// property which will be list of all elements of this class
// 2) When filtring, i want to do somethin like:
// foreach obj in list
//  foreach condition in conditions (here also something smart for managing or/and etc
//      obj.Check(condition)
// condition should be class with fields: (property, value, conditionType)
// conditionType should be enum with possibilites (equal, greater, lower, greater-equal, lower-equal)
public class QueryFactory
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
        return new DeleteQuery();
    }

    private static IQuery CreateDisplayQuery(string query)
    {
        return new DisplayQuery();
    }

    private static IQuery CreateUpdateQuery(string query)
    {
        return new UpdateQuery();
    }
}