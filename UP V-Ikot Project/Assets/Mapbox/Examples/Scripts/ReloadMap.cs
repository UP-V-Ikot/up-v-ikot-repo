namespace Mapbox.Examples
{
	using Mapbox.Geocoding;
	using UnityEngine.UI;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using System;
	using System.Collections;

	public class ReloadMap : MonoBehaviour
	{
		Camera _camera;
		Vector3 _cameraStartPos;
		AbstractMap _map;

		[SerializeField]
		ForwardGeocodeUserInput _forwardGeocoder;

		[SerializeField]
		Slider _zoomSlider;

		private HeroBuildingSelectionUserInput[] _heroBuildingSelectionUserInput;

		Coroutine _reloadRoutine;

		WaitForSeconds _wait;

		void Awake()
		{
			_camera = Camera.main;
			_cameraStartPos = _camera.transform.position;
			_map = FindObjectOfType<AbstractMap>();
			if(_map == null)
			{
				Debug.LogError("Error: No Abstract Map component found in scene.");
				return;
			}
			if (_zoomSlider != null)
			{
				_map.OnUpdated += () => { _zoomSlider.value = _map.Zoom; };
				_zoomSlider.onValueChanged.AddListener(Reload);
			}
			if(_forwardGeocoder != null)
			{
				_forwardGeocoder.OnGeocoderResponse += ForwardGeocoder_OnGeocoderResponse;
			}
			_heroBuildingSelectionUserInput = GetComponentsInChildren<HeroBuildingSelectionUserInput>();
			if(_heroBuildingSelectionUserInput != null)
			{
				for (int i = 0; i < _heroBuildingSelectionUserInput.Length; i++)
				{
					_heroBuildingSelectionUserInput[i].OnGeocoderResponse += ForwardGeocoder_OnGeocoderResponse;
				}
			}
			_wait = new WaitForSeconds(.3f);
		}

		void ForwardGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response)
		{

			float minLat = 14.646926951459918f; // Set the minimum latitude here.
			float maxLat = 14.663119425420888f; // Set the maximum latitude here.
			float minLon = 121.05908765935553f; // Set the minimum longitude here.
			float maxLon = 121.07378616381652f; // Set the maximum longitude here.

			if (null != response.Features && response.Features.Count > 0)
			{
				
				var center = response.Features[0].Center;

				double lat = center.x;
		        double lon = center.y;
		        Debug.Log("Search result: latitude=" + lat + ", longitude=" + lon);

		        if(center.x < minLat || center.x > maxLat || center.y < minLon || center.y > maxLon) {
		            Debug.Log("Search result is outside the specified area.");
		            return;
		        }
		        int zoom = _map.AbsoluteZoom;
		        _map.UpdateMap(center, zoom);
			}
		}

		void ForwardGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response, bool resetCamera)
		{
			if (response == null)
			{
				return;
			}
			if (resetCamera)
			{
				_camera.transform.position = _cameraStartPos;
			}
			ForwardGeocoder_OnGeocoderResponse(response);
		}

		void Reload(float value)
		{
			if (_reloadRoutine != null)
			{
				StopCoroutine(_reloadRoutine);
				_reloadRoutine = null;
			}
			_reloadRoutine = StartCoroutine(ReloadAfterDelay((int)value));
		}

		IEnumerator ReloadAfterDelay(int zoom)
		{
			yield return _wait;
			_camera.transform.position = _cameraStartPos;
			_map.UpdateMap(_map.CenterLatitudeLongitude, zoom);
			_reloadRoutine = null;
		}
	}
}