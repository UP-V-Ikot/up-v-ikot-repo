using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    private float rotateSpeed = 0.2f;
    private float _startingPosition;
    private float initialDistance;
    private Vector3 initialScale;

    // Update is called once per frame
    void Update() {

        // foreach (Touch touch in Input.touches) {
        //     Debug.Log("Touching at: " + touch.position);
        //     Ray camRay = cam.ScreenPointToRay(touch.position);
        //     RaycastHit raycastHit;

        //     if(Physics.Raycast(camRay, out raycastHit, 10)) {
        //         switch (touch.phase) {
        //             case TouchPhase.Began:
        //                 Debug.Log("Touch phase began at: " + touch.position);
        //                 break;
        //             case TouchPhase.Moved:
        //                 Debug.Log("Touch phase moved");
        //                 transform.Rotate(touch.deltaPosition.y * rotateSpeed, 
        //                     -touch.deltaPosition.x * rotateSpeed, 0, Space.World);
        //                 break;
        //             case TouchPhase.Ended:
        //                 Debug.Log("Touch phase Ended");
        //                 break;
        //         }   

        //     }

        // }


        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase){
                case TouchPhase.Began:
                    _startingPosition = touch.position.x;
                    break;
                case TouchPhase.Moved:
                    transform.Rotate(touch.deltaPosition.y * rotateSpeed, 
                        -touch.deltaPosition.x * rotateSpeed, 0);
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Touch Phase Ended.");
                    break;
                
            }
        }
        else if (Input.touchCount == 2) {
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            if (touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled ||
                    touchTwo.phase == TouchPhase.Ended || touchTwo.phase == TouchPhase.Canceled) {
                return;
            }

            if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began) {
                initialDistance = Vector2.Distance(touchOne.position, touchTwo.position);
                initialScale = transform.localScale;
            }
            else if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved) {
                var currentDistance = Vector2.Distance(touchOne.position, touchTwo.position);

                // If pinch distance too small or accidentally touched
                if (Mathf.Approximately(initialDistance, 0)) {
                    return;
                }

                var factor = currentDistance / initialDistance;
                transform.localScale = initialScale * factor;
            }
        }
    }
}
