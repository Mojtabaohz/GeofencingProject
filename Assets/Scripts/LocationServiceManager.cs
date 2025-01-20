using System.Collections;
using TMPro;
using UnityEngine;
using Wintor.Util;

public class LocationServiceManager : MonoSingleton<LocationServiceManager>
{
    public bool IsLocationEnabled { get; private set; } = false;

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public float Accuracy { get; private set; }
    public float Altitude { get; private set; }

    public TMP_Text LocationText;
    void Start()
    {
        Input.location.Start();
        StartCoroutine(StartLocationService());
    }

    void Update()
    {
        if (LocationText != null && IsLocationEnabled)
        {
            LocationText.text = $"Lat: {Latitude}, Lon: {Longitude}, Accuracy: {Accuracy}m";
        }
    }
    IEnumerator StartLocationService()
    {
        // Check if location services are enabled by the user
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location services are not enabled by the user.");
            yield break;
        }

        // Wait until the service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Check if the service timed out or failed
        if (maxWait <= 0 || Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogError("Location service failed or timed out.");
            yield break;
        }

        IsLocationEnabled = true;
        Debug.Log("Location service started successfully.");

        // Fetch location updates in a loop
        StartCoroutine(FetchLocationUpdates());
    }

    IEnumerator FetchLocationUpdates()
    {
        while (IsLocationEnabled)
        {
            var locationData = Input.location.lastData;

            Latitude = locationData.latitude;
            Longitude = locationData.longitude;
            Accuracy = locationData.horizontalAccuracy;
            Altitude = locationData.altitude;

            Debug.Log($"Location: Lat {Latitude}, Lon {Longitude}, Accuracy {Accuracy}m, Altitude {Altitude}m");
            yield return new WaitForSeconds(5); // Update every 5 seconds
        }
    }
}
