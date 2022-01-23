using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    private bool right_on;
    private bool left_on;
    private bool back_on;
    private bool front_on;
    [SerializeField] private Material Yellow_light;
    [SerializeField] private Material Yellow_light_on;
    [SerializeField] private Material Red_light;
    [SerializeField] private Material Red_light_on;
    [SerializeField] private Material White_light;
    [SerializeField] private Material White_light_on;
    [SerializeField] private GameObject HeadLight;
    [SerializeField] private GameObject BackLight;
    [SerializeField] private GameObject RightLight;
    [SerializeField] private GameObject LeftLight;
    
    // Start is called before the first frame update
    void Start()
    {
        right_on = false;
        left_on = false;
        front_on = false;
        back_on = false;
    }

    // Update is called once per frame
    void Update()
    {
        changeLight();
    }

    private void changeLight()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            if(right_on){
                RightLight.GetComponent<Renderer>().material = Yellow_light;
                right_on = false;
            } else{
                RightLight.GetComponent<Renderer>().material = Yellow_light_on;
                right_on = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            if(left_on){
                LeftLight.GetComponent<Renderer>().material = Yellow_light;
                left_on = false;
            } else{
                LeftLight.GetComponent<Renderer>().material = Yellow_light_on;
                left_on = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.H)){
            if(front_on){
                HeadLight.GetComponent<Renderer>().material = White_light;
                front_on = false;
            } else{
                HeadLight.GetComponent<Renderer>().material = White_light_on;
                front_on = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.B)){
            if(back_on){
                BackLight.GetComponent<Renderer>().material = Red_light;
                back_on = false;
            } else{
                BackLight.GetComponent<Renderer>().material = Red_light_on;
                back_on = true;
            }
        }
    }
}
