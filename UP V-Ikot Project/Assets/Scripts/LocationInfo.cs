using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocationInfo : MonoBehaviour
{  
    public Text poiVal;

    public GameObject MalcolmObject;
    public GameObject PalmaObject;
    public GameObject MelchorObject;
    public GameObject QuezonObject;
    public GameObject OblationObject;
    // Coords hardcoded for now; set to your location area to test
    // Ryo Home Coordinates:
    // Top = 14.63462; Bottom = 14.63416; Left = 121.07305; Right = 121.07340

    // Tuple Items: Top, Bottom, Left, Right, POIName
    public Tuple<double, double, double, double, string>[] coordinates = {
        Tuple.Create(14.65659, 14.65613, 121.07186, 121.07248, "Malcolm Hall"),
        Tuple.Create(14.65390, 14.65341, 121.06956, 121.07019, "Palma Hall"),
        Tuple.Create(14.65660, 14.65608, 121.06910, 121.07012, "Melchor Hall"),
        Tuple.Create(14.65513, 14.65472, 121.06494, 121.06525, "Quezon Hall"),
        Tuple.Create(14.65508, 14.65477, 121.06433, 121.06490, "Oblation Statue"),
    };

    // Start is called before the first frame update
    void Start() {
        switch(LocationController.poiName)
        {
            case "Malcolm Hall":
                MalcolmObject.SetActive(true);
                break;
            case "Palma Hall":
                PalmaObject.SetActive(true);
                break;
            case "Melchor Hall":
                MelchorObject.SetActive(true);
                break;
            case "Quezon Hall":
                QuezonObject.SetActive(true);
                break;
            case "Oblation Statue":
                OblationObject.SetActive(true);
                break;
        }

        StartCoroutine(GPSLoc());
    }

    IEnumerator GPSLoc() {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start(5f, 5f);

        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1) {
            yield break;
        }
        
        if (Input.location.status == LocationServiceStatus.Failed) {
            yield break;
        }
        else {
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    } 

    private void UpdateGPSData() {
        bool inBounds = false;
        if (Input.location.status == LocationServiceStatus.Running) {
            LocationController.latitude = Input.location.lastData.latitude;
            LocationController.longitude = Input.location.lastData.longitude;

            foreach(Tuple<double, double, double, double, string> coordinate in coordinates) {
                if ((LocationController.latitude < coordinate.Item1 && LocationController.latitude > coordinate.Item2) && 
                (LocationController.longitude > coordinate.Item3 && LocationController.longitude < coordinate.Item4)) 
                {
                    LocationController.poiName = coordinate.Item5;
                    inBounds = true;
                    break;
                }
            }

            if (!inBounds) {
                SceneManager.LoadScene("GPSLocation");
            }
    
            poiVal.text = LocationController.poiName;

        }
    }
}
