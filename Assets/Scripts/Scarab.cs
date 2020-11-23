using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scarab enemy
public class Scarab : Enemy
{
    public float mvmt_timer = 0.0f; //used to update enemy movement every few seconds.
    public int mvmt_seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageSpeed = 1;
        damage = 5;
        detectionRange = 7;
        randTarget = new GameObject().transform;
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        base.Start();
    }

    private void Update()
    {
        // the player could die at which point the player scripts will handle the event of death
        try
        {
            //update the timer and number of seconds passed since last movement
            mvmt_timer += Time.deltaTime;
            mvmt_seconds = (int)(mvmt_timer % 60);

            if (room == null)
            {
                throw new System.Exception("The Scarab enemy:" + transform.name + "'s room is Null. Set Scarab's room.");
            }
            if (psm == null)
            {
                throw new System.Exception("The Scarab enemy:" + transform.name + "'s target player is Null");
            }
            //if the player is in the same room as the Scarab, and is within the Scarab's detection radius, pursue the player to attack!
            if (room.Equals(psm.GetRoom()) && Vector3.Distance(Player.position, transform.position) <= detectionRange)
            {
                destination.target = Player;
                int x = UnityEngine.Random.Range(0, 1000);
                if (x == 0)
                {
                    EventManager.TriggerOnScarabAgro();
                }
            }
            //enter randomly-moving patrol mode if the player isn't nearby.
            //(if the player is in the same room and is out of range, patrol randomly).
            else if (room.Equals(psm.GetRoom()) && Vector3.Distance(Player.position, transform.position) > detectionRange)
            {
                //only perform a random move if a few seconds have passed.
                //Scarabs are fast, and so move quite a bit.
                if (mvmt_seconds >= 2)
                {
                    //generate random increments by which to move for the x and y directions.
                    //movement increments are larger for snakes, which move quite quickly.
                    float rand_x = UnityEngine.Random.Range(-100f, 100f);
                    float rand_y = UnityEngine.Random.Range(-100f, 100f);
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
        catch { }  // ignore on purpose...see try
    }
}
