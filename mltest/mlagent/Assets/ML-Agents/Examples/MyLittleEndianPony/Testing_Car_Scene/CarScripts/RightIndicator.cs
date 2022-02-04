using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightIndicator : MonoBehaviour
{
    public Renderer rightIndicator;
    public Material rightIndicatorOn;
    public Material rightIndicatorOff;
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
        rightIndicator.material = rightIndicatorOff;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(myKey)) {
            isOn = !isOn;
            if (isOn) {
                Toggle();
            }
            else {
                rightIndicator.material = rightIndicatorOff;
                toggle = false;
            }
        }
        if (isOn) {
            Toggle();
        }
    }

    public void TurnOn() {
        isOn = true;
        rightIndicator.material = rightIndicatorOn;
    }
    public void TurnOff() {
        isOn = false;
        rightIndicator.material = rightIndicatorOff;
    }
    public void Toggle() {
        if (!isOn) isOn = true;
        current = Time.frameCount;
            if (current - before >= diff) {
                before = current;
                toggle = !toggle;
                if (toggle) {
                    rightIndicator.material = rightIndicatorOn;
                }
                else {
                    rightIndicator.material = rightIndicatorOff;
                }
            }
    }
}

