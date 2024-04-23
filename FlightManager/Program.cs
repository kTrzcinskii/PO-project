using FlightManager.DataLoader;
using FlightManager.DataSerializer;
using FlightManager.DataUpdater.NSSUpdater;
using FlightManager.ProgramArguments;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        var arguments = ArgumentsParser.Parse(args);
        FlightManager flightManager = new FlightManager(new NetworkSourceSimulatorDataLoader(), new JSONDataSerializer(), new NetworkSourceSimulatorDataUpdater(), arguments.updatePath);
        flightManager.StartApp(arguments.inputPath);
    }
}
