using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraAdjust : MonoBehaviour
{
    [SerializeField] float sceneWidth = 10;
   // public Camera auxiliarCamera;

    private void Start()
    {
        if(Application.isPlaying)
        {
            float unitsPerPixel = sceneWidth / Screen.width;

            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

            Camera.main.orthographicSize = desiredHalfHeight;
        //    auxiliarCamera.orthographicSize = desiredHalfHeight;
            enabled = false;
        }

        
    }

    private void Update()
    {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        Camera.main.orthographicSize = desiredHalfHeight;
    }
}
