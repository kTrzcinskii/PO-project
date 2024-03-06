using FlightManager.DataLoader;
using FlightManager.DataSerializer;

namespace FlightManager;
internal class Program
{
    static void Main(string[] args)
    {
        var ftrDataPath = "example_data.ftr";

        FlightManager flightManager = new FlightManager(new NetworkSourceSimulatorDataLoader(), new JSONDataSerializer());
        flightManager.StartApp(ftrDataPath);
    }
}
