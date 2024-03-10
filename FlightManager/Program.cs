using FlightManager.DataLoader;
using FlightManager.DataSerializer;
using FlightManager.ProgramArguments;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        var arguments = ArgumentsParser.Parse(args);
        FlightManager flightManager = new FlightManager(new NetworkSourceSimulatorDataLoader(), new JSONDataSerializer());
        flightManager.StartApp(arguments.inputPath);
    }
}
