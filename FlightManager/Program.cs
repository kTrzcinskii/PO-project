using FlightManager.DataParser;
using FlightManager.DataSerializer;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        var arguments = new ArgumentsParser(args);
        IDataParser parser = new FTRDataParser();
        IDataSerializer dataSerializer = new JSONDataSerializer();

        FlightManager flightManager = new FlightManager(parser, dataSerializer);
        flightManager.LoadEntities(arguments.InputPath);
        flightManager.SerializeEntities(arguments.OutputPath);
    }
}
