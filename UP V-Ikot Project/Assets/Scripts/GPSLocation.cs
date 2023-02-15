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
    // Palma Coordinates:
    // Top = 14.65390; Bottom = 14.65341; Left = 121.06956; Right = 121.07019
    // Ryo Home Coordinates:
    // Top = 14.63462; Bottom = 14.63416; Left = 121.07305; Right = 121.07340
    public const double Top = 14.65390;
    public const double Bottom = 14.65341;
    public const double Left = 121.06956;
    public const double Right = 121.07019;

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

    public void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
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

            // Insert accept area conditional here
            if ((LocationController.latitude < Top && LocationController.latitude > Bottom) && 
                (LocationController.longitude > Left && LocationController.longitude < Right)) 
            {
                LocationController.poiName = "Palma Hall";
                arPanel.SetActive(true);
                panelStatus.text = "Active";
            }
            else {
                LocationController.poiName = "Unknown";
                arPanel.SetActive(false);
                panelStatus.text = "Inactive";
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
