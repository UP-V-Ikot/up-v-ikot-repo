namespace Mapbox.Unity.Map
{
	using System;
	using System.Collections;
	using Mapbox.Unity.Location;
	using Mapbox.Utils;
	using UnityEngine;

	public class InitializeMapWithLocationProvider : MonoBehaviour
	{

		public static double UserX;
		public static double UserY;

		[SerializeField]
		AbstractMap _map;

		ILocationProvider _locationProvider;
    
		private void Awake()
		{
			// Prevent double initialization of the map. 
			_map.InitializeOnStart = false;
		}

		protected virtual IEnumerator Start()
		{
			yield return null;
			_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
			_locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
		}

		void LocationProvider_OnLocationUpdated(Unity.Location.Location location)
		{
			

		    float minLatitude = 14.646926951459918f; // Set the minimum latitude here.
			float maxLatitude = 14.663119425420888f; // Set the maximum latitude here.
			float minLongitude = 121.05908765935553f; // Set the minimum longitude here.
			float maxLongitude = 121.07378616381652f; // Set the maximum longitude here.

			Debug.Log(location.LatitudeLongitude.x);
			Debug.Log(location.LatitudeLongitude.y);

		    // Check if the user's location is within the specified coordinates.
		    if (location.LatitudeLongitude.x >= minLatitude &&
		        location.LatitudeLongitude.x <= maxLatitude &&
		        location.LatitudeLongitude.y >= minLongitude &&
		        location.LatitudeLongitude.y <= maxLongitude)
		    {
		    	//UserX = location.LatitudeLongitude.x;
		    	//UserY = location.LatitudeLongitude.y;

		    	//directionsFactory.UserCoordinates = new Vector2d(UserX, UserY);
		        // User is within the specified coordinates. Initialize the map.
		        _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
		        _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
		    }
		    else
		    {
		        // User is outside the specified coordinates. Do not initialize the map.
		        Debug.Log("User is outside the specified coordinates. Map initialization skipped.");
		        _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
		        _map.Initialize(new Vector2d(14.6537, 121.0699), _map.AbsoluteZoom);
		    }
		}


	}
}
