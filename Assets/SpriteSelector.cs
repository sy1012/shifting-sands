using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour
{

    public List<GameObject> doorTypes;
    public List<GameObject> lockedDoorTypes;
    // Start is called before the first frame update
    void Start()
    {
        //Default state is unlocked door sprites
        SetUnlocked();
    }
    public void SetLocked()
    {
        SetInactive();
        if (transform.rotation.eulerAngles.z == 0)
        {
            lockedDoorTypes[0].SetActive(true);
        }
        else if (transform.rotation.eulerAngles.z == 90)
        {
            lockedDoorTypes[1].SetActive(true);
        }
        else if (transform.rotation.eulerAngles.z == 180)
        {
            lockedDoorTypes[2].SetActive(true);
        }
        else if (transform.rotation.eulerAngles.z == 270)
        {
            lockedDoorTypes[3].SetActive(true);
        }
        this.enabled = false;
    }
    public void SetUnlocked()
    {
        SetInactive();
        if (transform.rotation.eulerAngles.z == 0)
        {
            doorTypes[0].SetActive(true);
        }
        else if (transform.rotation.eulerAngles.z == 90)
        {
            doorTypes[1].SetActive(true);
        }
        else if (transform.rotation.eulerAngles.z == 180)
        {
            doorTypes[2].SetActive(true);
        }
        else if (transform.rotation.eulerAngles.z == 270)
        {
            doorTypes[3].SetActive(true);
        }
        this.enabled = false;
    }
    private void SetInactive()
    {
        foreach (var door in doorTypes)
        {
            door.SetActive(false);
        }
        foreach (var door in lockedDoorTypes)
        {
            door.SetActive(false);
        }
    }
}
