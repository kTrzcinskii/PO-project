using FlightManager.Entity;

namespace FlightManager.Storage;

internal class EntityStorage
{
    private List<IEntity> all = new List<IEntity>();
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
            return new List<IEntity>(all);
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
            all.Add(airport);
            airports.Add(airport.ID, airport);
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
            all.Add(cargo);
            cargos.Add(cargo.ID, cargo);
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
            all.Add(cargoPlane);
            cargoPlanes.Add(cargoPlane.ID, cargoPlane);
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
            all.Add(c);
            crew.Add(c.ID, c);
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
            all.Add(flight);
            flights.Add(flight.ID, flight);
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
            all.Add(passenger);
            passengers.Add(passenger.ID, passenger);
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
            all.Add(passengerPlane);
            passengerPlanes.Add(passengerPlane.ID, passengerPlane);
        }
    }

}
