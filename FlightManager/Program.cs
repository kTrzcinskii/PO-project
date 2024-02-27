using FlightManager.DataParser;
using FlightManager.DataSerializer;
using FlightManager.ProgramArguments;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        var arguments = ArgumentsParser.Parse(args);
        IDataParser parser = new FTRDataParser();
        IDataSerializer dataSerializer = new JSONDataSerializer();

        FlightManager flightManager = new FlightManager(parser, dataSerializer);
        flightManager.LoadEntities(arguments.inputPath);
        flightManager.SerializeEntities(arguments.outputPath);
    }
}
