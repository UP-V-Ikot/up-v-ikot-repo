namespace Mapbox.Examples
{
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.MeshGeneration.Factories;

	public class ImmediatePositionWithLocationProvider : MonoBehaviour
	{

		public static double UserX;
		public static double UserY;

		bool _isInitialized;

		ILocationProvider _locationProvider;
		ILocationProvider LocationProvider
		{
			get
			{
				if (_locationProvider == null)
				{
					_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
				}

				return _locationProvider;
			}
		}

		Vector3 _targetPosition;

		void Start()
		{
			LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
		}

		void LateUpdate()
		{
			if (_isInitialized)
			{
				var map = LocationProviderFactory.Instance.mapManager;
				transform.localPosition = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);

				DirectionsFactory directionsFactory = FindObjectOfType<DirectionsFactory>();
				UserX = LocationProvider.CurrentLocation.LatitudeLongitude.x;
		    	UserY = LocationProvider.CurrentLocation.LatitudeLongitude.y;

		    	directionsFactory.UserCoordinates = new Vector2d(UserX, UserY);

			}
		}
	}
}