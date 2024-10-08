﻿namespace FlightManager.Entity;

public struct WorldPosition : IComparable
{
    public WorldPosition(float? l, float? lat)
    {
        Long = l;
        Lat = lat;
    }

    public float? Long { get; set; }
    public float? Lat { get; set; }
    public override string ToString()
    {
        return $"{{{Long}; {Lat}}}";
    }

    public int CompareTo(object? obj)
    {
        if (Long == null || Lat == null)
            return 1;
        if (obj is WorldPosition)
        {
            WorldPosition wp = (WorldPosition)obj;
            if (Long.Value.CompareTo(wp.Long) != 0)
                return Long.Value.CompareTo(wp.Long);
            return Lat.Value.CompareTo(wp.Lat);
        }
        return 1;
    }
}