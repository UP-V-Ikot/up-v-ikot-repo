using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPull : MonoBehaviour
{
    [SerializeField] private GameObject POIPreview;
    [SerializeField] private GameObject BackButton;
    
    public void DisplayPOIPreview(){
    	POIPreview.SetActive(true);
    	BackButton.SetActive(true);
    }
}
