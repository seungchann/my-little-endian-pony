using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWheel : MonoBehaviour
{
	public WheelCollider wheelCollider;
	private Vector3 wheelCCenter;
	private RaycastHit hit;
	void Start () {
	}
	void Update () {
        wheelCCenter = wheelCollider.transform.TransformPoint(wheelCollider.center);
		if ( Physics.Raycast(wheelCCenter, -wheelCollider.transform.up, out hit, 
        wheelCollider.suspensionDistance + wheelCollider.radius) ) {
            transform.position = hit.point + (wheelCollider.transform.up * wheelCollider.radius);
            print ("충돌발생");
		} else {
			transform.position = wheelCCenter - (wheelCollider.transform.up * wheelCollider.suspensionDistance);
			print ("비충돌");
		}
		transform.position += new Vector3(0f, 3f, 0f);
	}
	void FixedUpdate() {	
	}
}
