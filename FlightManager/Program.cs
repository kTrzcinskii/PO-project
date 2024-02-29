using FlightManager.DataParser;
using FlightManager.DataSerializer;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        var input = "example_data.ftr";
        var output = "output.json";
        IDataParser parser = new FTRDataParser();
        IDataSerializer dataSerializer = new JSONDataSerializer();

        FlightManager flightManager = new FlightManager(parser, dataSerializer);
        flightManager.LoadEntitiesFromFile(input);
        flightManager.SaveEntitiesToFile(output);
    }
}
