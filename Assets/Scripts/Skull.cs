using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skull enemy
public class Skull : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 1.1f, 0);
    }

}
