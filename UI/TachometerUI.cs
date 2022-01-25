using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TachometerUI : MonoBehaviour
{
    [SerializeField] private GameObject needle;
    [SerializeField] private Vector3 startAngle;
    private Vector3 midleAngle = new Vector3(0,0,0);
    [SerializeField] private Vector3 endAngle;
    private Rigidbody carBody;
    private float speed;

    private float maxSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        carBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = carBody.velocity.magnitude * 3.6f;
        if(speed/maxSpeed <= 0.5f)
            needle.transform.localRotation = Quaternion.Euler(Vector3.Lerp(startAngle, midleAngle, 2 * speed / maxSpeed));
        else
            needle.transform.localRotation = Quaternion.Euler(Vector3.Lerp(midleAngle, endAngle, 2 * speed / maxSpeed));
    }
}
