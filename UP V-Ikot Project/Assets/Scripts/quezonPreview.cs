using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class quezonPreview : MonoBehaviour
{

	PreviewPull PreviewPull;
	public Sprite newSprite;

	void Start(){
		PreviewPull = GameObject.Find("CanvasMap").GetComponent<PreviewPull>();
		//ChangeTitle = GameObject.Find("Info").GetComponent<ChangeTitle>();
		
	}

    private void OnMouseDown(){
    	Debug.Log("clicked!");
    	PreviewPull.DisplayPOIPreview();
    	Text quezonPreviewTitle = GameObject.Find("CanvasMap/POIPreview/Info/POIName").GetComponent<Text>();
    	quezonPreviewTitle.text = "Quezon Hall";
    	
    	GameObject.Find("CanvasMap/POIPreview/Info/POIImage").GetComponent<Image>().sprite = newSprite;
    }
}
