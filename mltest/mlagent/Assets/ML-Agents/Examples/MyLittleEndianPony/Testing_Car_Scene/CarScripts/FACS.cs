using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FACS : MonoBehaviour
{
    public Transform lidar;
    public bool isLeft;
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;

    private bool isFourWayIndicatorOn = false;
    private int distance = 10;
    private int brakeForce = 10000;
    
    private LineRenderer lineRenderer;
    
    private Ray[] rayList = new Ray[91];

    void FixedUpdate() {
        for (var i = 0; i <= 30; i++) {
            int direction = isLeft ? -1 : 1;
            rayList[i] = new Ray(lidar.transform.position, Quaternion.Euler(0, (i - 15) * direction, 0) * lidar.transform.forward);
        }
        checkDirections();
    }

    void checkDirections() {
        Rigidbody rb = GetComponent<Rigidbody>();
        int cnt = 0;
        float hitDistance = distance * 2;
        float currentSpeed = rb.velocity.magnitude;
        for (var i = 0; i <= 90; i++) {
            RaycastHit hit;
            if (Physics.Raycast(rayList[i], out hit, distance)) {
                lastHit = hit.transform.gameObject;
                collision = hit.point;
                cnt += 1;
                hitDistance = hitDistance > hit.distance ? hit.distance : hitDistance;
            }
        }
        if (cnt > 0) {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
                gameObject.GetComponent<Car>().ApplyBreaking(brakeForce * currentSpeed * 10000);
                TurnOnFourWayIndicator();
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 1.2f);
        for (var i = 0; i <= 90; i++) {
            Gizmos.DrawLine(rayList[i].origin, rayList[i].origin + rayList[i].direction * distance);
        }
    }

    void TurnOnFourWayIndicator() {
        gameObject.GetComponent<LeftIndicator>().Toggle();
        gameObject.GetComponent<RightIndicator>().Toggle();
    }
}
