namespace Mapbox.Unity.MeshGeneration.Factories
{
    using UnityEngine;
    using Mapbox.Directions;
    using System.Collections.Generic;
    using System.Linq;
    using Mapbox.Unity.Map;
    using Data;
    using Modifiers;
    using Mapbox.Utils;
    using Mapbox.Unity.Utilities;
    using System.Collections;

    public class DirectionsFactory : MonoBehaviour
    {
        public Vector2d UserCoordinates;
        public Vector2d POICoordinates;

        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        MeshModifier[] MeshModifiers;
        [SerializeField]
        Material _material;

        [SerializeField]
        Transform[] _waypoints;
        private List<Vector3> _cachedWaypoints;

        [SerializeField]
        [Range(1, 10)]
        private float UpdateFrequency = 2;

        private Directions _directions;
        private int _counter;

        GameObject _directionsGO;
        private bool _recalculateNext;

        private bool _isProcessingPath;

        protected virtual void Awake()
        {

            if (_map == null)
            {
                _map = FindObjectOfType<AbstractMap>();
            }
            _directions = MapboxAccess.Instance.Directions;
           // _map.OnInitialized += Query;
            _map.OnUpdated += Query;
            Debug.Log("awakecheck");
        }

        public void Start(){
        	StartCoroutine(QueryTimer());
        }

        public void QueryStart()
        {
        	//_map.OnUpdated -= Query;
        	Debug.Log("querystartcheck");

        	foreach (var modifier in MeshModifiers)
			{
				modifier.Initialize();
			}

            _map.OnUpdated += Query;

        }

        protected virtual void OnDestroy()
		{
			//_map.OnInitialized -= Query;
			Debug.Log("destroycheck");
			_map.OnUpdated -= Query;
		}

		public void ClearPath()
        {
        	POICoordinates = new Vector2d(0.00000, 0.00000);
        	//_map.OnUpdated -= Query;
        	_isProcessingPath = false;
        	
            if (_directionsGO != null)
            {
            	Debug.Log("destroycheck");
                _directionsGO.Destroy();
            }
            Debug.Log("clearcheck");
        }

        public void Query()
        {
        	if (!UserCoordinates.Equals(new Vector2d(0.00000, 0.00000)) && !POICoordinates.Equals(new Vector2d(0.00000, 0.00000)))
            {
		        if (_directionsGO != null){
		            
		            float minZoomLevel = 17f;
		            float maxZoomLevel = 17f;

		            float currentZoomLevel = _map.Zoom;

		            if (currentZoomLevel < minZoomLevel || currentZoomLevel > maxZoomLevel)
		            {
		                float targetZoomLevel = Mathf.Clamp(currentZoomLevel, minZoomLevel, maxZoomLevel);
		                _map.UpdateMap(targetZoomLevel);
		            }
		        }

                _isProcessingPath = true;
                Debug.Log("query success");
                Debug.Log(POICoordinates);

                var wp = new[]
                {
                    UserCoordinates,
                    POICoordinates
                };

                var _directionResource = new DirectionResource(wp, RoutingProfile.Walking);
                _directionResource.Steps = true;
                _directions.Query(_directionResource, HandleDirectionsResponse);
            }
            else
            {
                Debug.LogWarning("UserCoordinates and/or POICoordinates are not set!");
                Debug.Log(UserCoordinates);
            }
        }
        
        public IEnumerator QueryTimer()
		{
			while (true)
			{
				yield return null;

				if (_recalculateNext)
				{
					Query();
					_recalculateNext = false;
				}
			}
		}

        void HandleDirectionsResponse(DirectionsResponse response)
        {
        	Debug.Log("handlebich");
        	if (!_isProcessingPath)
            {
                return; // Stop execution if ClearPath was called
            }

            if (response == null || null == response.Routes || response.Routes.Count < 1)
            {
                return;
            }

            var meshData = new MeshData();
            var dat = new List<Vector3>();

            foreach (var point in response.Routes[0].Geometry)
            {
                var geoPosition = Conversions.GeoToWorldPosition(point.x, point.y, _map.CenterMercator, _map.WorldRelativeScale);
                dat.Add(new Vector3((float)(geoPosition.x), 30f, (float)(geoPosition.y))); // set the Z-coordinate to zero
            }

            var feat = new VectorFeatureUnity();
            feat.Points.Add(dat);

            foreach (MeshModifier mod in MeshModifiers.Where(x => x.Active))
            {
                mod.Run(feat, meshData, _map.WorldRelativeScale);
            }

            CreateGameObject(meshData);
        }

        GameObject CreateGameObject(MeshData data)
        {
        	if (!_isProcessingPath)
            {
                return null; // Stop execution if ClearPath was called
            }

        	Debug.Log("drawbich");
            if (_directionsGO != null)
            {
                _directionsGO.Destroy();
            }
            _directionsGO = new GameObject("direction waypoint " + " entity");
            var mesh = _directionsGO.AddComponent<MeshFilter>().mesh;
            mesh.subMeshCount = data.Triangles.Count;

            mesh.SetVertices(data.Vertices);
            _counter = data.Triangles.Count;
            for (int i = 0; i < _counter; i++)
            {
                var triangle = data.Triangles[i];
                mesh.SetTriangles(triangle, i);
            }

            _counter = data.UV.Count;
            for (int i = 0; i < _counter; i++)
            {
                 var uv = data.UV[i];
            mesh.SetUVs(i, uv);
        }

        mesh.RecalculateNormals();

        var renderer = _directionsGO.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Unlit/Color")); // Use your desired material

        // Change color
        renderer.material.color = new Color(0.5f, 0f, 0f); // Use your desired color

        // Change thickness
        renderer.material.SetFloat("_LineWidth", 0.05f); // Use your desired thickness

        return _directionsGO;
    }
}
}