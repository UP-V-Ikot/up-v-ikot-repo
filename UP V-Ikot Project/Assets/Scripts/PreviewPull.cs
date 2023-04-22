using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using APIHandler;

public class PreviewPull : MonoBehaviour
{
    [SerializeField] private GameObject POIPreview;
    [SerializeField] private GameObject BackButton;

    //POI Names
    public static string MalcolmName;
    public static string MelchorName;
    public static string OblationName;
    public static string PalmaName;
    public static string QuezonName;

    //POI PreviewText
    public static string MalcolmText;
    public static string MelchorText;
    public static string OblationText;
    public static string PalmaText;
    public static string QuezonText;

    //POI History
    public static string MalcolmHist;
    public static string MelchorHist;
    public static string OblationHist;
    public static string PalmaHist;
    public static string QuezonHist;

    //POI Courses
    public static string MalcolmCourse;
    public static string MelchorCourse;
    public static string OblationCourse;
    public static string PalmaCourse;
    public static string QuezonCourse;
    
    //POI Office
    public static string MalcolmOffice;
    public static string MelchorOffice;
    public static string OblationOffice;
    public static string PalmaOffice;
    public static string QuezonOffice;
    

    void Start(){

    	StartCoroutine(GetRequest("https://v-ikot-api-production.up.railway.app/api/pois"));
    }

    IEnumerator GetRequest(string uri){
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)){
                yield return webRequest.SendWebRequest();
                switch(webRequest.result){
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(String.Format("Something went wrong: {0}", webRequest.error));
                        break;
                    case UnityWebRequest.Result.Success:
                        List<Root> data = JsonConvert.DeserializeObject<List<Root>>(webRequest.downloadHandler.text);
                        Debug.Log("retrieved");		

                        MalcolmName = data[0].name;
    					MelchorName = data[2].name; 
    					OblationName = data[4].name;
    					PalmaName = data[1].name;
    					QuezonName = data[3].name;

    					MalcolmText = data[0].history;
    					MelchorText = data[2].history; 
    					OblationText = data[4].history;
    					PalmaText = data[1].history;
    					QuezonText = data[3].history;

    					MalcolmHist = data[0].history;
    			 		MelchorHist = data[2].history;
     					OblationHist = data[4].history;
     					PalmaHist = data[1].history;
     					QuezonHist = data[3].history;

    
					    MalcolmCourse = data[0].courses;
					    MelchorCourse = data[2].courses;
					    OblationCourse = data[4].courses;
					    PalmaCourse = data[1].courses;
					    QuezonCourse = data[3].courses;
					    
					    
					    MalcolmOffice = data[0].offices;
					    MelchorOffice = data[2].offices;
					    OblationOffice = data[4].offices;
					    PalmaOffice = data[1].offices;
					    QuezonOffice = data[3].offices;

                        break;
                }
            }
        }
            

    
    public void DisplayPOIPreview(){
    	POIPreview.SetActive(true);
    	BackButton.SetActive(true);
    }
}
