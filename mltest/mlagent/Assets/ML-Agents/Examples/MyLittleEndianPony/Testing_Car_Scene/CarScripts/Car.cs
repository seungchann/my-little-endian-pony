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
public class Car : MonoBehaviour
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

    [SerializeField] private float maxSpeed;
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


    [SerializeField] private Renderer ReverseLight;
    [SerializeField] private Material ReverseLightOn;
    [SerializeField] private Material ReverseLightOff;

    [SerializeField] private Renderer BackLight;
    [SerializeField] private Material BackLightOn;
    [SerializeField] private Material BackLightOff;

    [SerializeField] private Vector3 RotationAxis;
    [SerializeField] private Handle handle;
    

    private void Start(){
        carBody = GetComponent<Rigidbody>();
    }

    private void Update(){}

    private void FixedUpdate(){
        GetInput();
        HandlerMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        steerAngle /= 2;
        fLCollider.steerAngle = steerAngle;
        fRCollider.steerAngle = steerAngle;

        rLCollider.steerAngle = steerAngle * -1 / 3;
        rRCollider.steerAngle = steerAngle * -1 / 3;

        if(horizontalInput > 0){
            handle.HandleObject.transform.localRotation = Quaternion.Lerp(handle.HandleObject.transform.localRotation, Quaternion.Euler(handle.rightMaxAngle), steerAngle / maxSteeringAngle);
        } else{
            handle.HandleObject.transform.localRotation = Quaternion.Lerp(handle.HandleObject.transform.localRotation, Quaternion.Euler(handle.leftMaxAngle), Math.Abs(steerAngle / maxSteeringAngle));
        }
            
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

    private void HandlerMotor() {   
        if(horizontalInput != 0) {
            if(horizontalInput > 0){
                carBody.centerOfMass = new Vector3(Math.Abs(steerAngle) / 50, -0.2f, 0);
            }
            else {
                carBody.centerOfMass = new Vector3((-1) * Math.Abs(steerAngle) / 50, -0.2f, 0);
            }
        } 
        
        fLCollider.motorTorque = verticalInput * motorForce;
        fRCollider.motorTorque = verticalInput * motorForce;
        carBody.centerOfMass = new Vector3(0, -0.9f, 0.5f);

        if(isBreaking){
            ApplyBreaking(breakForce * 10);
            BackLight.material = BackLightOn;
        }
        else{
            if (verticalInput != 0) {
                Rigidbody rb = GetComponent<Rigidbody>();
                float currentSpeed = rb.velocity.magnitude;
                if (currentSpeed <= maxSpeed) Accelerate();
                
                if (verticalInput < 0) { 
                    ReverseLight.material = ReverseLightOn;
                    BackLight.material = BackLightOn;
                }
                else { 
                    ReverseLight.material = ReverseLightOff;
                    BackLight.material = BackLightOff;
                }
            }
            // else { StopMotorTorque(); }
        }
    }

    private void StopMotorTorque() {
        fRCollider.motorTorque = 0f;
        fLCollider.motorTorque = 0f;
        rRCollider.motorTorque = 0f;
        rLCollider.motorTorque = 0f;

        fRCollider.brakeTorque = breakForce / 30;
        fLCollider.brakeTorque = breakForce / 30;
        rRCollider.brakeTorque = breakForce / 30;
        rLCollider.brakeTorque = breakForce / 30;
    }
    private void Accelerate()
    {
        fRCollider.brakeTorque = 0f;
        fLCollider.brakeTorque = 0f;
        rRCollider.brakeTorque = 0f;
        rLCollider.brakeTorque = 0f;
    }

    public void ApplyBreaking(float brakingForce)
    {
        fRCollider.brakeTorque = brakingForce;
        fLCollider.brakeTorque = brakingForce;
        rRCollider.brakeTorque = brakingForce;
        rLCollider.brakeTorque = brakingForce;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
}