using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera carCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        cameraSwitch();
    }

    void cameraSwitch(){
        if(Input.GetKey("1")){
            mainCamera.enabled = true;
            carCamera.enabled = false;
        } else if (Input.GetKey("2")){
            mainCamera.enabled = false;
            carCamera.enabled = true;
        }
    }
}
