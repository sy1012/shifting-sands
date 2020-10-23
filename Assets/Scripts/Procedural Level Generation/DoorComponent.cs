using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour
{
    private Door door;

    public void SetDoor(Door ownerDoor)
    {
        door = ownerDoor;
    }
    // Start is called before the first frame update
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyDoor()
    {
        Destroy(gameObject);
    }
}
