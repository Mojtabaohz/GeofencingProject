using UnityEngine;

[System.Serializable]
public class GeofenceZone
{
    public string Name; // Name of the geofence
    public double Latitude; // Latitude of the center point
    public double Longitude; // Longitude of the center point
    public float Radius; // Radius in meters

    public GeofenceZone(string name, double latitude, double longitude, float radius)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        Radius = radius;
    }

    // Debugging information for the geofence
    public override string ToString()
    {
        return $"{Name}: Lat {Latitude}, Lon {Longitude}, Radius {Radius}m";
    }
}
