using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CarAgent : Agent
{
    Rigidbody rBody;
    void Start() {
        rBody = GetComponent<Rigidbody>();    
    }

    public Transform Target;
    public Renderer CorrectRenderer;
    public Renderer WrongRenderer;
    public Material BlackMaterial;
    public Material RedMaterial;
    public Material BlueMaterial;
    public override void OnEpisodeBegin() {
        if (this.transform.localPosition.y < 0) {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        // FloorRenderer.material = BlackMaterial;

        float rx = 0;
        float rz = 0;

        rx = Random.value * 16 - 8;
        rz = Random.value * 16 - 8;

        Target.localPosition = new Vector3(rx, 0.5f, rz);
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public float forceMultiplier = 10;
    public GameObject viewModel = null;
    public override void OnActionReceived(ActionBuffers actions) {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);
        viewModel.transform.LookAt(Target);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 2.02f) {
            CorrectRenderer.material = BlueMaterial;
            WrongRenderer.material = BlackMaterial;
            SetReward(1.0f);
            EndEpisode();
        }

        else if (this.transform.localPosition.y < 0) {
            CorrectRenderer.material = BlackMaterial;
            WrongRenderer.material = RedMaterial;
            SetReward(-0.1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
