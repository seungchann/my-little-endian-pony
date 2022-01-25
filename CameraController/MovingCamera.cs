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
    private int mainIndex = 0;
    private bool isSwitching = false;
    private bool isOrthographic = false;
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
    [SerializeField] private Camera[] MainCamera;
    [SerializeField] private float zoomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        foreach(Camera i in MainCamera) i.enabled = true;
        foreach(Camera i in cameraArray) i.enabled = false;
        SetCamera();
    }

    private void SetCamera()
    {
        location = cameraArray[index].transform.localPosition;
        rotation = cameraArray[index].transform.localRotation.eulerAngles;
        distance = cameraArray[index].fieldOfView;
        MainCamera[mainIndex].transform.localPosition = location;
        MainCamera[mainIndex].transform.localRotation = Quaternion.Euler(rotation);
        MainCamera[mainIndex].fieldOfView = distance;
    }

    private void SetCameraWithAnimation(){
        float weight = (Time.time - startTime) * speed / totalTime;
        if(MainCamera[mainIndex].transform.parent.gameObject.transform.localPosition != new Vector3(0,0,0) || MainCamera[mainIndex].transform.parent.gameObject.transform.localRotation.eulerAngles != new Vector3(0,0,0)){
            MainCamera[mainIndex].transform.parent.gameObject.transform.localPosition = new Vector3(0,0,0);
            MainCamera[mainIndex].transform.parent.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));

        }
        
        if(Vector3.Distance(MainCamera[mainIndex].transform.localPosition, location) < 0.1f){
            isSwitching = false;
            return ;
        } 
            
        
        localPosition = MainCamera[mainIndex].transform.localPosition;
        localRotation = MainCamera[mainIndex].transform.localRotation.eulerAngles;
        
        MainCamera[mainIndex].transform.localRotation = Quaternion.Lerp(Quaternion.Euler(localRotation), Quaternion.Euler(rotation), weight);
        MainCamera[mainIndex].transform.localPosition = Vector3.Lerp(localPosition, location, weight);
        MainCamera[mainIndex].fieldOfView = distance;
        // Debug.Log(weight);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomCamera();
        if(Input.GetKeyDown(KeyCode.V) && !isSwitching)
        {   
            getMainIndex();
            index = (index + 1) % (cameraArray.Length);
            isSwitching = true;
            GetParameters();
            startTime = Time.time;
        }
        if (isSwitching){
            SetCameraWithAnimation();
        }
    }

    private void getMainIndex()
    {
        float x = Input.mousePosition.x / Screen.width;
        float y = Input.mousePosition.y / Screen.height;
        for(int i = 0; i<MainCamera.Length; i++){
            

            if(MainCamera[i].rect.Contains(new Vector2(x, y))){
                mainIndex = i;
                break;
            } 
            Debug.Log(i);
        }
    }

    private void GetParameters()
    {
        location = cameraArray[index].transform.localPosition;
        rotation = cameraArray[index].transform.localRotation.eulerAngles;
        distance = cameraArray[index].fieldOfView;
        isOrthographic = cameraArray[index].orthographic;

        localPosition = MainCamera[mainIndex].transform.localPosition;
        localRotation = MainCamera[mainIndex].transform.localRotation.eulerAngles;

        cameraDistance = Vector3.Distance(MainCamera[mainIndex].transform.localPosition, location);

        deltaDistance = distance - MainCamera[mainIndex].fieldOfView;

        MainCamera[mainIndex].orthographic = isOrthographic;
        
        // Debug.Log("Location" + deltaLocation);
        // Debug.Log("Rotation" + deltaRotation);
    }

    private void ZoomCamera()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel")*-1*zoomSpeed;
        if(distance !=0){
            MainCamera[mainIndex].fieldOfView += distance;
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
