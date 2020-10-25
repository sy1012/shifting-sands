using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] public LayerMask dashLayerMask;
    public Rigidbody2D rb;
    public Animator animator;
    GameObject text;
    GameObject background;
    public float playerRootOffset = -0.5f;
    public Weapon weaponEquiped;
    KeyCode interactKey = KeyCode.E;
    KeyCode rollKey = KeyCode.Space;
    
    //!!The Behavioural State of the Player!!
    public State state;
    // Debugging representation of state
    public StateEnum currentState;
    public float speed;

    //Change state. This is used by states to change the players current state. Always call states Enter() routine. Enum is just for debugging.
    public void SetState(State newstate)
    {
        currentState = newstate.stateEnum;
        state = newstate;
        StartCoroutine(newstate.Enter());
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(new NormalState(this));
    }

    //Try to get input from player here consistently
    public Vector2 GetArrowKeysDirectionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }


    public void MoveCharacter(Vector2 heading, float speed)
    {
        rb.MovePosition(rb.position + heading * speed * Time.fixedDeltaTime);
    }

    //Apply Normal movement of the player based on arrow keys
    public void NormalMovement()
    {
        Vector2 input = GetArrowKeysDirectionalInput();
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        MoveCharacter(GetArrowKeysDirectionalInput(), speed);
    }
    //Fraction of normal movement. e.g. post roll, or hit
    public void NormalMovementFraction(float fraction)
    {
        Vector2 input = GetArrowKeysDirectionalInput();
        animator.SetFloat("Horizontal", input.x*fraction);
        animator.SetFloat("Vertical", input.y*fraction);
        animator.SetFloat("Speed", input.sqrMagnitude*fraction);
        MoveCharacter(GetArrowKeysDirectionalInput(), speed*fraction);
    }

    // Update is called once per frame
    void Update()
    {
        //State Specific Behaviour

        //Handle Input
        if (Input.GetKeyDown(interactKey))
        {
            StartCoroutine(state.Interact());
        }

        if (Input.GetKeyDown(rollKey))
        {
            StartCoroutine(state.OnRoll());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Change State to Attack");
            StartCoroutine(state.OnAttack());
        }

        state.Execute();

        //Global Player State Behaviour

        //Handle Loot
        if (DungeonMaster.getLootInRange(this.transform.position, 1).Count != 0)
        {
            (text, background) = DungeonMaster.getLootInRange(this.transform.position, 1)[0].GetComponent<Weapon>().Info();
            text.transform.position = this.transform.position + new Vector3(0, 2, 0);
            background.transform.position = this.transform.position + new Vector3(0, 2, 0);
        }
        if (DungeonMaster.getLootOuttaRange(this.transform.position, 1).Count != 0)
        {
            List<GameObject> loot = DungeonMaster.getLootOuttaRange(this.transform.position, 1);
            foreach (GameObject item in loot)
            {
                item.GetComponent<Weapon>().DestroyInfo();
            }
        }

    }
    // Get the world position of the characters feet, the place he is standing. This is determined by an offset from the transform center
    public Vector2 GetRoot()
    {
        return new Vector2(transform.position.x, transform.position.y + playerRootOffset);
    }
    //  Handle Triggers
    private void OnTriggerStay2D(Collider2D collision)
    {
        state.HandleTrigger(collision);
    }
}
