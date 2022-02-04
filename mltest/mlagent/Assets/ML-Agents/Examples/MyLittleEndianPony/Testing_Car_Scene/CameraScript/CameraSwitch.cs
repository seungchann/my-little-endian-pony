using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // public Camera FirstPersonView;
    // public Camera BackView;

    private int index = 0;
    public Camera[] cameraArray;


    void Start()
    {
        cameraArray[index].enabled = true;
        // cameraArray = new Camera[]{FirstPersonView, BackView};
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) {
            SwitchCamera();
        }
    }

    void SwitchCamera() {
        cameraArray[index].enabled = false;
        index = (index + 1) % cameraArray.Length;
        cameraArray[index].enabled = true;
    }
}
