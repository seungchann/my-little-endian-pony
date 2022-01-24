using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    [SerializeField] private float totalTime;

    [Range(0.0f, 100f)]
    [SerializeField] private float speed;

    private bool isSwitching = false;
    private float startTime;
    private float cameraDistance;
    private Vector3 location;
    private Vector3 rotation;
    private float distance;
    private Vector3 deltaLocation;
    private Vector3 deltaRotation;
    private float deltaDistance;

    private Vector3 localPosition;
    private Vector3 localRotation;

    private int index = 0;
    public Camera[] cameraArray;
    [SerializeField] private Transform CameraArm;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private float zoomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        MainCamera.enabled = true;
        SetCamera();
    }

    private void SetCamera()
    {
        location = cameraArray[index].transform.localPosition;
        rotation = cameraArray[index].transform.localRotation.eulerAngles;
        distance = cameraArray[index].fieldOfView;
        MainCamera.transform.localPosition = location;
        MainCamera.transform.localRotation = Quaternion.Euler(rotation);
        MainCamera.fieldOfView = distance;
    }

    private void SetCameraWithAnimation(){
        float weight = (Time.time - startTime) * speed / totalTime;
        if(MainCamera.transform.parent.gameObject.transform.localPosition != new Vector3(0,0,0) || MainCamera.transform.parent.gameObject.transform.localRotation.eulerAngles != new Vector3(0,0,0)){
            MainCamera.transform.parent.gameObject.transform.localPosition = new Vector3(0,0,0);
            MainCamera.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));

        }
        if(Vector3.Distance(MainCamera.transform.localPosition, location) < 0.1f){
            Debug.Log(Vector3.Distance(MainCamera.transform.localPosition, location));
            isSwitching = false;
            return ;
        }
        localPosition = MainCamera.transform.localPosition;
        localRotation = MainCamera.transform.localRotation.eulerAngles;
        MainCamera.transform.rotation = Quaternion.Euler(Vector3.Lerp(localRotation, rotation, weight));
        MainCamera.transform.localPosition = Vector3.Lerp(localPosition, location, weight);
        MainCamera.fieldOfView += deltaDistance * totalTime / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomCamera();
        if(Input.GetKeyDown(KeyCode.V) && !isSwitching)
        {
            index = (index + 1) % (cameraArray.Length);
            isSwitching = true;
            GetParameters();
            startTime = Time.time;
        }
        if (isSwitching){
            SetCameraWithAnimation();
        }
    }

    private void GetParameters()
    {
        location = cameraArray[index].transform.localPosition;
        rotation = cameraArray[index].transform.localRotation.eulerAngles;
        distance = cameraArray[index].fieldOfView;

        localPosition = MainCamera.transform.localPosition;
        localRotation = MainCamera.transform.localRotation.eulerAngles;

        cameraDistance = Vector3.Distance(MainCamera.transform.localPosition, location);

        deltaDistance = distance - MainCamera.fieldOfView;
        Debug.Log("Location" + deltaLocation);
        Debug.Log("Rotation" + deltaRotation);
    }

    private void ZoomCamera()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel")*-1*zoomSpeed;
        if(distance !=0){
            MainCamera.fieldOfView += distance;
        }

    }

    private void MoveCamera()
    {
        if(Input.GetMouseButton(0)){
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 cameraAngle = CameraArm.rotation.eulerAngles;

            CameraArm.rotation = Quaternion.Euler(cameraAngle.x - mouseMovement.y, cameraAngle.y + mouseMovement.x, cameraAngle.z);
        }
    }
}
