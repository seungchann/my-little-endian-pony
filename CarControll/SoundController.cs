using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private float horizental;
    private float vertical;
    private Rigidbody carBody;
    [SerializeField] private AudioSource Starting;
    [SerializeField] private AudioSource Stoping;
    [SerializeField] private AudioSource Driving;

    
    void start(){
        carBody = GetComponent<Rigidbody>();
    }   

    void Update(){
        getInfo();
    }

    private void getInfo()
    {
        horizental = Input.GetAxis("Horizental");
        vertical = Input.GetAxis("Vertical");
    }
}