using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeofenceManager : MonoBehaviour
{
    public List<GeofenceZone> Geofences = new List<GeofenceZone>();
    public TMP_Text _TMPText;

    void Start()
    {
        // Example geofence data
        Geofences.Add(new GeofenceZone("Test Zone 1", 37.7749, -122.4194, 100)); // San Francisco
        Geofences.Add(new GeofenceZone("Test Zone 2", 34.0522, -118.2437, 200)); // Los Angeles

        foreach (var geofence in Geofences)
        {
            Debug.Log(geofence.ToString());
            _TMPText.text += geofence + "\n";
        }
    }
    
    void Update()
    {
        foreach (var geofence in Geofences)
        {
            double distance = GeofenceUtils.CalculateDistance(
                LocationServiceManager.Instance.Latitude,
                LocationServiceManager.Instance.Longitude,
                geofence.Latitude,
                geofence.Longitude
            );

            if (distance <= geofence.Radius)
            {
                Debug.Log($"Inside geofence: {geofence.Name}");
            }
            else
            {
                Debug.Log($"Outside geofence: {geofence.Name}");
            }
        }
    }
}
