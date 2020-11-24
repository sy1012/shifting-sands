using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Rhoss : Enemy
{
    public float mvmt_timer = 0.0f; //used to update enemy movement every few seconds.
    public int mvmt_seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageSpeed = 1;
        damage = 25;
        detectionRange = 3;
        randTarget = new GameObject().transform;
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 1.7f, 0);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //update the timer and number of seconds passed since last movement
        mvmt_timer += Time.deltaTime;
        mvmt_seconds = (int)(mvmt_timer % 60);

        if (room == null)
        {
            throw new System.Exception("The Mummy enemy:" + transform.name + "'s room is Null. Set Mummy's room.");
        }
        if (psm == null)
        {
            throw new System.Exception("The Mummy enemy:" + transform.name + "'s target player is Null");
        }
        //if the player is in the same room as the Mummy, and is within the Mummy's detection radius, pursue the player to attack!
        if (room.Equals(psm.GetRoom()) && Vector3.Distance(Player.position, transform.position) <= detectionRange)
        {
            destination.target = Player;
            int x = UnityEngine.Random.Range(0, 1000);
            if (x == 0)
            {
                EventManager.TriggerOnMummyAgro();
            }
        }
        //enter randomly-moving patrol mode if the player isn't nearby.
        //(If the player is in the same room and is out of range, patrol randomly).
        else if (room.Equals(psm.GetRoom()) && Vector3.Distance(Player.position, transform.position) > detectionRange)
        {
            //only perform a random move if a few seconds have passed.
            //Mummies are slow, and so prefer not to move as often.
            if (mvmt_seconds >= 5)
            {
                //generate random increments by which to move for the x and y directions.
                //movement increments are smaller for mummies, who struggle to get around.
                float rand_x = UnityEngine.Random.Range(-40f, 40f);
                float rand_y = UnityEngine.Random.Range(-40f, 40f);
                //set the new position for the invisible gameobject which is the target of the pathfinding.
                Vector3 newPosition = new Vector3(Mathf.Lerp(transform.position.x, rand_x, Time.deltaTime), Mathf.Lerp(transform.position.y, rand_y, Time.deltaTime), transform.position.z);
                randTarget.position = newPosition;
                //set the pathfinding target to be the random destination.
                destination.target = randTarget;
                //reset this so another several seconds can be counted for the next move.
                mvmt_timer = 0;
            }
        }
    }
}
