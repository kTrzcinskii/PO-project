﻿using FlightManager.Entity;
using Mapsui.Projections;
using System.Numerics;

namespace FlightManager.Adapter;
internal class FlightGUIAdapter : FlightGUI
{
    private const int DAY_IN_S = 24 * 60 * 60;

    public FlightGUIAdapter(Flight flight, Airport origin, Airport target)
    {
        var currentPosition = GetCurrentPosition(flight, origin, target);
        ID = flight.ID;
        WorldPosition = new WorldPosition(currentPosition.Y, currentPosition.X);
        MapCoordRotation = GetRotation(origin, target);
    }

    private Vector2 GetCurrentPosition(Flight f, Airport origin, Airport target)
    {
        Vector2 startV = new Vector2(origin.Longitude, origin.Latitude);
        Vector2 endV = new Vector2(target.Longitude, target.Latitude);
        float timePassed = GetFlightTimePassed(f);
        var currentPosition = Vector2.Lerp(startV, endV, timePassed);
        return currentPosition;
    }

    private float GetFlightTimePassed(Flight f)
    {
        var startTime = TimeSpan.Parse(f.TakeOffTime).TotalSeconds;
        var endTime = TimeSpan.Parse(f.LandingTime).TotalSeconds;
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

    private double GetRotation(Airport origin, Airport target)
    {
        var pointStart = SphericalMercator.FromLonLat(origin.Longitude, origin.Latitude);
        var pointEnd = SphericalMercator.FromLonLat(target.Longitude, target.Latitude);
        var rotation = Math.PI / 2 - Math.Atan2(pointEnd.y - pointStart.y, pointEnd.x - pointStart.x);
        return rotation;
    }
}
