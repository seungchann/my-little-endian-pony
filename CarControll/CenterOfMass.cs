using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Vector3 com;
    private Rigidbody carBody;
    public bool Awake;
    void Start()
    {
        carBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        carBody.centerOfMass = com;
        carBody.WakeUp();
        Awake = !carBody.IsSleeping();
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * com, 1f);
    }
}
