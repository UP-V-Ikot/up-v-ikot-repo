using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARNavigation : MonoBehaviour
{   
    [SerializeField] GameObject[] panels;

    public void OptionBarClick(GameObject activePanel) {
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        activePanel.SetActive(true);
    }

    // Goes back to the original main menu screen
    public void BackButtonClick() {
        SceneManager.LoadScene(0);
    }

    // Closes the active panel in the scene
    public void ExitButtonClick(GameObject activePanel) {
        activePanel.SetActive(false);
    }
}
