using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Handle{
    public GameObject HandleObject;
    public Vector3 leftMaxAngle;
    public Vector3 rightMaxAngle;
}

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    private float rotated = 0f;

    private Rigidbody carBody;

    [SerializeField] private Handle handle;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider fLCollider;
    [SerializeField] private WheelCollider fRCollider;
    [SerializeField] private WheelCollider rRCollider;
    [SerializeField] private WheelCollider rLCollider;
    [SerializeField] private Transform fLTransform;
    
    [SerializeField] private Transform fRTransform;
    [SerializeField] private Transform rRTransform;
    [SerializeField] private Transform rLTransform;

    [SerializeField] private Material Red_light;
    [SerializeField] private Material Red_light_on;
    [SerializeField] private GameObject ReverseLight;
    
    [Header("Hinges Settings")]
    [SerializeField] private Vector3 RotationAxis;
    
    private void Start(){
        carBody = GetComponent<Rigidbody>();
    }

    private void Update(){
    }
    private void FixedUpdate(){
        GetInput();
        HandlerMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(fLCollider, fLTransform);
        UpdateSingleWheel(fRCollider, fRTransform);
        UpdateSingleWheel(rLCollider, rLTransform);
        UpdateSingleWheel(rRCollider, rRTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void HandleSteering()
    {
        // Debug.Log(horizontalInput);
        steerAngle = maxSteeringAngle * horizontalInput;
        fLCollider.steerAngle = steerAngle;
        fRCollider.steerAngle = steerAngle;
        if(horizontalInput > 0){
            handle.HandleObject.transform.localRotation = Quaternion.Lerp(handle.HandleObject.transform.localRotation, Quaternion.Euler(handle.rightMaxAngle), steerAngle / maxSteeringAngle);
        } else{
            handle.HandleObject.transform.localRotation = Quaternion.Lerp(handle.HandleObject.transform.localRotation, Quaternion.Euler(handle.leftMaxAngle), Math.Abs(steerAngle / maxSteeringAngle));
        }
            
    }

    private void HandlerMotor()
    {   
        if(verticalInput < 0) return ;
        if(horizontalInput != 0){
            if(horizontalInput > 0){
                fLCollider.motorTorque = verticalInput * motorForce * Math.Abs(steerAngle);
                fRCollider.motorTorque = verticalInput * motorForce;
                carBody.centerOfMass = new Vector3(Math.Abs(steerAngle) / 50, -0.2f, 0);
            }
            else{
                fRCollider.motorTorque = verticalInput * motorForce * Math.Abs(steerAngle);
                fLCollider.motorTorque = verticalInput * motorForce;
                carBody.centerOfMass = new Vector3((-1) * Math.Abs(steerAngle) / 50, -0.2f, 0);
            }
            
        }else{
            fLCollider.motorTorque = verticalInput * motorForce;
            fRCollider.motorTorque = verticalInput * motorForce;
            carBody.centerOfMass = new Vector3(0, 0, 0);
        }
        currentbreakForce = isBreaking ? breakForce : 0f;
        if(isBreaking){
            ReverseLight.GetComponent<Renderer>().material = Red_light_on;
            ApplyBreaking();
        }
        else{
            ReverseLight.GetComponent<Renderer>().material = Red_light;
            Accelerate();
        }
    }

    private void Accelerate()
    {
        fRCollider.brakeTorque = 0f;
        fLCollider.brakeTorque = 0f;
        rRCollider.brakeTorque = 0f;
        rLCollider.brakeTorque = 0f;
    }

    private void ApplyBreaking()
    {
        fRCollider.brakeTorque = currentbreakForce;
        fLCollider.brakeTorque = currentbreakForce;
        rRCollider.brakeTorque = currentbreakForce;
        rLCollider.brakeTorque = currentbreakForce;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
}
