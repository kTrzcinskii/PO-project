using FlightManager.Entity;
using Mapsui.Projections;
using System.Numerics;

namespace FlightManager.GUI;
internal class FlightGUIAdapter : FlightGUI
{
    private const int DAY_IN_S = 24 * 60 * 60;

    public FlightGUIAdapter(Flight flight, Airport origin, Airport target)
    {
        if (flight.Longitude == null || flight.Latitude == null)
        {
            Vector2 initalPosition = GetInitialPosition(flight, origin, target);
            flight.Longitude = initalPosition.X;
            flight.Latitude = initalPosition.Y;
        }

        UpdateFlightPosition(flight, origin, target);
        
        WorldPosition = new WorldPosition(flight.Latitude!.Value, flight.Longitude.Value!);
        MapCoordRotation = GetRotation(flight, target);
        ID = flight.ID;
    }

    private Vector2 GetInitialPosition(Flight f, Airport origin, Airport target)
    {
        Vector2 startV = new Vector2(origin.Longitude, origin.Latitude);
        Vector2 endV = new Vector2(target.Longitude, target.Latitude);
        float timePassed = GetInitialTimePassed(f);
        var initialPosition = Vector2.Lerp(startV, endV, timePassed);
        return initialPosition;
    }

    private void UpdateFlightPosition(Flight f, Airport origin, Airport target)
    {
        float timeDiff = GetTimeDiff(f);
        if (timeDiff < 0.0f)
            return;
        var (lonSpeed, latSpeed) = GetVelocity(f, target, timeDiff);
        f.Longitude += lonSpeed * FlightManager.REFRESH_SCREEN_MS / 1000;
        f.Latitude += latSpeed * FlightManager.REFRESH_SCREEN_MS / 1000;
    }
    
    private (float lonSpeed, float latSpeed) GetVelocity(Flight f, Airport target, float timeDiff)
    {
        var diffLon = target.Longitude - f.Longitude!.Value;
        var diffLat = target.Latitude - f.Latitude!.Value;
        
        return (diffLon / timeDiff, diffLat / timeDiff);
    }

    private float GetTimeDiff(Flight f)
    {
        var startTime = f.TakeOffTime.TimeOfDay.TotalSeconds;
        var endTime = f.LandingTime.TimeOfDay.TotalSeconds;
        var currentTime = DateTime.Now.TimeOfDay.TotalSeconds;

        if (startTime < endTime)
        {
            if (currentTime < startTime)
                return -1.0f;
            if (currentTime > endTime)
                return -1.0f;
        }
        else
        {
            if (currentTime > endTime && currentTime < startTime)
                return -1.0f;
            if (currentTime < endTime)
                currentTime += DAY_IN_S;
            endTime += DAY_IN_S;
        }

        return (float)(endTime - currentTime);
    }

    private float GetInitialTimePassed(Flight f)
    {
        var startTime = f.TakeOffTime.TimeOfDay.TotalSeconds;
        var endTime = f.LandingTime.TimeOfDay.TotalSeconds;
        var currentTime = DateTime.Now.TimeOfDay.TotalSeconds;

        if (startTime < endTime)
        {
            if (currentTime < startTime)
                return 0.0f;
            if (currentTime > endTime)
                return 1.0f;
        }
        else
        {
            if (currentTime > endTime && currentTime < startTime)
                return 0.0f;
            if (currentTime < endTime)
                currentTime += DAY_IN_S;
            endTime += DAY_IN_S;
        }

        var duration = endTime - startTime;
        var passed = currentTime - startTime;
        return (float)(passed / duration);
    }
    
    private double GetRotation(Flight flight, Airport target)
    {
        var pointStart = SphericalMercator.FromLonLat(flight.Longitude!.Value, flight.Latitude!.Value);
        var pointEnd = SphericalMercator.FromLonLat(target.Longitude, target.Latitude);
        var rotation = Math.PI / 2 - Math.Atan2(pointEnd.y - pointStart.y, pointEnd.x - pointStart.x);
        return rotation;
    }
}
