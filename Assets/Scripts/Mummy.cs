using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mummy enemy
public class Mummy : Enemy
{
    public float mvmt_timer = 0.0f; //used to update enemy movement every few seconds.
    public int mvmt_seconds = 0;    //discrete version of the above timer
    public int charge_chance = 0;  //used to store a randomly generated number determining if a charge occurs
    public int charge_speed = 12;   //the speed of the mummy in its enraged state
    public bool charging = false;  //a flag used to identify whether or not the mummy is charging



    // Start is called before the first frame update
    void Start()
    {
        damageSpeed = 1;
        damage = 25;
        detectionRange = 5;
        randTarget = new GameObject().transform;
        //move healthbar to a more suitable position
        healthCanvas.transform.position = transform.position + new Vector3(0, 1.7f, 0);
        base.Start();
    }

    private void Update()
    {
        // the player could die at which point the player scripts will handle the event of death
        try
        {
            //update the timer and number of seconds passed since last movement
            //this will influence random patrolling if the player is out of range,
            //or the possibility of a charge attack if they aren't.
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
                //if several seconds have passed and the mummy isn't already charging, allow him to attempt a charge.
                if (mvmt_seconds >= 3 && !charging)
                {
                    charge_chance = UnityEngine.Random.Range(0, 100);
                    //if the randomly generated number is in the acceptable range, perform the charge.
                    if (charge_chance <= 60 )
                    {
                        //reset the timer, allowing a gap between charges.
                        mvmt_timer = 0;
                        charging = true;
                        //give the player an auditory cue to inform them that a charge is coming.
                        EventManager.TriggerOnMummyAgro();
                        //begin the charge routine.
                        StartCoroutine("Charge");
                    }
                    //if the randomly-generated value was not in the right range, reset it as well.
                    mvmt_timer = 0;
                } 
                //when the player is in range and the mummy isnt charging, follow the player as normal.
                destination.target = Player;

                //occasionally play the mummy agro noise.
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
        catch { }// ignore on purpose...see try
    }


     IEnumerator Charge()
    {
        //stand still for a moment to telegraph that you're about to charge
        this.GetComponent<AIPath>().maxSpeed = 0;
        yield return new WaitForSeconds(2.0f);

        //start running
        this.GetComponent<AIPath>().maxSpeed = charge_speed;
        Debug.Log("Reached animator speed increase.");
        GetComponentInChildren<Animator>().speed = 3;
        Debug.Log("Passed animator speed thing.");

        //determine the general direction of the player in relation to yourself
        Vector3 playerDirection = Player.position - transform.position;
        if ((playerDirection.x - transform.position.x) > (playerDirection.y - transform.position.y))
        {
            //if they are further away in the x direction, move further in that direction to attack them.
            Vector3 newPosition = new Vector3(transform.position.x + 80.0f, transform.position.y);
            randTarget.position = newPosition;
            destination.target = randTarget;
            yield return new WaitForSeconds(1.0f);
        }
        else
        {
            //otherwise, do the same as above, but move further in the y direction.
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 80.0f);
            randTarget.position = newPosition;
            destination.target = randTarget;
            yield return new WaitForSeconds(1.0f);
        }
        
        //once you have completed your charge, slow to normal speed, reset the charging flag, and resume normal pursuit.
        this.GetComponent<AIPath>().maxSpeed = 0.5f;
        GetComponentInChildren<Animator>().speed = 1;
        charging = false;
        destination.target = Player;
        StopCoroutine("Charge");
        
    }
}
