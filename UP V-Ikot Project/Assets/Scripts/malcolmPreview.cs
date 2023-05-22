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

public class malcolmPreview : MonoBehaviour
{

	PreviewPull PreviewPull;
	public Sprite newSprite;

	void Start(){
		PreviewPull = GameObject.Find("CanvasMap").GetComponent<PreviewPull>();
	}

    private void OnMouseDown(){
    	Debug.Log("clicked!");
    	PreviewPull.DisplayPOIPreview();
    	Text malcolmPreviewTitle = GameObject.Find("CanvasMap/POIPreview/Info/POIName").GetComponent<Text>();
        Text malcolmPreviewText = GameObject.Find("CanvasMap/POIPreview/Info/POIText").GetComponent<Text>();
    	
    	GameObject.Find("CanvasMap/POIPreview/Info/POIImage").GetComponent<Image>().sprite = newSprite;

        malcolmPreviewTitle.text = PreviewPull.MalcolmName.ToString(); 
        malcolmPreviewText.text = PreviewPull.MalcolmText.ToString(); 
    }
}
