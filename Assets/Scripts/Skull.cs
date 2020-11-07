using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skull enemy
public class Skull : Enemy
{

    [SerializeField]
    private Room room;
    [SerializeField]
    private AIDestinationSetter destination;
    [SerializeField]
    private Transform Player;
    private PlayerStateMachine psm;
    

    // Start is called before the first frame update
    void Start()
    {
        damageSpeed = 1;
        damage = 10;
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 0.9f, 0);
        psm = Player.GetComponent<PlayerStateMachine>();
        
    }

    private void Update()
    {
        if (room == null)
        {
            throw new System.Exception("The Skull enemy:" + transform.name + "'s room is Null. Set Skull's room.");
        }
        if (psm == null)
        {
            throw new System.Exception("The Skull enemy:" + transform.name + "'s target player is Null");
        }
        if(room.Equals(psm.GetRoom()))
        {
            destination.target = Player;
        }
    }

    

}
