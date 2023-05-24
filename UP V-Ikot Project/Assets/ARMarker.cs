using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMarker : MonoBehaviour
{   
    [SerializeField] GameObject[] floors;
    public GameObject nextFloorBtn;
    public int currentFloorIndex = 1;

    public void ShowNextFloorButton() {
        nextFloorBtn.SetActive(true);
    }

    public void HideNextFloorButton() {
        nextFloorBtn.SetActive(false);
    }

    public void NextFloorButtonClick() {
        foreach (GameObject floor in floors) {
            floor.SetActive(false);
        }
        currentFloorIndex++;
        floors[currentFloorIndex % 5].SetActive(true);
    }
}
