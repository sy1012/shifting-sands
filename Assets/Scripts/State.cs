using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum StateEnum
{
    Normal,
    Roll,
    Attack
}
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

    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator Roll()
    {
        yield break;
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

    public override IEnumerator Attack()
    {
        //new attack
        psm.SetState(new AttackState(psm));
        yield break;
    }

    public override IEnumerator Roll()
    {
        psm.SetState(new RollState(psm));
        yield break;
    }

    public override IEnumerator Interact()
    {
        List<GameObject> items = DungeonMaster.getLootInRange(psm.transform.position, 1);
        foreach (GameObject item in items)
        {
            DungeonMaster.loot.Remove(item);
            yield return null;
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
    public override IEnumerator Enter()
    {
        float dashAmount = 3f;//in units
        float dashTime = 0.05f;//in seconds
        int segments = 6;//amount of splits for dash
        float tick = 0;//time tracker
        Vector2 keyInput = psm.GetArrowKeysDirectionalInput();
        Vector3 keyInputV3 = new Vector3(keyInput.x, keyInput.y, 0);

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
            yield return new WaitForSeconds(dashTime/segments);
            tick += dashTime / segments;
        }
        psm.SetState(new NormalState(psm));
        yield break;
    }

    public override string ToString()
    {
        return base.ToString() + " Roll";
    }
}

