using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOrientation : MonoBehaviour
{
    public enum Heading { N, E, S, W };
    public Heading heading;
    private void Awake()
    {
        switch (heading)
        {
            case Heading.N:
                transform.up = Vector3.up;
                break;
            case Heading.E:
                transform.up = Vector3.right;
                break;
            case Heading.S:
                transform.up = Vector3.down;
                Debug.Log("Flip door" + transform.up);
                break;
            case Heading.W:
                transform.up = Vector3.left;
                break;
        }
    }
    private void Start()
    {
        switch (heading)
        {
            case Heading.N:
                transform.up = Vector3.up;
                break;
            case Heading.E:
                transform.up = Vector3.right;
                break;
            case Heading.S:
                transform.up = Vector3.down;
                Debug.Log("Flip door" + transform.up);
                break;
            case Heading.W:
                transform.up = Vector3.left;
                break;
        }
    }
}
