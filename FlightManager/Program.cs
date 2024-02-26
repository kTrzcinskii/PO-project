using FlightManager.DataParser;
using FlightManager.DataSerializer;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        string outputPath = "test.json";
        string inputPath = "example_data.ftr";
        IDataParser parser = new FTRDataParser();
        IDataSerializer dataSerializer = new JSONDataSerializer();

        FlightManager flightManager = new FlightManager(parser, dataSerializer);
        flightManager.LoadEntities(inputPath);
        flightManager.SerializeEntities(outputPath);
    }
}
