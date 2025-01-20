using System;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class GeofenceUtilsTests
    {
        [Test]
        public void CalculateDistance_ValidCoordinates_ReturnsCorrectDistance()
        {
            // Example: San Francisco (37.7749, -122.4194) to Los Angeles (34.0522, -118.2437)
            double lat1 = 37.7749, lon1 = -122.4194; // San Francisco
            double lat2 = 34.0522, lon2 = -118.2437; // Los Angeles

            double expectedDistance = 559000; // Approximate distance in meters (559 km)
            double actualDistance = GeofenceUtils.CalculateDistance(lat1, lon1, lat2, lon2);

            double tolerance = 1000; // Define your acceptable tolerance
            bool areEqual = Math.Abs(expectedDistance - actualDistance) <= tolerance;
            Assert.IsTrue(areEqual, $"Distance calculation is incorrect. Expected: {expectedDistance}, Actual: {actualDistance}");
        }
    }
}
