using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : Interactable
{
    public Door door;

    public Transform GetSisterDoor()
    {
        //One of these will be this transfomr
        Transform[] doorTransforms = door.GetDoorTransforms();
        return doorTransforms[0] == transform ? doorTransforms[1] : doorTransforms[0]; 
    }
    public void SetDoor(Door ownerDoor)
    {
        door = ownerDoor;
    }
    public void DestroyDoor()
    {
        Destroy(gameObject);
    }
}