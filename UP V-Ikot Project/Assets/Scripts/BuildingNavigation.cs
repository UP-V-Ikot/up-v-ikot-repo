using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingNavigation : MonoBehaviour
{   
    public GameObject MalcolmBuilding;
    public GameObject PalmaBuilding;
    public GameObject MelchorBuilding;
    public GameObject QuezonBuilding;
    public GameObject OblationBuilding;

    public void BackButtonClick() {
        SceneManager.LoadScene(1);
    }

    void Start() {
        switch(LocationController.poiName)
        {
            case "Malcolm Hall":
                MalcolmBuilding.SetActive(true);
                break;
            case "Palma Hall":
                PalmaBuilding.SetActive(true);
                break;
            case "Melchor Hall":
                MelchorBuilding.SetActive(true);
                break;
            case "Quezon Hall":
                QuezonBuilding.SetActive(true);
                break;
            case "Oblation Statue":
                OblationBuilding.SetActive(true);
                break;
        }
    }

}
