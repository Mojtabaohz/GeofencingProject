using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GeofenceManager : MonoBehaviour
{
    [System.Serializable]
    public class GeofenceEvent : UnityEvent<GeofenceZone>
    {
    }

    public GeofenceEvent OnEnter;
    public GeofenceEvent OnExit;
    public List<GeofenceZone> Geofences = new List<GeofenceZone>();
    [SerializeField] private TMP_Text _tmpText;

    private Dictionary<string, bool> _geofenceStates = new Dictionary<string, bool>(); // Track entry state

    void Start()
    {
        // Example geofence data
        Geofences.Add(new GeofenceZone("Test Zone 1", 37.7749, -122.4194, 100)); // San Francisco
        Geofences.Add(new GeofenceZone("Test Zone 2", 34.0522, -118.2437, 200)); // Los Angeles

        foreach (var geofence in Geofences)
        {
            Debug.Log(geofence.ToString());
            _tmpText.text += geofence + "\n";
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

            bool isInside = distance <= geofence.Radius;

            // Check for entry/exit
            if (isInside && !_geofenceStates[geofence.Name])
            {
                _geofenceStates[geofence.Name] = true;
                OnEnterGeofence(geofence);
            }
            else if (!isInside && _geofenceStates[geofence.Name])
            {
                _geofenceStates[geofence.Name] = false;
                OnExitGeofence(geofence);
            }
        }
    }

    private void OnEnterGeofence(GeofenceZone geofence)
    {
        Debug.Log($"Entered geofence: {geofence.Name}");
        OnEnter?.Invoke(geofence); // Trigger event
    }

    private void OnExitGeofence(GeofenceZone geofence)
    {
        Debug.Log($"Exited geofence: {geofence.Name}");
        OnExit?.Invoke(geofence); // Trigger event
    }
}
