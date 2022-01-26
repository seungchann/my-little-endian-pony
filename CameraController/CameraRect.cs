using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRect : MonoBehaviour{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera[] cameraArray;
    [SerializeField] private int num;
    private int depth = 1;
    

    void Awake()
    {   
        foreach(Camera i in cameraArray) {
            i.enabled = false;
            i.depth = depth;
            depth += 1;
        }
        MainCamera.enabled = true;
        ViewRect();
        if(num > (cameraArray.Length + 1)){
            Debug.LogWarning("num 설정 제대로");
            num = 1;
        } 
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            num = 1;
            ViewRect();
        } else if(Input.GetKeyDown(KeyCode.Alpha2)){
            num = 2;
            ViewRect();
        } else if(Input.GetKeyDown(KeyCode.Alpha3)){
            num = 3;
            ViewRect();
        } else if(Input.GetKeyDown(KeyCode.Alpha4)){
            num = 4;
            ViewRect();
        }

    }

    private void ViewRect()
    {
        if(num > (cameraArray.Length + 1)) return;
        if(num == 1){
            MainCamera.rect = new Rect(0f, 0f, 1f, 1f);
            for(int i=0;i<cameraArray.Length;i++){
                cameraArray[i].enabled = false;
            }
        }
        else if(num == 2){
            MainCamera.rect = new Rect(0.0f, 0f, 0.5f, 1);
            cameraArray[0].rect = new Rect(0.5f, 0f, 1, 1);
            cameraArray[0].enabled = true;
            for(int i=1;i<cameraArray.Length;i++){
                cameraArray[i].enabled = false;
            }
        }
        else if(num == 3){
            MainCamera.rect = new Rect(0.0f, 0, 0.5f, 1);
            cameraArray[0].rect = new Rect(0.5f, 0, 1.0f, 0.5f);
            cameraArray[1].rect = new Rect(0.5f, 0.5f, 1.0f, 1.0f);
            cameraArray[0].enabled = true;
            cameraArray[1].enabled = true;
            for(int i=2;i<cameraArray.Length;i++){
                cameraArray[i].enabled = false;
            }
        }
        else if(num == 4){
            MainCamera.rect = new Rect(0.0f, 0.5f, 0.5f, 1);
            cameraArray[2].rect = new Rect(0.5f, 0, 1.0f, 0.5f);
            cameraArray[0].rect = new Rect(0.5f, 0.5f, 1.0f, 1.0f);
            cameraArray[0].enabled = true;
            cameraArray[1].enabled = true;
            cameraArray[1].rect = new Rect(0.0f, 0f, 0.5f, 0.5f);
            cameraArray[2].enabled = true;
        }
        
    }
}
