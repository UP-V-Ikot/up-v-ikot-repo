using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class palmaPreview : MonoBehaviour
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
    	Text palmaPreviewTitle = GameObject.Find("CanvasMap/POIPreview/Info/POIName").GetComponent<Text>();
    	Text palmaPreviewText = GameObject.Find("CanvasMap/POIPreview/Info/POIText").GetComponent<Text>();

        GameObject.Find("CanvasMap/POIPreview/Info/POIImage").GetComponent<Image>().sprite = newSprite;
    	
        palmaPreviewTitle.text = PreviewPull.PalmaName.ToString(); 
        palmaPreviewText.text = PreviewPull.PalmaText.ToString();     	
    }
}
