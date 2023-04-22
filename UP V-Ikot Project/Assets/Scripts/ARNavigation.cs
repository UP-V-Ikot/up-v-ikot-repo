using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARNavigation : MonoBehaviour
{   
    [SerializeField] GameObject[] panels;

    public void OptionBarClick(GameObject activePanel) {
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        activePanel.SetActive(true);

        if(LocationController.poiName == "Melchor Hall"){
            if(panels[0].activeSelf == true){
                Text HistoryText = GameObject.Find("Canvas/HistoryPanel/Text").GetComponent<Text>();
                HistoryText.text = "History Panel\n\n" + PreviewPull.MelchorHist.ToString();
                }
            if(panels[1].activeSelf == true){
                Text CourseText = GameObject.Find("Canvas/CoursePanel/Text").GetComponent<Text>();
                CourseText.text = "Course Panel\n\n" + PreviewPull.MelchorCourse.ToString();
                }
            if(panels[2].activeSelf == true){
                Text OfficeText = GameObject.Find("Canvas/OfficePanel/Text").GetComponent<Text>();
                OfficeText.text = "Office Panel\n\n" + PreviewPull.MelchorOffice.ToString();
                }
            }
    }

    // Goes back to the original main menu screen
    public void BackButtonClick() {
        SceneManager.LoadScene("GPSLocation");
    }

    // Closes the active panel in the scene
    public void ExitButtonClick(GameObject activePanel) {
        activePanel.SetActive(false);
    }

    public void BuildingButtonClick() {
        SceneManager.LoadScene("BuildingScene");
    }

}
