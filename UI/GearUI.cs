using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUI : MonoBehaviour
{
    bool changeXY;
    bool canMove;
    float currentX;
    float currentY;
    float mX;
    float mY;
    float currentChanged;

    float timer;
    bool recentlyGear;

    Dictionary<float, float> GetGear;

    float[] ChangeXYGear;


    [SerializeField] private GameObject Gear;
    [SerializeField] private RectTransform GearTransform;
    [SerializeField] private AudioSource GearSound;
    // Start is called before the first frame update
    void Start()
    {
        currentChanged = 0f;
        mX = 0.0f;
        mY = 0f;

        recentlyGear = false;
        timer = 0.0f;
        changeXY = false;
        canMove = true;

        currentX = -68.9f;
        currentY = 18.4f;

        GearTransform.anchoredPosition.Set(currentX, currentY);
        ChangeXYGear = new float[3];
        ChangeXYGear[0] = -68.8f;
        ChangeXYGear[1] = -24.8f;
        ChangeXYGear[2] = 17.8f;
        InitDict();
    }

    private void InitDict()
    {
        GetGear = new Dictionary<float, float>();

        GetGear.Add(-68.8f, 18.4f);
        GetGear.Add(-68.79f, -70.4f);
        GetGear.Add(-24.8f, 18.4f);
        GetGear.Add(-24.79f, -70.4f);
        GetGear.Add(17.8f, 18.4f);
    }

    // Update is called once per frame
    void Update()
    {   
        changeGear();
        moveGear();
    }

    private void changeGear(){
        foreach(KeyValuePair<float, float> i in GetGear){
            float x = GearTransform.anchoredPosition.x;
            float y = GearTransform.anchoredPosition.y;
            if(Vector2.Distance(new Vector2(x,y), new Vector2(i.Key, i.Value)) <= 0.4f){
                // Debug.Log(i.Key + i.Value);
                float p = i.Key + i.Value;
                // if(p==-50.4f)
                //     CarController.motorForce = 300f;
                // else if(p==-139.19f)
                //     CarController.motorForce = 500f;
                // else if(p==-6.4f)
                //     CarController.motorForce = 800f;
                // else if(p==-95.19f)
                //     CarController.motorForce = 1200f;
                // else if(p==36.2f)
                //     CarController.motorForce = -300f;

                break;
            }
        }
    }
    private void moveGear(){
        float x = GearTransform.anchoredPosition.x;
        float y = GearTransform.anchoredPosition.y;
        bool gx = false;
        bool gy = false;

            if(Input.GetMouseButton(2))
            {
                float movementX = Input.GetAxis("Mouse X") * 3;
                float movementY = Input.GetAxis("Mouse Y") * 3;
                if ((x + movementX) < -68.9f) movementX = -68.9f - x;
                if ((x + movementX) > 17.9f) movementX = 17.9f - x;
                if ((y + movementY) < -70.5f) movementY = -70.5f - y;
                if ((y + movementY) > 18.5f) movementY = 18.5f - y;
                
                foreach(float i in ChangeXYGear){
                    if (Vector2.Distance(new Vector2(x, y), new Vector2(i, -25.4f)) > 1f){
                        if(Vector2.Distance(new Vector2(x, y), new Vector2(x, -25.4f)) <= 0.3f)
                            gx = true;
                        else
                            gy = true;
                    }
                    else{
                        gx = false;
                        gy = false;
                        break;
                    }
                }

                if(gx) movementY = 0;
                if(gy) movementX = 0;
                Gear.transform.Translate(new Vector2(movementX, movementY));
                currentChanged += Vector2.Distance(new Vector2(x, y), GearTransform.anchoredPosition);
                  
            }
    }
}
