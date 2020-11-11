using System.Collections;
using UnityEngine;

public class TransitionState : State
{
    public Transform startDoor;
    public Transform endDoor;
    public TransitionState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        stateEnum = StateEnum.Transition;
    }
    public override IEnumerator Enter()
    {
        //Step one, move through door, turn off coliders
        Vector3 target;
        if (startDoor == null)
        {
            throw new System.Exception("start door should not be null");
        }
        if (endDoor == null)
        {
            throw new System.Exception("end door should not be null");
        }
        target = startDoor.position;
        TurnOffColliders();
        bool goneThroughDoor = false;
        while (!goneThroughDoor)
        {
            psm.MoveCharacter(target - psm.transform.position, 4f);
            if ((target - psm.transform.position).magnitude <0.1f)
            {
                goneThroughDoor = true;
            }
            yield return null;
        }
        //Step two, turn off renderer and move to next door
        target = endDoor.position;
        bool atNextDoor = false;
        while (!atNextDoor)
        {
            psm.MoveCharacter(target - psm.transform.position, 12f);
            if ((target - psm.transform.position).magnitude < 0.2f)
            {
                atNextDoor = true;
            }
            yield return null;
        }
        //Step three, move into room, turn colliders back on. Keep dungeoneering
        target = endDoor.position - endDoor.up*1.5f;
        bool inRoom = false;
        while (!inRoom)
        {
            psm.MoveCharacter(target - psm.transform.position, 4f);
            if ((target - psm.transform.position).magnitude < 0.1f)
            {
                inRoom = true;
            }
            yield return null;
        }

        Room newRoom = endDoor.GetComponentInParent<Room>();
        if(newRoom != null)
        {
            psm.SetRoom(newRoom);
        }
        TurnOnColliders();
        psm.SetState(new NormalState(psm));
    }
    public void TurnOffColliders()
    {
        var collider = psm.transform.GetComponent<CircleCollider2D>();
        collider.enabled = false;
    }
    public void TurnOnColliders()
    {
        var collider = psm.transform.GetComponent<CircleCollider2D>();
        collider.enabled = true;
    }
}