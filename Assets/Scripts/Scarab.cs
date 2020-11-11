using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skull enemy
public class Scarab : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        damageSpeed = 1;
        damage = 1;
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        base.Start();
    }

    private void Update()
    {
        if (room == null)
        {
            throw new System.Exception("The Scarab enemy:" + transform.name + "'s room is Null. Set Scarab's room.");
        }
        if (psm == null)
        {
            throw new System.Exception("The Scarab enemy:" + transform.name + "'s target player is Null");
        }
        if(room.Equals(psm.GetRoom()))
        {
            destination.target = Player;
        }
    }

    

}
