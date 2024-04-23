using FlightManager.Entity;
using FlightManager.NewsSource;

namespace FlightManager.Storage;

internal class EntityStorage
{
    private Dictionary<ulong,IEntity> all = new Dictionary<ulong, IEntity>();
    private List<IReportable> reportables = new List<IReportable>();
    private List<INewsSource> newsSources = new List<INewsSource>();
    private Dictionary<ulong, Airport> airports = new Dictionary<ulong, Airport>();
    private Dictionary<ulong, Cargo> cargos = new Dictionary<ulong, Cargo>();
    private Dictionary<ulong, CargoPlane> cargoPlanes = new Dictionary<ulong, CargoPlane>();
    private Dictionary<ulong, Crew> crew = new Dictionary<ulong, Crew>();
    private Dictionary<ulong, Flight> flights = new Dictionary<ulong, Flight>();
    private Dictionary<ulong, Passenger> passengers = new Dictionary<ulong, Passenger>();
    private Dictionary<ulong, PassengerPlane> passengerPlanes = new Dictionary<ulong, PassengerPlane>();
    private readonly object entitiesLock = new object();
    private static EntityStorage? instance = null;

    public static EntityStorage GetStorage()
    {
        if (instance == null)
            instance = new EntityStorage();
        return instance;
    }

    public List<IEntity> GetAll()
    {
        lock (entitiesLock)
        {
            return new List<IEntity>(all.Values);
        }
    }

    public IEntity? GetByID(ulong id)
    {
        if (all.ContainsKey(id))
            return all[id];
        return null;
    }
    
    public List<IReportable> GetReportables()
    {
        lock (entitiesLock)
        {
            return new List<IReportable>(reportables);
        }
    }

    public Airport? GetAirport(ulong id)
    {
        lock (entitiesLock)
        {
            if (!airports.ContainsKey(id))
                return null;
            return airports[id];
        }
    }

    public Dictionary<ulong, Airport> GetAllAirports()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, Airport>(airports);
        }
    }

    public void Add(Airport airport)
    {
        lock (entitiesLock)
        {
            all.Add(airport.ID, airport);
            reportables.Add(airport);
            airports.Add(airport.ID, airport);
        }
    }

    public void RemoveAirport(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = airports.TryGetValue(id, out var airport);
            if (airports.ContainsKey(id))
                airports.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
                reportables.Remove(airport!);
            }
        }
    }

    public Cargo? GetCargo(ulong id)
    {
        lock (entitiesLock)
        {
            if (!cargos.ContainsKey(id))
                return null;
            return cargos[id];
        }
    }

    public Dictionary<ulong, Cargo> GetAllCargos()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, Cargo>(cargos);
        }
    }

    public void Add(Cargo cargo)
    {
        lock (entitiesLock)
        {
            all.Add(cargo.ID, cargo);
            cargos.Add(cargo.ID, cargo);
        }
    }

    public void RemoveCargo(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = cargos.TryGetValue(id, out var cargo);
            if (cargos.ContainsKey(id))
                cargos.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
            }
        }
    }

    public CargoPlane? GetCargoPlane(ulong id)
    {
        lock (entitiesLock)
        {
            if (!cargoPlanes.ContainsKey(id))
                return null;
            return cargoPlanes[id];
        }
    }

    public Dictionary<ulong, CargoPlane> GetAllCargoPlanes()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, CargoPlane>(cargoPlanes);
        }
    }

    public void Add(CargoPlane cargoPlane)
    {
        lock (entitiesLock)
        {
            all.Add(cargoPlane.ID, cargoPlane);
            reportables.Add(cargoPlane);
            cargoPlanes.Add(cargoPlane.ID, cargoPlane);
        }
    }

    public void RemoveCargoPlane(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = cargoPlanes.TryGetValue(id, out var cargoPlane);
            if (cargoPlanes.ContainsKey(id))
                cargoPlanes.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
                reportables.Remove(cargoPlane!);
            }
        }
    }

    public Crew? GetCrew(ulong id)
    {
        lock (entitiesLock)
        {
            if (!crew.ContainsKey(id))
                return null;
            return crew[id];
        }
    }

    public Dictionary<ulong, Crew> GetAllCrews()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, Crew>(crew);
        }
    }

    public void Add(Crew c)
    {
        lock (entitiesLock)
        {
            all.Add(c.ID, c);
            crew.Add(c.ID, c);
        }
    }

    public void RemoveCrew(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = crew.TryGetValue(id, out var c);
            if (crew.ContainsKey(id))
                crew.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
            }
        }
    }

    public Flight? GetFlight(ulong id)
    {
        lock (entitiesLock)
        {
            if (!flights.ContainsKey(id))
                return null;
            return flights[id];
        }
    }

    public Dictionary<ulong, Flight> GetAllFlights()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, Flight>(flights);
        }
    }

    public void Add(Flight flight)
    {
        lock (entitiesLock)
        {
            all.Add(flight.ID, flight);
            flights.Add(flight.ID, flight);
        }
    }

    public void RemoveFlight(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = flights.TryGetValue(id, out var flight);
            if (flights.ContainsKey(id))
                flights.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
            }
        }
    }

    public Passenger? GetPassenger(ulong id)
    {
        lock (entitiesLock)
        {
            if (!passengers.ContainsKey(id))
                return null;
            return passengers[id];
        }
    }

    public Dictionary<ulong, Passenger> GetAllPassengers()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, Passenger>(passengers);
        }
    }
    public void Add(Passenger passenger)
    {
        lock (entitiesLock)
        {
            all.Add(passenger.ID, passenger);
            passengers.Add(passenger.ID, passenger);
        }
    }

    public void RemovePassenger(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = passengers.TryGetValue(id, out var passenger);
            if (passengers.ContainsKey(id))
                passengers.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
            }
        }
    }

    public PassengerPlane? GetPassengerPlane(ulong id)
    {
        lock (entitiesLock)
        {
            if (!passengerPlanes.ContainsKey(id))
                return null;
            return passengerPlanes[id];
        }
    }

    public Dictionary<ulong, PassengerPlane> GetAllPassengerPlanes()
    {
        lock (entitiesLock)
        {
            return new Dictionary<ulong, PassengerPlane>(passengerPlanes);
        }
    }
    public void Add(PassengerPlane passengerPlane)
    {
        lock (entitiesLock)
        {
            all.Add(passengerPlane.ID, passengerPlane);
            reportables.Add(passengerPlane);
            passengerPlanes.Add(passengerPlane.ID, passengerPlane);
        }
    }

    public void RemovePassengerPlane(ulong id)
    {
        lock (entitiesLock)
        {
            bool contains = passengerPlanes.TryGetValue(id, out var passengerPlane);
            if (passengerPlanes.ContainsKey(id))
                passengerPlanes.Remove(id);
            if (contains) 
            {
                all.Remove(id); 
                reportables.Remove(passengerPlane!);
            }
        }
    }

    public List<INewsSource> GetNewsSources()
    {
        lock (entitiesLock)
        {
            return new List<INewsSource>(newsSources);
        }
    }

    public void Add(INewsSource source)
    {
        lock (entitiesLock)
        {
            newsSources.Add(source);
        }
    }
}
