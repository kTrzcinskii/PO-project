namespace FlightManager.DataParser;

internal interface IDataParser
{
    // For every entry (line in data) - class instance to be created return 
    // tuple (entityName, parameters). If parameter is of array type it should
    // be in form of: "[value1;value2;value3;...]". Parameters should be in
    // exactly same order as they appear in IEntity constructors.
    public IList<(string, string[])> ParseData(string[] data);
}
