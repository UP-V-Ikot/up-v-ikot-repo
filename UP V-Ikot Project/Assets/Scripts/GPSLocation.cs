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
    public Text panelStatus;
    public Text poiName;

    public GameObject arPanel;
    public GameObject minimizedArPanel;
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
        panelStatus.text = "Inactive";
        StartCoroutine(GPSLoc());
        SceneManager.LoadScene("ZoomableMap", LoadSceneMode.Additive);
    }

    public void LoadMarkerlessARScene() {
        SceneManager.LoadScene("MarkerlessAR");
    }

    public void LoadMarkerARScene() {
        SceneManager.LoadScene("MarkerAR");
    }

    public void MinimizeARPanel() {
        arPanel.SetActive(false);
        minimizedArPanel.SetActive(true);
        poiName.text = LocationController.poiName;
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
        bool inBounds = false;
        if (Input.location.status == LocationServiceStatus.Running) {
            LocationController.latitude = Input.location.lastData.latitude;
            LocationController.longitude = Input.location.lastData.longitude;

            Debug.Log(LocationController.latitude);
            Debug.Log(LocationController.longitude);

            foreach(Tuple<double, double, double, double, string> coordinate in coordinates) {
                if ((LocationController.latitude < coordinate.Item1 && LocationController.latitude > coordinate.Item2) && 
                (LocationController.longitude > coordinate.Item3 && LocationController.longitude < coordinate.Item4)) 
                {
                    LocationController.poiName = coordinate.Item5;
                    if (!minimizedArPanel.activeSelf) {
                        arPanel.SetActive(true);
                    }
                    inBounds = true;
                    panelStatus.text = "Active";
                    break;
                }
            }

            if (!inBounds) {
                if (arPanel.activeSelf) {
                    arPanel.SetActive(false); 
                }

                if (minimizedArPanel.activeSelf) {
                    minimizedArPanel.SetActive(false);
                }
                panelStatus.text = "Inactive";
            }
            

            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longitudeValue.text = Input.location.lastData.longitude.ToString();

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
