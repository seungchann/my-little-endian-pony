using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentbreakForce;
    private bool isBreaking;

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
    

    private void Start(){
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
        steerAngle = maxSteeringAngle * horizontalInput;
        fLCollider.steerAngle = steerAngle;
        fRCollider.steerAngle = steerAngle;
    }

    private void HandlerMotor()
    {
        fLCollider.motorTorque = verticalInput * motorForce;
        fRCollider.motorTorque = verticalInput * motorForce;
        rLCollider.motorTorque = verticalInput * motorForce;
        rRCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        if(isBreaking){
            ApplyBreaking();
        }
        else{
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
