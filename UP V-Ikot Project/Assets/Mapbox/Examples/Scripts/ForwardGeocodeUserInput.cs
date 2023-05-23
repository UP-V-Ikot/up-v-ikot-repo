namespace Mapbox.Examples
{
    using Mapbox.Unity;
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using Mapbox.Geocoding;
    using Mapbox.Utils;

    [RequireComponent(typeof(InputField))]
    public class ForwardGeocodeUserInput : MonoBehaviour
    {
        InputField _inputField;

        ForwardGeocodeResource _resource;

        Vector2d _coordinate;
        public Vector2d Coordinate
        {
            get { return _coordinate; }
        }

        bool _hasResponse;
        public bool HasResponse
        {
            get { return _hasResponse; }
        }

        public ForwardGeocodeResponse Response { get; private set; }

        public event Action<ForwardGeocodeResponse> OnGeocoderResponse = delegate { };

        private Vector2d _validAreaMinCoordinate = new Vector2d(14.646926951459918, 121.05908765935553);
        private Vector2d _validAreaMaxCoordinate = new Vector2d(14.663119425420888, 121.07378616381652);

        void Awake()
        {
            _inputField = GetComponent<InputField>();
            _inputField.onEndEdit.AddListener(HandleUserInput);
            _resource = new ForwardGeocodeResource("");
        }

        void HandleUserInput(string searchString)
        {
            _hasResponse = false;
            if (!string.IsNullOrEmpty(searchString))
            {
                _resource.Query = searchString;
                _resource.Bbox = new Vector2dBounds(_validAreaMinCoordinate, _validAreaMaxCoordinate);
                MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
            }
        }

        void HandleGeocoderResponse(ForwardGeocodeResponse res)
        {
            _hasResponse = true;
            if (null == res)
            {
                _inputField.text = "POI not found";
            }
            else if (null != res.Features && res.Features.Count > 0)
            {
                bool foundValidArea = false;

                foreach (var feature in res.Features)
                {
                    var center = feature.Center;
                    if (IsCoordinateWithinValidArea(center))
                    {

                        _coordinate = center;
                        foundValidArea = true;
                        break;
                    }
                }

                if (!foundValidArea)
                {
                    _inputField.text = "POI not found";
                    _coordinate = Vector2d.zero;
                }
            }
            else
            {
                _inputField.text = "POI not found";
                _coordinate = Vector2d.zero;
            }

            Response = res;
            OnGeocoderResponse(res);
        }

        bool IsCoordinateWithinValidArea(Vector2d coordinate)
        {
            return coordinate.x >= _validAreaMinCoordinate.x &&
                coordinate.x <= _validAreaMaxCoordinate.x &&
                coordinate.y >= _validAreaMinCoordinate.y &&
                coordinate.y <= _validAreaMaxCoordinate.y;
        }
    }
}