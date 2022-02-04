using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftIndicator : MonoBehaviour
{
    public Renderer leftIndicator;
    public Material leftIndicatorOn;
    public Material leftIndicatorOff;
    public KeyCode myKey;

    private bool isOn;
    private bool toggle;
    private int before;
    private int current;
    private int diff = 60;
    void Start()
    {
        before = Time.frameCount;
        isOn = false;
        toggle = false;
        leftIndicator.material = leftIndicatorOff;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(myKey)) {
            isOn = !isOn;
            if (isOn) {
                Toggle();
            }
            else {
                leftIndicator.material = leftIndicatorOff;
                toggle = false;
            }
        }
        if (isOn) {
            Toggle();
        }
    }

    public void TurnOn() {
        isOn = true;
        leftIndicator.material = leftIndicatorOn;
    }
    public void TurnOff() {
        isOn = false;
        leftIndicator.material = leftIndicatorOff;
    }
    public void Toggle() {
        if (!isOn) isOn = true;
        current = Time.frameCount;
            if (current - before >= diff) {
                before = current;
                toggle = !toggle;
                if (toggle) {
                    leftIndicator.material = leftIndicatorOn;
                }
                else {
                    leftIndicator.material = leftIndicatorOff;
                }
            }
    }
}

