using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationInfo : MonoBehaviour
{   
    public Text longitudeVal;
    public Text latitudeVal;
    public Text poiVal;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("UpdateLocationInfo", 0.5f, 1f);
    }

    void UpdateLocationInfo() {
        longitudeVal.text = LocationController.longitude.ToString();
        latitudeVal.text = LocationController.latitude.ToString();
        poiVal.text = LocationController.poiName;
    }
}
