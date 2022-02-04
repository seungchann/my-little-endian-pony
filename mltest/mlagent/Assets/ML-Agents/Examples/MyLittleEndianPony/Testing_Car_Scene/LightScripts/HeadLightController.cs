using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLightController : MonoBehaviour
{
    public Light leftHeadLight;
    public Light rightHeadLight;
    public Renderer headlight;
    public Material headlightOn;
    public Material headlightOff;

    private bool isOn;
    void Start()
    {
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            isOn = !isOn;
            if (isOn) {
                headlight.material = headlightOn;
                leftHeadLight.enabled = true;
                rightHeadLight.enabled = true;
            }
            else {
                headlight.material = headlightOff;
                leftHeadLight.enabled = false;
                rightHeadLight.enabled = false;
            }
        }
    }
}
