using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class melchorPreview : MonoBehaviour
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
    	Text melchorPreviewTitle = GameObject.Find("CanvasMap/POIPreview/Info/POIName").GetComponent<Text>();
    	Text melchorPreviewText = GameObject.Find("CanvasMap/POIPreview/Info/POIText").GetComponent<Text>();

        GameObject.Find("CanvasMap/POIPreview/Info/POIImage").GetComponent<Image>().sprite = newSprite;
    	
        melchorPreviewTitle.text = PreviewPull.MelchorName.ToString(); 
        melchorPreviewText.text = PreviewPull.MelchorText.ToString();    	
    }
}
