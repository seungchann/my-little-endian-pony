using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRect : MonoBehaviour{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera[] cameraArray;
    

    void Awake(){
        MainCamera.rect = new Rect(0.0f, 0, 0.5f, 1);
        cameraArray[0].rect = new Rect(0.5f, 0, 1.0f, 0.5f);
        cameraArray[0].depth = -1;
        cameraArray[1].depth = -2;
        cameraArray[1].rect = new Rect(0.5f, 0.5f, 1.0f, 1.0f);
        cameraArray[0].enabled = true;
        cameraArray[1].enabled = true;
    }
}