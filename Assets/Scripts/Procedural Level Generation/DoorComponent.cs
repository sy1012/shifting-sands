using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour
{
    private Door door;
    private LineRenderer lr;
    public void SetDoor(Door ownerDoor)
    {
        door = ownerDoor;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetUpLineRenderer()
    {
        var templatesLR = FindObjectsOfType<LineRenderer>();
        LineRenderer template = null;
        foreach (var lr in templatesLR)
        {
            //Template could be null if someone renames this
            if (lr.name == "DoorLineRenderer")
            {
                template = lr;
            }
        }
        lr = gameObject.AddComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        Transform[] doorTr = door.GetDoorTransforms();
        lr.SetPosition(0,doorTr[0].transform.position);
        lr.SetPosition(1,doorTr[1].transform.position);
        lr.material = template.material;
        lr.startColor = Color.cyan;
        lr.endColor = Color.cyan;
        lr.startWidth = 0.35f;
        lr.endWidth = 0.35f;
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
