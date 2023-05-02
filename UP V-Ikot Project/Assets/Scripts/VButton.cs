using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VButton : MonoBehaviour
{   
    [SerializeField] GameObject[] floors;
    public VirtualButtonBehaviour upVb;
    public int currentFloorIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        upVb.RegisterOnButtonPressed(UpButtonPressed);
    }

    public void UpButtonPressed(VirtualButtonBehaviour upVb) {
        foreach (GameObject floor in floors) {
            floor.SetActive(false);
        }
        currentFloorIndex++;
        floors[currentFloorIndex % 5].SetActive(true);
    }
}
