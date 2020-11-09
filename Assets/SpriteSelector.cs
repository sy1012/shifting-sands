using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    public List<GameObject> doorTypes;
    // Start is called before the first frame update
    void Start()
    {
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
        this.enabled = false ;
    }

}
