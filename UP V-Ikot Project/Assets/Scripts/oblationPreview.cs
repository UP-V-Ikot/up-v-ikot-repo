using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using APIHandler;
public class oblationPreview : MonoBehaviour
{

	PreviewPull PreviewPull;
	public Sprite newSprite;

	void Start(){
		PreviewPull = GameObject.Find("CanvasMap").GetComponent<PreviewPull>();
		
	}

    private void OnMouseDown(){
    	Debug.Log("clicked!");
    	PreviewPull.DisplayPOIPreview();
    	Text oblationPreviewTitle = GameObject.Find("CanvasMap/POIPreview/Info/POIName").GetComponent<Text>();
        Text oblationPreviewText = GameObject.Find("CanvasMap/POIPreview/Info/POIText").GetComponent<Text>();
    	
    	GameObject.Find("CanvasMap/POIPreview/Info/POIImage").GetComponent<Image>().sprite = newSprite;

        oblationPreviewTitle.text = PreviewPull.OblationName.ToString(); 
        oblationPreviewText.text = PreviewPull.OblationText.ToString(); 
    }
}
