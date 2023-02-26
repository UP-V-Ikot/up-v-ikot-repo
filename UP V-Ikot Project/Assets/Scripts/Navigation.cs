using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    public void NavigationBarClick(GameObject activePanel) {
        foreach (GameObject panel in panels) {
            panel.SetActive(false);
        }
        activePanel.SetActive(true);
    }

    public void BackButtonClick(GameObject MarkerlessARPanel) {
        MarkerlessARPanel.SetActive(false);
    }
}