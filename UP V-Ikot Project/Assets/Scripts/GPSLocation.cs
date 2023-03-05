using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GPSLocation : MonoBehaviour
{   

    public Text GPSStatus;
    public Text latitudeValue;
    public Text longitudeValue;
    public Text altitudeValue;
    public Text horizontalAccuracyValue;
    public Text timestampValue;
    public Text panelStatus;

    // To keep script running in different scenes
    public static GPSLocation instance;

    public GameObject arPanel;
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
    void Start()
    {   
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        // Initially set to inactive
        arPanel.SetActive(false);
        panelStatus.text = "Inactive";
        StartCoroutine(GPSLoc());
    }

    public void LoadMarkerlessARScene() {
        SceneManager.LoadScene(1);
    }

    public void LoadMarkerARScene() {
        SceneManager.LoadScene(2);
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
            GPSStatus.text = "Time Out";
            yield break;
        }
        
        if (Input.location.status == LocationServiceStatus.Failed) {
            GPSStatus.text = "Unable to determine device location";
            yield break;
        }
        else {
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    } 

    private void UpdateGPSData() {
        if (Input.location.status == LocationServiceStatus.Running) {
            LocationController.latitude = Input.location.lastData.latitude;
            LocationController.longitude = Input.location.lastData.longitude;

            foreach(Tuple<double, double, double, double, string> coordinate in coordinates) {
                if ((LocationController.latitude < coordinate.Item1 && LocationController.latitude > coordinate.Item2) && 
                (LocationController.longitude > coordinate.Item3 && LocationController.longitude < coordinate.Item4)) 
                {
                    LocationController.poiName = coordinate.Item5;
                    arPanel.SetActive(true);
                    panelStatus.text = "Active";
                    break;
                }
                else {
                    arPanel.SetActive(false);
                    panelStatus.text = "Inactive";
                }
            }
            

            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();
            altitudeValue.text = Input.location.lastData.altitude.ToString();
            horizontalAccuracyValue.text = Input.location.lastData.horizontalAccuracy.ToString();
            timestampValue.text = Input.location.lastData.timestamp.ToString();

        }
        else {
            GPSStatus.text = "Stop";
            arPanel.SetActive(false);
            panelStatus.text = "Inactive";
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
