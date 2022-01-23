using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject lightGameObject = new GameObject("The Light");
        Light lightComp = lightGameObject.AddComponent<Light>();
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
