using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTranManager2 : MonoBehaviour
{
    void Update() {
        if(Input.GetKey(KeyCode.M)) {
            SceneManager.LoadScene("Testing_Car_Scene");
        }    
    }
}
