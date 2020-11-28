using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
public abstract class State
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
    public virtual IEnumerator Inventory()
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
        //Check if player has moved into door trigger area
        // Door Trigger Logic
        var triggerCollisions = new List<Collider2D>(psm.GetTriggerCollisions);
        foreach (var collision in triggerCollisions)
        {
            var dc = collision.GetComponent<DoorComponent>();
            if (dc != null)
            {
                //Does nothing right now, but might be useful later
                EventManager.TriggerDoorEntered(dc);
                //Make new transition state, set it up with the doors involved, set state to the Transition
                var transition = new TransitionState(psm);
                transition.startDoor = collision.transform;
                transition.endDoor = dc.GetSisterDoor();
                psm.SetState(transition);
                return;
            }
        }
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

    public override IEnumerator Inventory()
    {
        EventManager.TriggerOnInventoryTrigger();
        yield break;
    }

    /// <summary>
    /// A coroutine that runs to handle the Player interaction with items and objects.
    /// </summary>
    /// <returns></returns>
    public override IEnumerator Interact()
    {
        //Interact with items
        List<GameObject> items = DungeonMaster.getLootInRange(psm.transform.position, 1.5f);
        foreach (GameObject item in items)
        {
            if (item.GetComponent<Item>() != null && item.GetComponent<Item>().data.itemName == "Coin") { Camera.main.GetComponent<Inventory>().PickUpCoin(1); GameObject.Destroy(item); }
            item.GetComponent<ItemArchtype>().PickedUp();
            yield return null;
        }
        
        //Interact with objects with Interactable 
        var triggerCollisions = new List<Collider2D>(psm.GetTriggerCollisions);
        foreach (var collision in triggerCollisions)
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable != null)
            {
                //Delegate to the Interactable
                interactable.Interact(psm.gameObject);
                yield return null;
            }


        }
        yield return null;
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
        if (psm.GetWeapon != null)
        {
            // trigger attack event
            EventManager.TriggerOnAttack();
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 atkHeading = (mouse - psm.transform.position);
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

            if (psm.GetWeapon != null)
            {
                psm.GetWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
                psm.GetWeapon.transform.position = psm.transform.position+(mouse - psm.transform.position).normalized*3;
                psm.GetWeapon.Attack();
                //Set player to look in the direction attacking
                psm.animator.SetFloat("PrevVertical", atkHeading.y);
                psm.animator.SetFloat("PrevHorizontal", atkHeading.x);
                psm.animator.SetFloat("Vertical", atkHeading.y);
                psm.animator.SetFloat("Horizontal", atkHeading.x);

            }
        }
        yield return new WaitForSeconds(0.5f);
        psm.SetState(new NormalState(psm));
    }
    public override void Execute()
    {
        psm.animator.SetFloat("Speed", psm.GetArrowKeysDirectionalInput().sqrMagnitude);
        psm.MoveCharacter(psm.GetArrowKeysDirectionalInput(), psm.speed/4);
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
        float dashAmount = 4f;//in units
        float dashTime = .2f;//in seconds
        int segments = 4;//amount of splits for dash
        float tick = 0;//time tracker
        Vector2 keyInput = psm.GetArrowKeysDirectionalInput();
        Vector3 keyInputV3 = new Vector3(keyInput.x, keyInput.y, 0);
        // trigger on dash event
        EventManager.TriggerOnDash();
        psm.animator.SetTrigger("Dash");
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
        psm.animator.SetTrigger("DashOver");
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
    EquipmentManager equipment = GameObject.Find("Equipment").GetComponent<EquipmentManager>();
    float stunTime = 0.35f;
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
        if (equipment.GetArmour().data != null)
        {
            float armour = equipment.GetArmour().data.flatArmour;
            if (equipment.GetRune().data != null && equipment.GetRune().data.type == RuneType.Type.earth)
            {
                Debug.Log("Is it doing anything?");
                armour += armour * equipment.GetRune().data.effectiveness;
            }
            psm.health = (int)(((psm.health + psm.health * armour) - damageTaking) / (1 + armour));
            if (psm.health <= 0) { PlayerDeath(); }
        }
        else psm.health -= damageTaking;
        if (psm.health <= 0) { PlayerDeath(); }
        var healthbar = psm.GetComponentInChildren<Healthbar>();
        Vector3 dir = hitCollision.transform.position - psm.transform.position;
        dir = dir.normalized;
        //Vector3 target = psm.transform.position - dir*damageTaking / 4;
        healthbar.SetHealth(psm.health);
        // Triggers the OnPlayerHit event
        EventManager.TriggerOnPlayerHit();
        yield return new WaitForSeconds(stunTime);
        psm.InvincibleTime = invincibleTime;
        psm.SetState(new NormalState(psm));
        yield break;

    }

    public void PlayerDeath()
    {
        // clean up the hierarchy
        GameObject.Destroy(GameObject.Find("Canvas"));
        GameObject.Destroy(GameObject.Find("SoundManager(Clone)"));
        GameObject.Destroy(GameObject.Find("DungeonData"));
        GameObject.Destroy(GameObject.Find("Equipment"));

        Debug.Log("Death Noises");
        SceneManager.LoadScene("MainMenu");
    }
}

