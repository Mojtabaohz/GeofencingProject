using UnityEngine;

public static class GeofenceUtils
{
    private const double EarthRadius = 6371e3; // Earth's radius in meters

    /// <summary>
    /// Calculates the distance between two GPS points using the Haversine formula.
    /// </summary>
    /// <param name="lat1">Latitude of the first point.</param>
    /// <param name="lon1">Longitude of the first point.</param>
    /// <param name="lat2">Latitude of the second point.</param>
    /// <param name="lon2">Longitude of the second point.</param>
    /// <returns>Distance in meters.</returns>
    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double radLat1 = (float)lat1 * Mathf.Deg2Rad;
        double radLat2 = (float)lat2 * Mathf.Deg2Rad;
        double deltaLat = (float)(lat2 - lat1) * Mathf.Deg2Rad;
        double deltaLon = (float)(lon2 - lon1) * Mathf.Deg2Rad;

        double a = Mathf.Sin((float)(deltaLat / 2)) * Mathf.Sin((float)(deltaLat / 2)) +
                   Mathf.Cos((float)radLat1) * Mathf.Cos((float)radLat2) *
                   Mathf.Sin((float)(deltaLon / 2)) * Mathf.Sin((float)(deltaLon / 2));
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));

        return EarthRadius * c; // Distance in meters
    }
}
