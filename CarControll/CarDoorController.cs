using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum DoorState
{
    Closed,
    Opening,
    Opened,
    Closing,
}
[Serializable]
public class Doors
{
    [HideInInspector]
    public float currentDegree;
    public DoorState DoorState;
    private DoorState PreviousState;
    [HideInInspector]
    public Vector3 CurrentAngle;
    public GameObject DoorObject;
    private Vector3 OriginalPosition;
    [SerializeField] public Vector3 startAngle;
    [SerializeField] public Vector3 endAngle;
    private Quaternion OriginalAngle;
    [Header("Hinges Settings")]
    public Vector3 RotationAxis;
    [Range(0f,720f)]
    public float RotationAngle;
    public void InitOriginalValues()
    {
        if (DoorObject != null)
        {
            OriginalPosition = DoorObject.transform.localPosition;
            OriginalAngle = DoorObject.transform.localRotation;
            DoorState = DoorState.Closed;
            PreviousState = DoorState.Closed;
        }
    }
    public Vector3 GetOriginalPosition()
    {
        return OriginalPosition;
    }
    public Quaternion GetOriginalAngle()
    {
        return OriginalAngle;
    }
    public void SetCurrentStateSaved()
    {
        PreviousState = DoorState;
    }
    public DoorState GetPreviousDoorState()
    {
        return PreviousState;
    }
}
public class CarDoorController : MonoBehaviour
{
    [Range(0.25f, 120f)]
    public float DoorSpeed = 45f;
    public List<Doors> DoorList = new List<Doors>();
    void Start()
    {
        for (int i=0;i<DoorList.Count;i++)
        {
            DoorList[i].InitOriginalValues();
        }
    }
    public void ToggleDoorControl(int doorIndex)
    {
        if (doorIndex < DoorList.Count && doorIndex >= 0)
        {
            DoorList[doorIndex].CurrentAngle = DoorList[doorIndex].DoorObject.transform.localEulerAngles;
            switch (DoorList[doorIndex].DoorState)
            {
                case DoorState.Closed:
                    DoorList[doorIndex].DoorState = DoorState.Opening;
                    break;
                case DoorState.Opened:
                    DoorList[doorIndex].DoorState = DoorState.Closing;
                    break;
                case DoorState.Closing:
                    DoorList[doorIndex].DoorState = DoorState.Opening;
                    break;
                case DoorState.Opening:
                    DoorList[doorIndex].DoorState = DoorState.Closing;
                    break;
            }
        }
    }
    private void FixedUpdate() {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for(int i=0; i<DoorList.Count; i++){
                ToggleDoorControl(i);
            }
        }
        DoorAnimaton();
    }

    private void DoorAnimaton()
    {
        foreach (Doors curDoor in DoorList)
        {
            if (curDoor.DoorObject != null)
            {
                switch (curDoor.DoorState)
                {
                    case DoorState.Closed:
                        curDoor.DoorObject.transform.localRotation = curDoor.GetOriginalAngle();
                        break;
                    case DoorState.Opened:
                        break;
                    case DoorState.Opening:
                        if (curDoor.GetPreviousDoorState() == DoorState.Opened)
                            break;
                        curDoor.currentDegree += DoorSpeed*Time.deltaTime / curDoor.RotationAngle;
                        if (curDoor.currentDegree < 1)
                        {
                            curDoor.DoorObject.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(curDoor.CurrentAngle), Quaternion.Euler(curDoor.endAngle), 
                            curDoor.currentDegree);
                        }
                        else
                        {
                            curDoor.currentDegree = 0;
                            curDoor.DoorState = DoorState.Opened;
                            curDoor.SetCurrentStateSaved();
                        }
                        break;
                    case DoorState.Closing:
                        if (curDoor.GetPreviousDoorState() == DoorState.Closed)
                            break;
                        curDoor.currentDegree += DoorSpeed*Time.deltaTime / curDoor.RotationAngle;
                        if (curDoor.currentDegree < 1)
                        {
                            curDoor.DoorObject.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(curDoor.CurrentAngle), Quaternion.Euler(curDoor.startAngle), 
                            curDoor.currentDegree);
                        }
                        else
                        {
                            curDoor.currentDegree = 0;
                            curDoor.DoorState = DoorState.Closed;
                            curDoor.SetCurrentStateSaved();
                        }
                        break;
                }
            }
        }
    }
}