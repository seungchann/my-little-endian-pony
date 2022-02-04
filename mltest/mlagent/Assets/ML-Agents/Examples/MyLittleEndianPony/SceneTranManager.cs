using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTranManager : MonoBehaviour
{
    void Update() {
        if(Input.GetKey(KeyCode.M)) {
            SceneManager.LoadScene("gTest");
        }    
    }
}
