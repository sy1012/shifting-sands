﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum StateEnum
{
    //Just for debugging because Unity cannot serialize abstract states
    Normal,
    Roll,
    Attack,
    Hit,
    Transition
}
/// <summary>
/// Abstract class for all states. Add new behaviours declarations here and implement in a concrete state
/// Makes sure they ruturn null or if a IEnumerator, yield break.
/// If a state does not have an implemented command, the command will be ignored.
/// </summary>
public  abstract class State
{
    protected PlayerStateMachine psm;
    public StateEnum stateEnum;
    public State(PlayerStateMachine playerStateMachine)
    {
        psm = playerStateMachine;
    }

    public virtual IEnumerator Enter()
    {
        yield break;
    }
    public virtual void Execute()
    {
    }
    public virtual IEnumerator Interact()
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        yield break;
    }

    public virtual IEnumerator OnAttack()
    {
        yield break;
    }
    public virtual IEnumerator OnRoll()
    {
        yield break;
    }
    public virtual IEnumerator OnHit(int damage, Collision2D collision)
    {
        yield break;
    }
    public virtual void HandleTrigger(Collider2D collision)
    {
        return;
    }
    public override String ToString()
    {
        return "State";
    }

}
public class NormalState : State
{
    public NormalState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        stateEnum = StateEnum.Normal;
    }

    public override void Execute()
    {
        psm.NormalMovement();
    }

    public override IEnumerator OnAttack()
    {
        //new attack
        psm.SetState(new AttackState(psm));
        yield break;
    }

    public override IEnumerator OnRoll()
    {
        //new roll
        psm.SetState(new RollState(psm));
        yield break;
    }

    public override IEnumerator OnHit(int damage, Collision2D collision)
    {
        psm.SetState(new HitState(psm,damage,collision));
        yield break;
    }

    public override IEnumerator Interact()
    {
        //Interact with items
        List<GameObject> items = DungeonMaster.getLootInRange(psm.transform.position, 1);
        foreach (GameObject item in items)
        {
            DungeonMaster.loot.Remove(item);
            yield return null;
        }
    }

    public override void HandleTrigger(Collider2D collision)
    {
        //Door Trigger and Player Inputs to go to next room
        if (Input.GetKeyDown(KeyCode.E) && collision.GetComponent<DoorComponent>())
        {
            var dc = collision.GetComponent<DoorComponent>();
            //Does nothing right now, but might be useful later
            EventManager.TriggerDoorEntered(dc);
            //Make new transition state, set it up with the doors involved, set state to the Transition
            var transition = new TransitionState(psm);
            transition.startDoor = collision.transform;
            transition.endDoor = dc.GetSisterDoor();
            psm.SetState(transition);
        }
    }

    public override string ToString()
    {
        return base.ToString()+" Normal";
    }
}

public class AttackState : State
{
    public AttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        stateEnum = StateEnum.Attack;
    }
    /// <summary>
    /// Main player attack behaviour
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Enter()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Rad2Deg * Mathf.Atan((psm.transform.position.y - mouse.y) / (psm.transform.position.x - mouse.x));

        // now convert the angle into a degrees cw of up(north) based on the current value of direction and what quadrant it is in
        // quadrant 1
        if (mouse.y >= psm.transform.position.y && mouse.x >= psm.transform.position.x) angle -= 90;

        // quadrant 2
        else if (mouse.y >= psm.transform.position.y) angle += 90;

        // quadrant 3
        else if (mouse.x <= psm.transform.position.x) angle += 90;

        // quadrant 4
        else angle -= 90;
        psm.weaponEquiped.transform.rotation = Quaternion.Euler(0, 0, angle);
        psm.weaponEquiped.Attack();
        yield return new WaitForSeconds(1f);
        psm.SetState(new NormalState(psm));
    }
    public override void Execute()
    {
        //Allow partial movement during an attack state
        psm.NormalMovementFraction(0.5f);
    }
    public override string ToString()
    {
        return base.ToString() + " Attack";
    }
}

public class RollState : State
{
    public RollState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        stateEnum = StateEnum.Roll;
    }
    
    /// <summary>
    /// Roll Coroutine Behaviour
    /// Lasts a certain amount of time split into x segments.
    /// Post Cond - set state back to Normal
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Enter()
    {
        float dashAmount = 3f;//in units
        float dashTime = 0.05f;//in seconds
        int segments = 6;//amount of splits for dash
        float tick = 0;//time tracker
        Vector2 keyInput = psm.GetArrowKeysDirectionalInput();
        Vector3 keyInputV3 = new Vector3(keyInput.x, keyInput.y, 0);
        //Change the color for a cool effect
        var spriteRenderer = psm.spriteRenderer;
        spriteRenderer.color = Color.cyan/4;
        //Start dash loop. Continue dashing each timestep until we go x dash segments or hit a wall. The advantage of a couroutine is we get behaviour over time
        //and only need to call Enter() once. Once done change state back to Normal 
        while (tick<dashTime)
        {
            Vector3 dashPosition = psm.transform.position + keyInputV3 * dashAmount/segments;
            RaycastHit2D raycastHit2d = Physics2D.Raycast(psm.GetRoot(), keyInput, dashAmount/segments, psm.dashLayerMask);
            // RayCast to avoid teleporting through walls
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
                dashPosition = new Vector2(dashPosition.x, dashPosition.y - psm.playerRootOffset);
                psm.rb.MovePosition(dashPosition);
                // Have hit wall end coroutine
                break;
            }
            psm.rb.MovePosition(dashPosition);
            //wait some time then come back to this line, continue loop
            yield return new WaitForSeconds(dashTime/segments);
            tick += dashTime / segments;
        }
        //reset color back to white
        spriteRenderer.color = new Color(255,255,255,255);
        psm.SetState(new NormalState(psm));
        yield break;
    }

    public override string ToString()
    {
        return base.ToString() + " Roll";
    }
}
public class HitState:State
{
    float stunTime = 0.5f;
    float invincibleTime = 0.5f;
    int damageTaking;
    Collision2D hitCollision;
    public HitState(PlayerStateMachine playerStateMachine, int damage, Collision2D collision):base(playerStateMachine)
    {
        damageTaking = damage;
        hitCollision = collision;
        psm.currentState = StateEnum.Hit;
    }
    public override IEnumerator Enter()
    {
        psm.animator.SetTrigger("hit");
        psm.health -= damageTaking;
        var healthbar = psm.GetComponentInChildren<Healthbar>();
        Vector3 dir = hitCollision.transform.position - psm.transform.position;
        dir = dir.normalized;
        //Vector3 target = psm.transform.position - dir*damageTaking / 4;
        healthbar.SetHealth(psm.health);
        yield return new WaitForSeconds(stunTime);
        psm.InvincibleTime = invincibleTime;
        psm.SetState(new NormalState(psm));
        yield break;
    }

}
